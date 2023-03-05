using Application.Features.FindeksCreditRates.Constants;
using Application.Features.FindeksCreditRates.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;

namespace Application.Features.FindeksCreditRates.Commands.Delete;

public class DeleteFindeksCreditRateCommand : IRequest<DeletedFindeksCreditRateResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { FindeksCreditRatesOperationClaims.Admin, FindeksCreditRatesOperationClaims.Delete };

    public class DeleteFindeksCreditRateCommandHandler : IRequestHandler<DeleteFindeksCreditRateCommand, DeletedFindeksCreditRateResponse>
    {
        private readonly IFindeksCreditRateRepository _findeksCreditRateRepository;
        private readonly IMapper _mapper;
        private readonly FindeksCreditRateBusinessRules _findeksCreditRateBusinessRules;

        public DeleteFindeksCreditRateCommandHandler(
            IFindeksCreditRateRepository findeksCreditRateRepository,
            IMapper mapper,
            FindeksCreditRateBusinessRules findeksCreditRateBusinessRules
        )
        {
            _findeksCreditRateRepository = findeksCreditRateRepository;
            _mapper = mapper;
            _findeksCreditRateBusinessRules = findeksCreditRateBusinessRules;
        }

        public async Task<DeletedFindeksCreditRateResponse> Handle(
            DeleteFindeksCreditRateCommand request,
            CancellationToken cancellationToken
        )
        {
            await _findeksCreditRateBusinessRules.FindeksCreditRateIdShouldExistWhenSelected(request.Id);

            FindeksCreditRate mappedFindeksCreditRate = _mapper.Map<FindeksCreditRate>(request);
            FindeksCreditRate deletedFindeksCreditRate = await _findeksCreditRateRepository.DeleteAsync(mappedFindeksCreditRate);
            DeletedFindeksCreditRateResponse deletedFindeksCreditRateDto = _mapper.Map<DeletedFindeksCreditRateResponse>(
                deletedFindeksCreditRate
            );
            return deletedFindeksCreditRateDto;
        }
    }
}
