using Application.Services.FindeksService;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.FindeksCreditRates.Commands.UpdateFromService;

public class UpdateFindeksCreditRateFromServiceCommand : IRequest<UpdateFindeksCreditRateFromServiceResponse>
{
    public int Id { get; set; }
    public string IdentityNumber { get; set; }

    public class UpdateFindeksCreditRateFromServiceCommandHandler
        : IRequestHandler<UpdateFindeksCreditRateFromServiceCommand, UpdateFindeksCreditRateFromServiceResponse>
    {
        private readonly IFindeksCreditRateRepository _findeksCreditRateRepository;
        private readonly IFindeksService _findeksCreditRateService;
        private readonly IMapper _mapper;

        public UpdateFindeksCreditRateFromServiceCommandHandler(
            IFindeksCreditRateRepository findeksCreditRateRepository,
            IFindeksService findeksCreditRateService,
            IMapper mapper
        )
        {
            _findeksCreditRateRepository = findeksCreditRateRepository;
            _findeksCreditRateService = findeksCreditRateService;
            _mapper = mapper;
        }

        public async Task<UpdateFindeksCreditRateFromServiceResponse> Handle(
            UpdateFindeksCreditRateFromServiceCommand request,
            CancellationToken cancellationToken
        )
        {
            FindeksCreditRate? findeksCreditRate = await _findeksCreditRateRepository.GetAsync(f => f.Id == request.Id);
            findeksCreditRate.Score = _findeksCreditRateService.GetScore(request.IdentityNumber);
            FindeksCreditRate updatedFindeksCreditRate = await _findeksCreditRateRepository.UpdateAsync(findeksCreditRate);
            UpdateFindeksCreditRateFromServiceResponse? updatedFindeksCreditRateDto =
                _mapper.Map<UpdateFindeksCreditRateFromServiceResponse>(updatedFindeksCreditRate);
            return updatedFindeksCreditRateDto;
        }
    }
}
