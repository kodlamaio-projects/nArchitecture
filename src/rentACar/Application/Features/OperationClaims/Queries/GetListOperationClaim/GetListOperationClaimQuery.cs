using Application.Features.OperationClaims.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Domain.Security.Entities;
using Core.Persistence.Paging;
using MediatR;

namespace Application.Features.OperationClaims.Queries.GetListOperationClaim;

public class GetListOperationClaimQuery : IRequest<OperationClaimListModel>
{
    public PageRequest PageRequest { get; set; }

    public class
        GetListOperationClaimQueryHandler : IRequestHandler<GetListOperationClaimQuery, OperationClaimListModel>
    {
        private readonly IOperationClaimRepository _operationClaimRepository;
        private readonly IMapper _mapper;

        public GetListOperationClaimQueryHandler(IOperationClaimRepository operationClaimRepository, IMapper mapper)
        {
            _operationClaimRepository = operationClaimRepository;
            _mapper = mapper;
        }

        public async Task<OperationClaimListModel> Handle(GetListOperationClaimQuery request,
                                                          CancellationToken cancellationToken)
        {
            IPaginate<OperationClaim> operationClaims = await _operationClaimRepository.GetListAsync(
                                                            index: request.PageRequest.Page,
                                                            size: request.PageRequest.PageSize);
            OperationClaimListModel mappedOperationClaimListModel =
                _mapper.Map<OperationClaimListModel>(operationClaims);
            return mappedOperationClaimListModel;
        }
    }
}