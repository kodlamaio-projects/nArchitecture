using Application.Features.Colors.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using static Application.Features.Colors.Constants.OperationClaims;
using static Domain.Constants.OperationClaims;

namespace Application.Features.Colors.Commands.DeleteColor;

public class DeleteColorCommand : IRequest<DeletedSpeedDto>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Admin, ColorDelete };

    public class DeleteColorCommandHandler : IRequestHandler<DeleteColorCommand, DeletedSpeedDto>
    {
        private readonly IColorRepository _colorRepository;
        private readonly IMapper _mapper;

        public DeleteColorCommandHandler(IColorRepository colorRepository, IMapper mapper)
        {
            _colorRepository = colorRepository;
            _mapper = mapper;
        }

        public async Task<DeletedSpeedDto> Handle(DeleteColorCommand request, CancellationToken cancellationToken)
        {
            Color mappedColor = _mapper.Map<Color>(request);
            Color updatedColor = await _colorRepository.DeleteAsync(mappedColor);
            DeletedSpeedDto deletedColorDto = _mapper.Map<DeletedSpeedDto>(updatedColor);
            return deletedColorDto;
        }
    }
}