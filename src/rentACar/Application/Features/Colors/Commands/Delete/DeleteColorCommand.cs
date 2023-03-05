using Application.Features.Colors.Constants;
using Application.Features.Colors.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using static Application.Features.Colors.Constants.ColorsOperationClaims;

namespace Application.Features.Colors.Commands.Delete;

public class DeleteColorCommand : IRequest<DeletedColorResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Domain.Constants.OperationClaims.Admin, Admin, Write, ColorsOperationClaims.Delete };

    public class DeleteColorCommandHandler : IRequestHandler<DeleteColorCommand, DeletedColorResponse>
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

        public async Task<DeletedColorResponse> Handle(DeleteColorCommand request, CancellationToken cancellationToken)
        {
            await _colorBusinessRules.ColorIdShouldExistWhenSelected(request.Id);
            Color mappedColor = _mapper.Map<Color>(request);
            Color updatedColor = await _colorRepository.DeleteAsync(mappedColor);
            DeletedColorResponse deletedColorDto = _mapper.Map<DeletedColorResponse>(updatedColor);
            return deletedColorDto;
        }
    }
}
