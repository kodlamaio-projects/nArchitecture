using Application.Features.AdditionalServices.Constants;
using Application.Features.AdditionalServices.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using static Application.Features.AdditionalServices.Constants.AdditionalServicesOperationClaims;

namespace Application.Features.AdditionalServices.Commands.Delete;

public class DeleteAdditionalServiceCommand : IRequest<DeletedAdditionalServiceResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Admin, Write, AdditionalServicesOperationClaims.Delete };

    public class DeleteAdditionalServiceCommandHandler : IRequestHandler<DeleteAdditionalServiceCommand, DeletedAdditionalServiceResponse>
    {
        private readonly IAdditionalServiceRepository _additionalServiceRepository;
        private readonly IMapper _mapper;
        private readonly AdditionalServiceBusinessRules _additionalServiceBusinessRules;

        public DeleteAdditionalServiceCommandHandler(
            IAdditionalServiceRepository additionalServiceRepository,
            IMapper mapper,
            AdditionalServiceBusinessRules additionalServiceBusinessRules
        )
        {
            _additionalServiceRepository = additionalServiceRepository;
            _mapper = mapper;
            _additionalServiceBusinessRules = additionalServiceBusinessRules;
        }

        public async Task<DeletedAdditionalServiceResponse> Handle(
            DeleteAdditionalServiceCommand request,
            CancellationToken cancellationToken
        )
        {
            await _additionalServiceBusinessRules.AdditionalServiceIdShouldExistWhenSelected(request.Id);

            AdditionalService mappedAdditionalService = _mapper.Map<AdditionalService>(request);
            AdditionalService deletedAdditionalService = await _additionalServiceRepository.DeleteAsync(mappedAdditionalService);
            DeletedAdditionalServiceResponse deletedAdditionalServiceResponse = _mapper.Map<DeletedAdditionalServiceResponse>(
                deletedAdditionalService
            );
            return deletedAdditionalServiceResponse;
        }
    }
}
