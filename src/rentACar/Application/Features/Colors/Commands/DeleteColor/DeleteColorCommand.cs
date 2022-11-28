using Application.Features.Colors.Dtos;
using Application.Features.Colors.Rules;
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
        private readonly ColorBusinessRules _colorBusinessRules;

        public DeleteColorCommandHandler(IColorRepository colorRepository, IMapper mapper, ColorBusinessRules colorBusinessRules)
        {
            _colorRepository = colorRepository;
            _mapper = mapper;
            _colorBusinessRules = colorBusinessRules;
        }

        public async Task<DeletedColorDto> Handle(DeleteColorCommand request, CancellationToken cancellationToken)
        {
            await _colorBusinessRules.ColorIdShouldExistWhenSelected(request.Id);
            Color mappedColor = _mapper.Map<Color>(request);
            Color updatedColor = await _colorRepository.DeleteAsync(mappedColor);
            DeletedColorDto deletedColorDto = _mapper.Map<DeletedColorDto>(updatedColor);
            return deletedColorDto;
        }
    }
}