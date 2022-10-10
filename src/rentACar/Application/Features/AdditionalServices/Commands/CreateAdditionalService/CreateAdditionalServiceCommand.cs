using Application.Features.AdditionalServices.Dtos;
using Application.Features.AdditionalServices.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using static Application.Features.AdditionalServices.Constants.OperationClaims;
using static Domain.Constants.OperationClaims;

namespace Application.Features.AdditionalServices.Commands.CreateAdditionalService;

public class CreateAdditionalServiceCommand : IRequest<CreatedAdditionalServiceDto>, ISecuredRequest
{
    public string Name { get; set; }
    public decimal DailyPrice { get; set; }

    public string[] Roles => new[] { Admin, AdditionalServiceAdd };

    public class
        CreateAdditionalServiceCommandHandler : IRequestHandler<CreateAdditionalServiceCommand,
            CreatedAdditionalServiceDto>
    {
        private readonly IAdditionalServiceRepository _additionalServiceRepository;
        private readonly IMapper _mapper;
        private readonly AdditionalServiceBusinessRules _additionalServiceBusinessRules;

        public CreateAdditionalServiceCommandHandler(IAdditionalServiceRepository additionalServiceRepository,
                                                     IMapper mapper,
                                                     AdditionalServiceBusinessRules additionalServiceBusinessRules)
        {
            _additionalServiceRepository = additionalServiceRepository;
            _mapper = mapper;
            _additionalServiceBusinessRules = additionalServiceBusinessRules;
        }

        public async Task<CreatedAdditionalServiceDto> Handle(CreateAdditionalServiceCommand request,
                                                              CancellationToken cancellationToken)
        {
            await _additionalServiceBusinessRules.AdditionalServiceNameCanNotBeDuplicatedWhenInserted(request.Name);

            AdditionalService mappedAdditionalService = _mapper.Map<AdditionalService>(request);
            AdditionalService createdAdditionalService =
                await _additionalServiceRepository.AddAsync(mappedAdditionalService);
            CreatedAdditionalServiceDto createdAdditionalServiceDto =
                _mapper.Map<CreatedAdditionalServiceDto>(createdAdditionalService);
            return createdAdditionalServiceDto;
        }
    }
}