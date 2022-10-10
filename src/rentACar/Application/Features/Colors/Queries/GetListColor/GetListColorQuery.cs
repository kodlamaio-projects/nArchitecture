using Application.Features.Colors.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;

namespace Application.Features.Colors.Queries.GetListColor;

public class GetListColorQuery : IRequest<ColorListModel>
{
    public PageRequest PageRequest { get; set; }

    public class GetListColorQueryHandler : IRequestHandler<GetListColorQuery, ColorListModel>
    {
        private readonly IColorRepository _colorRepository;
        private readonly IMapper _mapper;

        public GetListColorQueryHandler(IColorRepository colorRepository, IMapper mapper)
        {
            _colorRepository = colorRepository;
            _mapper = mapper;
        }

        public async Task<ColorListModel> Handle(GetListColorQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Color> colors = await _colorRepository.GetListAsync(index: request.PageRequest.Page,
                                                                          size: request.PageRequest.PageSize);
            ColorListModel mappedColorListModel = _mapper.Map<ColorListModel>(colors);
            return mappedColorListModel;
        }
    }
}