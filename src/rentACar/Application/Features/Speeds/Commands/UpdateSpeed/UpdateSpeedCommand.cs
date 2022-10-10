using Application.Features.Speeds.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using static Application.Features.Speeds.Constants.OperationClaims;
using static Domain.Constants.OperationClaims;

namespace Application.Features.Speeds.Commands.UpdateSpeed;

public class UpdateSpeedCommand : IRequest<UpdatedSpeedDto>, ISecuredRequest
{
    public int Id { get; set; }
    public string Name { get; set; }

    public string[] Roles => new[] { Admin, SpeedUpdate };

    public class UpdateSpeedCommandHandler : IRequestHandler<UpdateSpeedCommand, UpdatedSpeedDto>
    {
        private ISpeedRepository _speedRepository { get; }
        private IMapper _mapper { get; }

        public UpdateSpeedCommandHandler(ISpeedRepository speedRepository, IMapper mapper)
        {
            _speedRepository = speedRepository;
            _mapper = mapper;
        }

        public async Task<UpdatedSpeedDto> Handle(UpdateSpeedCommand request, CancellationToken cancellationToken)
        {
            Speed mappedSpeed = _mapper.Map<Speed>(request);
            Speed updatedSpeed = await _speedRepository.UpdateAsync(mappedSpeed);
            UpdatedSpeedDto updatedSpeedDto = _mapper.Map<UpdatedSpeedDto>(updatedSpeed);
            return updatedSpeedDto;
        }
    }
}