using Application.Features.Colors.Dtos;
using Application.Features.Colors.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Colors.Queries.GetByIdColor;

public class GetByIdSpeedQuery : IRequest<SpeedDto>
{
    public int Id { get; set; }

    public class GetByIdColorQueryHandler : IRequestHandler<GetByIdSpeedQuery, SpeedDto>
    {
        private readonly IColorRepository _colorRepository;
        private readonly IMapper _mapper;
        private readonly SpeedBusinessRules _colorBusinessRules;

        public GetByIdColorQueryHandler(IColorRepository colorRepository, SpeedBusinessRules colorBusinessRules,
                                        IMapper mapper)
        {
            _colorRepository = colorRepository;
            _colorBusinessRules = colorBusinessRules;
            _mapper = mapper;
        }


        public async Task<SpeedDto> Handle(GetByIdSpeedQuery request, CancellationToken cancellationToken)
        {
            await _colorBusinessRules.ColorIdShouldExistWhenSelected(request.Id);

            Color? color = await _colorRepository.GetAsync(c => c.Id == request.Id);
            SpeedDto colorDto = _mapper.Map<SpeedDto>(color);
            return colorDto;
        }
    }
}