using Application.Features.FindeksCreditRates.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;

namespace Application.Features.FindeksCreditRates.Queries.GetListFindeksCreditRate;

public class GetListFindeksCreditRateQuery : IRequest<FindeksCreditRateListModel>
{
    public PageRequest PageRequest { get; set; }

    public class
        GetListFindeksCreditRateQueryHandler : IRequestHandler<GetListFindeksCreditRateQuery,
            FindeksCreditRateListModel>
    {
        private readonly IFindeksCreditRateRepository _findeksCreditRateRepository;
        private readonly IMapper _mapper;

        public GetListFindeksCreditRateQueryHandler(IFindeksCreditRateRepository findeksCreditRateRepository,
                                                    IMapper mapper)
        {
            _findeksCreditRateRepository = findeksCreditRateRepository;
            _mapper = mapper;
        }

        public async Task<FindeksCreditRateListModel> Handle(GetListFindeksCreditRateQuery request,
                                                             CancellationToken cancellationToken)
        {
            IPaginate<FindeksCreditRate> findeksCreditRates = await _findeksCreditRateRepository.GetListAsync(
                                                                  index: request.PageRequest.Page,
                                                                  size: request.PageRequest.PageSize);
            FindeksCreditRateListModel mappedFindeksCreditRateListModel =
                _mapper.Map<FindeksCreditRateListModel>(findeksCreditRates);
            return mappedFindeksCreditRateListModel;
        }
    }
}