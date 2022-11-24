using Application.Features.AdditionalServices.Dtos;
using Application.Features.AdditionalServices.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using static Application.Features.AdditionalServices.Constants.OperationClaims;
using static Domain.Constants.OperationClaims;

namespace Application.Features.AdditionalServices.Commands.UpdateAdditionalService;

public class UpdateAdditionalServiceCommand : IRequest<UpdatedAdditionalServiceDto>, ISecuredRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal DailyPrice { get; set; }

    public string[] Roles => new[] { Admin, AdditionalServiceAdmin, AdditionalServiceWrite, AdditionalServiceUpdate };

    public class
        UpdateAdditionalServiceCommandHandler : IRequestHandler<UpdateAdditionalServiceCommand,
            UpdatedAdditionalServiceDto>
    {
        private readonly IAdditionalServiceRepository _additionalServiceRepository;
        private readonly IMapper _mapper;
        private readonly AdditionalServiceBusinessRules _additionalServiceBusinessRules;

        public UpdateAdditionalServiceCommandHandler(IAdditionalServiceRepository additionalServiceRepository,
                                                     IMapper mapper,
                                                     AdditionalServiceBusinessRules additionalServiceBusinessRules)
        {
            _additionalServiceRepository = additionalServiceRepository;
            _mapper = mapper;
            _additionalServiceBusinessRules = additionalServiceBusinessRules;
        }

        public async Task<UpdatedAdditionalServiceDto> Handle(UpdateAdditionalServiceCommand request,
                                                              CancellationToken cancellationToken)
        {
            AdditionalService mappedAdditionalService = _mapper.Map<AdditionalService>(request);
            AdditionalService updatedAdditionalService =
                await _additionalServiceRepository.UpdateAsync(mappedAdditionalService);
            UpdatedAdditionalServiceDto updatedAdditionalServiceDto =
                _mapper.Map<UpdatedAdditionalServiceDto>(updatedAdditionalService);
            return updatedAdditionalServiceDto;
        }
    }
}