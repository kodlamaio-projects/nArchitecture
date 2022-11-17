using Application.Features.Rentals.Dtos;
using Application.Features.Rentals.Rules;
using Application.Services.AdditionalServiceService;
using Application.Services.CarService;
using Application.Services.FindeksCreditRateService;
using Application.Services.InvoiceService;
using Application.Services.ModelService;
using Application.Services.POSService;
using Application.Services.RentalsIAdditionalServiceService;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Logging;
using Core.Mailing;
using Domain.Entities;
using MediatR;
using MimeKit;

namespace Application.Features.Rentals.Commands.CreateRental;

public class CreateRentalCommand : IRequest<CreatedRentalDto>, ILoggableRequest
{
    public int ModelId { get; set; }
    public int CustomerId { get; set; }
    public DateTime RentStartDate { get; set; }
    public DateTime RentEndDate { get; set; }
    public int RentStartRentalBranchId { get; set; }
    public int RentEndRentalBranchId { get; set; }
    public int[] AdditionalServiceIds { get; set; }

    public class CreateRentalCommandHandler : IRequestHandler<CreateRentalCommand, CreatedRentalDto>
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IMapper _mapper;
        private readonly RentalBusinessRules _rentalBusinessRules;

        private readonly IAdditionalServiceService _additionalServiceService;
        private readonly ICarService _carService;
        private readonly IFindeksCreditRateService _findeksCreditRateService;
        private readonly IInvoiceService _invoiceService;
        private readonly IModelService _modelService;
        private readonly IMailService _mailService;
        private readonly IPOSService _posService;
        private readonly IRentalsAdditionalServiceService _rentalsAdditionalServiceService;

        public CreateRentalCommandHandler(IRentalRepository rentalRepository, IMapper mapper,
                                          RentalBusinessRules rentalBusinessRules,
                                          IAdditionalServiceService additionalServiceService, ICarService carService,
                                          IFindeksCreditRateService findeksCreditRateService,
                                          IInvoiceService invoiceService, IModelService modelService,
                                          IMailService mailService, IPOSService posService,
                                          IRentalsAdditionalServiceService rentalsAdditionalServiceService)
        {
            _rentalRepository = rentalRepository;
            _mapper = mapper;
            _rentalBusinessRules = rentalBusinessRules;
            _additionalServiceService = additionalServiceService;
            _carService = carService;
            _findeksCreditRateService = findeksCreditRateService;
            _invoiceService = invoiceService;
            _modelService = modelService;
            _mailService = mailService;
            _posService = posService;
            _rentalsAdditionalServiceService = rentalsAdditionalServiceService;
        }

        public async Task<CreatedRentalDto> Handle(CreateRentalCommand request, CancellationToken cancellationToken)
        {
            FindeksCreditRate customerFindeksCreditRate =
                await _findeksCreditRateService.GetFindeksCreditRateByCustomerId(request.CustomerId);

            Car? carToBeRented = await _carService.GetAvailableCarToRent(
                                     request.ModelId, request.RentStartRentalBranchId, request.RentStartDate,
                                     request.RentEndDate);

            await _rentalBusinessRules.RentalCanNotBeCreatedWhenCustomerFindeksScoreLowerThanCarMinFindeksScore(
                customerFindeksCreditRate.Score, carToBeRented.MinFindeksCreditRate);

            Model model = await _modelService.GetById(carToBeRented.ModelId);

            Rental mappedRental = _mapper.Map<Rental>(request);
            mappedRental.CarId = carToBeRented.Id;
            //mappedRental.RentStartRentalBranchId = carToBeRented.RentalBranchId;
            mappedRental.RentStartKilometer = carToBeRented.Kilometer;

            IList<AdditionalService> additionalServices =
                await _additionalServiceService.GetListByIds(request.AdditionalServiceIds);
            decimal totalAdditionalServicesPrice = additionalServices.Sum(a => a.DailyPrice);

            decimal dailyPrice = model.DailyPrice + totalAdditionalServicesPrice;
            Invoice newInvoice = await _invoiceService.CreateInvoice(mappedRental, dailyPrice);

            await _posService.Pay(newInvoice.No, newInvoice.RentalPrice);

            await _invoiceService.Add(newInvoice);

            Rental createdRental = await _rentalRepository.AddAsync(mappedRental);
            await _rentalsAdditionalServiceService.AddManyByRentalIdAndAdditionalServices(
                createdRental.Id, additionalServices);


            var toEmailList = new List<MailboxAddress>
            {
                new("Ahmet Çetinkaya","ahmetcetinkaya7@outlook.com")
            };

            _mailService.SendMail(new Mail
            {
                Subject = "New Rental",
                TextBody = "A rental has been created.",
                ToList = toEmailList
            });

            CreatedRentalDto createdRentalDto = _mapper.Map<CreatedRentalDto>(createdRental);
            return createdRentalDto;
        }
    }
}