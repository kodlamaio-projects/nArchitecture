using Application.Features.AdditionalServices.Dtos;
using Application.Features.AdditionalServices.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using static Application.Features.AdditionalServices.Constants.OperationClaims;
using static Domain.Constants.OperationClaims;

namespace Application.Features.AdditionalServices.Commands.DeleteAdditionalService;

public class DeleteAdditionalServiceCommand : IRequest<DeletedAdditionalServiceDto>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Admin, AdditionalServiceDelete };

    public class
        DeleteAdditionalServiceCommandHandler : IRequestHandler<DeleteAdditionalServiceCommand,
            DeletedAdditionalServiceDto>
    {
        private readonly IAdditionalServiceRepository _additionalServiceRepository;
        private readonly IMapper _mapper;
        private readonly AdditionalServiceBusinessRules _additionalServiceBusinessRules;

        public DeleteAdditionalServiceCommandHandler(IAdditionalServiceRepository additionalServiceRepository,
                                                     IMapper mapper,
                                                     AdditionalServiceBusinessRules additionalServiceBusinessRules)
        {
            _additionalServiceRepository = additionalServiceRepository;
            _mapper = mapper;
            _additionalServiceBusinessRules = additionalServiceBusinessRules;
        }

        public async Task<DeletedAdditionalServiceDto> Handle(DeleteAdditionalServiceCommand request,
                                                              CancellationToken cancellationToken)
        {
            await _additionalServiceBusinessRules.AdditionalServiceIdShouldExistWhenSelected(request.Id);

            AdditionalService mappedAdditionalService = _mapper.Map<AdditionalService>(request);
            AdditionalService deletedAdditionalService =
                await _additionalServiceRepository.DeleteAsync(mappedAdditionalService);
            DeletedAdditionalServiceDto deletedAdditionalServiceDto =
                _mapper.Map<DeletedAdditionalServiceDto>(deletedAdditionalService);
            return deletedAdditionalServiceDto;
        }
    }
}