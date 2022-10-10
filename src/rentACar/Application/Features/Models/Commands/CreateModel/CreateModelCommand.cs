using Application.Features.Models.Dtos;
using Application.Features.Models.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Domain.Entities;
using MediatR;
using static Application.Features.Models.Constants.OperationClaims;
using static Domain.Constants.OperationClaims;

namespace Application.Features.Models.Commands.CreateModel;

public class CreateModelCommand : IRequest<CreatedModelDto>, ISecuredRequest, ICacheRemoverRequest
{
    public string Name { get; set; }
    public decimal DailyPrice { get; set; }
    public int BrandId { get; set; }
    public int TransmissionId { get; set; }
    public int FuelId { get; set; }
    public string ImageUrl { get; set; }

    public bool BypassCache { get; }
    public string CacheKey => "models-list";
    public string[] Roles => new[] { Admin, ModelsAdd };

    public class CreateModelCommandHandler : IRequestHandler<CreateModelCommand, CreatedModelDto>
    {
        private readonly IModelRepository _modelRepository;
        private readonly IMapper _mapper;
        private readonly ModelBusinessRules _modelBusinessRules;

        public CreateModelCommandHandler(IModelRepository modelRepository, IMapper mapper,
                                         ModelBusinessRules modelBusinessRules)
        {
            _modelRepository = modelRepository;
            _mapper = mapper;
            _modelBusinessRules = modelBusinessRules;
        }

        public async Task<CreatedModelDto> Handle(CreateModelCommand request, CancellationToken cancellationToken)
        {
            Model mappedModel = _mapper.Map<Model>(request);
            Model createdModel = await _modelRepository.AddAsync(mappedModel);
            CreatedModelDto createdModelDto = _mapper.Map<CreatedModelDto>(createdModel);
            return createdModelDto;
        }
    }
}