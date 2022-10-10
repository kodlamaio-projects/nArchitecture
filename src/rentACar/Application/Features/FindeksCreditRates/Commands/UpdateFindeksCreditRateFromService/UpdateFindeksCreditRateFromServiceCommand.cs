using Application.Features.FindeksCreditRates.Dtos;
using Application.Services.FindeksService;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.FindeksCreditRates.Commands.UpdateFindeksCreditRateFromService;

public class UpdateFindeksCreditRateFromServiceCommand : IRequest<UpdatedFindeksCreditRateDto>
{
    public int Id { get; set; }
    public string IdentityNumber { get; set; }

    public class UpdateFindeksCreditRateFromServiceCommandHandler : IRequestHandler<
        UpdateFindeksCreditRateFromServiceCommand,
        UpdatedFindeksCreditRateDto>
    {
        private readonly IFindeksCreditRateRepository _findeksCreditRateRepository;
        private readonly IFindeksService _findeksCreditRateService;
        private readonly IMapper _mapper;

        public UpdateFindeksCreditRateFromServiceCommandHandler(
            IFindeksCreditRateRepository findeksCreditRateRepository,
            IFindeksService findeksCreditRateService, IMapper mapper)
        {
            _findeksCreditRateRepository = findeksCreditRateRepository;
            _findeksCreditRateService = findeksCreditRateService;
            _mapper = mapper;
        }

        public async Task<UpdatedFindeksCreditRateDto> Handle(UpdateFindeksCreditRateFromServiceCommand request,
                                                              CancellationToken cancellationToken)
        {
            FindeksCreditRate? findeksCreditRate = await _findeksCreditRateRepository.GetAsync(f => f.Id == request.Id);
            findeksCreditRate.Score = _findeksCreditRateService.GetScore(request.IdentityNumber);
            FindeksCreditRate updatedFindeksCreditRate =
                await _findeksCreditRateRepository.UpdateAsync(findeksCreditRate);
            UpdatedFindeksCreditRateDto updatedFindeksCreditRateDto =
                _mapper.Map<UpdatedFindeksCreditRateDto>(updatedFindeksCreditRate);
            return updatedFindeksCreditRateDto;
        }
    }
}