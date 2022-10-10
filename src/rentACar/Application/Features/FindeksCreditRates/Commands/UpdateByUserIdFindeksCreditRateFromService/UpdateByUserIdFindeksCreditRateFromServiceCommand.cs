using Application.Features.FindeksCreditRates.Dtos;
using Application.Services.CustomerService;
using Application.Services.FindeksService;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.FindeksCreditRates.Commands.UpdateByUserIdFindeksCreditRateFromService;

public class UpdateByUserIdFindeksCreditRateFromServiceCommand : IRequest<UpdatedFindeksCreditRateDto>
{
    public int UserId { get; set; }
    public string IdentityNumber { get; set; }

    public class UpdateFindeksCreditRateFromServiceCommandHandler : IRequestHandler<
        UpdateByUserIdFindeksCreditRateFromServiceCommand,
        UpdatedFindeksCreditRateDto>
    {
        private readonly IFindeksCreditRateRepository _findeksCreditRateRepository;
        private readonly IFindeksService _findeksCreditRateService;
        private readonly IMapper _mapper;
        private readonly ICustomerService _customerService;

        public UpdateFindeksCreditRateFromServiceCommandHandler(
            IFindeksCreditRateRepository findeksCreditRateRepository,
            IFindeksService findeksCreditRateService, IMapper mapper, ICustomerService customerService)
        {
            _findeksCreditRateRepository = findeksCreditRateRepository;
            _findeksCreditRateService = findeksCreditRateService;
            _mapper = mapper;
            _customerService = customerService;
        }

        public async Task<UpdatedFindeksCreditRateDto> Handle(UpdateByUserIdFindeksCreditRateFromServiceCommand request,
                                                              CancellationToken cancellationToken)
        {
            Customer customer = await _customerService.GetByUserId(request.UserId);
            FindeksCreditRate? findeksCreditRate =
                await _findeksCreditRateRepository.GetAsync(f => f.CustomerId == customer.Id);

            findeksCreditRate.Score = _findeksCreditRateService.GetScore(request.IdentityNumber);

            FindeksCreditRate updatedFindeksCreditRate =
                await _findeksCreditRateRepository.UpdateAsync(findeksCreditRate);

            UpdatedFindeksCreditRateDto updatedFindeksCreditRateDto =
                _mapper.Map<UpdatedFindeksCreditRateDto>(updatedFindeksCreditRate);
            return updatedFindeksCreditRateDto;
        }
    }
}