using Application.Features.Speeds.Dtos;
using Application.Features.Speeds.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Speeds.Queries.GetByIdSpeed;

public class GetByIdSpeedQuery : IRequest<SpeedDto>
{
    public int Id { get; set; }

    public class GetByIdSpeedQueryHandler : IRequestHandler<GetByIdSpeedQuery, SpeedDto>
    {
        private readonly ISpeedRepository _speedRepository;
        private readonly IMapper _mapper;
        private readonly SpeedBusinessRules _speedBusinessRules;

        public GetByIdSpeedQueryHandler(ISpeedRepository speedRepository, SpeedBusinessRules speedBusinessRules,
                                        IMapper mapper)
        {
            _speedRepository = speedRepository;
            _speedBusinessRules = speedBusinessRules;
            _mapper = mapper;
        }


        public async Task<SpeedDto> Handle(GetByIdSpeedQuery request, CancellationToken cancellationToken)
        {
            await _speedBusinessRules.SpeedIdShouldExistWhenSelected(request.Id);

            Speed? speed = await _speedRepository.GetAsync(c => c.Id == request.Id);
            SpeedDto speedDto = _mapper.Map<SpeedDto>(speed);
            return speedDto;
        }
    }
}