using Application.Features.Speeds.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using static Application.Features.Speeds.Constants.OperationClaims;
using static Domain.Constants.OperationClaims;

namespace Application.Features.Speeds.Commands.DeleteSpeed;

public class DeleteSpeedCommand : IRequest<DeletedSpeedDto>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Admin, SpeedDelete };

    public class DeleteSpeedCommandHandler : IRequestHandler<DeleteSpeedCommand, DeletedSpeedDto>
    {
        private readonly ISpeedRepository _speedRepository;
        private readonly IMapper _mapper;

        public DeleteSpeedCommandHandler(ISpeedRepository speedRepository, IMapper mapper)
        {
            _speedRepository = speedRepository;
            _mapper = mapper;
        }

        public async Task<DeletedSpeedDto> Handle(DeleteSpeedCommand request, CancellationToken cancellationToken)
        {
            Speed mappedSpeed = _mapper.Map<Speed>(request);
            Speed updatedSpeed = await _speedRepository.DeleteAsync(mappedSpeed);
            DeletedSpeedDto deletedSpeedDto = _mapper.Map<DeletedSpeedDto>(updatedSpeed);
            return deletedSpeedDto;
        }
    }
}