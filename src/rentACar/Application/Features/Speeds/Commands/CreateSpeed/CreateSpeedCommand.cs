using AutoMapper;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Domain.Constants.OperationClaims;

namespace Application.Features.Speeds.Commands.CreateSpeed;

public class CreateSpeedCommand : IRequest<CreatedSpeedDto>, ISecuredRequest
{
    public string Name { get; set; }

    public string[] Roles => new[] { Admin, SpeedAdd };

    public class CreateSpeedCommandHandler : IRequestHandler<CreateSpeedCommand, CreatedSpeedDto>
    {
        private readonly ISpeedRepository _speedRepository;
        private readonly IMapper _mapper;
        private readonly SpeedBusinessRules _speedBusinessRules;

        public CreateSpeedCommandHandler(ISpeedRepository speedRepository, IMapper mapper,
                                         SpeedBusinessRules speedBusinessRules)
        {
            _speedRepository = speedRepository;
            _mapper = mapper;
            _speedBusinessRules = speedBusinessRules;
        }

        public async Task<CreatedSpeedDto> Handle(CreateSpeedCommand request, CancellationToken cancellationToken)
        {
            await _speedBusinessRules.SpeedNameCanNotBeDuplicatedWhenInserted(request.Name);

            Speed mappedSpeed = _mapper.Map<Speed>(request);
            Speed createdSpeed = await _speedRepository.AddAsync(mappedSpeed);
            CreatedSpeedDto createdSpeedDto = _mapper.Map<CreatedSpeedDto>(createdSpeed);
            return createdSpeedDto;
        }
    }
}