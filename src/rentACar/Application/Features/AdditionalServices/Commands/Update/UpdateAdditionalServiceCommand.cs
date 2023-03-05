using Application.Features.AdditionalServices.Constants;
using Application.Features.AdditionalServices.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using static Application.Features.AdditionalServices.Constants.AdditionalServicesOperationClaims;

namespace Application.Features.AdditionalServices.Commands.Update;

public class UpdateAdditionalServiceCommand : IRequest<UpdatedAdditionalServiceResponse>, ISecuredRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal DailyPrice { get; set; }

    public string[] Roles => new[] { Domain.Constants.OperationClaims.Admin, Admin, Write, AdditionalServicesOperationClaims.Update };

    public class UpdateAdditionalServiceCommandHandler : IRequestHandler<UpdateAdditionalServiceCommand, UpdatedAdditionalServiceResponse>
    {
        private readonly IAdditionalServiceRepository _additionalServiceRepository;
        private readonly IMapper _mapper;
        private readonly AdditionalServiceBusinessRules _additionalServiceBusinessRules;

        public UpdateAdditionalServiceCommandHandler(
            IAdditionalServiceRepository additionalServiceRepository,
            IMapper mapper,
            AdditionalServiceBusinessRules additionalServiceBusinessRules
        )
        {
            _additionalServiceRepository = additionalServiceRepository;
            _mapper = mapper;
            _additionalServiceBusinessRules = additionalServiceBusinessRules;
        }

        public async Task<UpdatedAdditionalServiceResponse> Handle(
            UpdateAdditionalServiceCommand request,
            CancellationToken cancellationToken
        )
        {
            AdditionalService mappedAdditionalService = _mapper.Map<AdditionalService>(request);
            AdditionalService updatedAdditionalService = await _additionalServiceRepository.UpdateAsync(mappedAdditionalService);
            UpdatedAdditionalServiceResponse updatedAdditionalServiceResponse = _mapper.Map<UpdatedAdditionalServiceResponse>(
                updatedAdditionalService
            );
            return updatedAdditionalServiceResponse;
        }
    }
}
