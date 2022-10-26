using Application.Features.Colors.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using static Application.Features.Colors.Constants.OperationClaims;
using static Domain.Constants.OperationClaims;

namespace Application.Features.Colors.Commands.DeleteColor;

public class DeleteColorCommand : IRequest<DeletedColorDto>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Admin, ColorAdmin, ColorWrite, ColorDelete };

    public class DeleteColorCommandHandler : IRequestHandler<DeleteColorCommand, DeletedColorDto>
    {
        private readonly IColorRepository _colorRepository;
        private readonly IMapper _mapper;

        public DeleteColorCommandHandler(IColorRepository colorRepository, IMapper mapper)
        {
            _colorRepository = colorRepository;
            _mapper = mapper;
        }

        public async Task<DeletedColorDto> Handle(DeleteColorCommand request, CancellationToken cancellationToken)
        {
            Color mappedColor = _mapper.Map<Color>(request);
            Color updatedColor = await _colorRepository.DeleteAsync(mappedColor);
            DeletedColorDto deletedColorDto = _mapper.Map<DeletedColorDto>(updatedColor);
            return deletedColorDto;
        }
    }
}