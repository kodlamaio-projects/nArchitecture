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

public class CreateSpeedCommand : IRequest<CreatedSpeedDto>, ISecuredRequest
{
    public string Name { get; set; }

    public string[] Roles => new[] { Admin, ColorAdd };

    public class CreateColorCommandHandler : IRequestHandler<CreateSpeedCommand, CreatedSpeedDto>
    {
        private readonly IColorRepository _colorRepository;
        private readonly IMapper _mapper;
        private readonly SpeedBusinessRules _colorBusinessRules;

        public CreateColorCommandHandler(IColorRepository colorRepository, IMapper mapper,
                                         SpeedBusinessRules colorBusinessRules)
        {
            _colorRepository = colorRepository;
            _mapper = mapper;
            _colorBusinessRules = colorBusinessRules;
        }

        public async Task<CreatedSpeedDto> Handle(CreateSpeedCommand request, CancellationToken cancellationToken)
        {
            await _colorBusinessRules.ColorNameCanNotBeDuplicatedWhenInserted(request.Name);

            Color mappedColor = _mapper.Map<Color>(request);
            Color createdColor = await _colorRepository.AddAsync(mappedColor);
            CreatedSpeedDto createdColorDto = _mapper.Map<CreatedSpeedDto>(createdColor);
            return createdColorDto;
        }
    }
}