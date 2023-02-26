using Application.Features.Rentals.Constants;
using Application.Services.CarService;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using static Application.Features.Rentals.Constants.RentalsOperationClaims;
using static Domain.Constants.OperationClaims;

namespace Application.Features.Rentals.Commands.PickUp;

public class PickUpRentalCommand : IRequest<PickUpRentalResponse>, ISecuredRequest
{
    public int Id { get; set; }
    public int RentEndRentalBranchId { get; set; }
    public DateTime? ReturnDate { get; set; }
    public int RentEndKilometer { get; set; }

    public string[] Roles => new[] { Domain.Constants.OperationClaims.Admin, RentalsOperationClaims.Admin, Write, RentalsOperationClaims.Update };

    public class PickUpRentalCommandHandler : IRequestHandler<PickUpRentalCommand, PickUpRentalResponse>
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IMapper _mapper;

        private readonly ICarService _carService;

        public PickUpRentalCommandHandler(IRentalRepository rentalRepository, IMapper mapper, ICarService carService)
        {
            _rentalRepository = rentalRepository;
            _mapper = mapper;
            _carService = carService;
        }

        public async Task<PickUpRentalResponse> Handle(PickUpRentalCommand request, CancellationToken cancellationToken)
        {
            Rental rental = await _rentalRepository.GetAsync(r => r.Id == request.Id);
            //rental.RentEndRentalBranchId = request.RentEndRentalBranchId;
            rental.RentEndKilometer = request.RentEndKilometer;
            rental.ReturnDate = request.ReturnDate;

            await _carService.PickUpCar(rental);

            Rental updatedRental = await _rentalRepository.UpdateAsync(rental);
            PickUpRentalResponse updatedRentalDto = _mapper.Map<PickUpRentalResponse>(updatedRental);
            return updatedRentalDto;
        }
    }
}