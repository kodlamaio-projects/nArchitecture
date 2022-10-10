using Application.Features.Colors.Dtos;
using Application.Features.Colors.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using static Application.Features.Colors.Constants.OperationClaims;
using static Domain.Constants.OperationClaims;

namespace Application.Features.Colors.Commands.CreateColor;

public class CreateColorCommand : IRequest<CreatedColorDto>, ISecuredRequest
{
    public string Name { get; set; }

    public string[] Roles => new[] { Admin, ColorAdd };

    public class CreateColorCommandHandler : IRequestHandler<CreateColorCommand, CreatedColorDto>
    {
        private readonly IColorRepository _colorRepository;
        private readonly IMapper _mapper;
        private readonly ColorBusinessRules _colorBusinessRules;

        public CreateColorCommandHandler(IColorRepository colorRepository, IMapper mapper,
                                         ColorBusinessRules colorBusinessRules)
        {
            _colorRepository = colorRepository;
            _mapper = mapper;
            _colorBusinessRules = colorBusinessRules;
        }

        public async Task<CreatedColorDto> Handle(CreateColorCommand request, CancellationToken cancellationToken)
        {
            await _colorBusinessRules.ColorNameCanNotBeDuplicatedWhenInserted(request.Name);

            Color mappedColor = _mapper.Map<Color>(request);
            Color createdColor = await _colorRepository.AddAsync(mappedColor);
            CreatedColorDto createdColorDto = _mapper.Map<CreatedColorDto>(createdColor);
            return createdColorDto;
        }
    }
}