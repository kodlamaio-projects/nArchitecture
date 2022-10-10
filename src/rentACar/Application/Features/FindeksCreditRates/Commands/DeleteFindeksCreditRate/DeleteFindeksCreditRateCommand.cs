using Application.Features.FindeksCreditRates.Dtos;
using Application.Features.FindeksCreditRates.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using static Application.Features.FindeksCreditRates.Constants.OperationClaims;
using static Domain.Constants.OperationClaims;

namespace Application.Features.FindeksCreditRates.Commands.DeleteFindeksCreditRate;

public class DeleteFindeksCreditRateCommand : IRequest<DeletedFindeksCreditRateDto>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Admin, FindeksCreditScoreDelete };

    public class
        DeleteFindeksCreditRateCommandHandler : IRequestHandler<DeleteFindeksCreditRateCommand,
            DeletedFindeksCreditRateDto>
    {
        private readonly IFindeksCreditRateRepository _findeksCreditRateRepository;
        private readonly IMapper _mapper;
        private readonly FindeksCreditRateBusinessRules _findeksCreditRateBusinessRules;

        public DeleteFindeksCreditRateCommandHandler(IFindeksCreditRateRepository findeksCreditRateRepository,
                                                     IMapper mapper,
                                                     FindeksCreditRateBusinessRules findeksCreditRateBusinessRules)
        {
            _findeksCreditRateRepository = findeksCreditRateRepository;
            _mapper = mapper;
            _findeksCreditRateBusinessRules = findeksCreditRateBusinessRules;
        }

        public async Task<DeletedFindeksCreditRateDto> Handle(DeleteFindeksCreditRateCommand request,
                                                              CancellationToken cancellationToken)
        {
            await _findeksCreditRateBusinessRules.FindeksCreditRateIdShouldExistWhenSelected(request.Id);

            FindeksCreditRate mappedFindeksCreditRate = _mapper.Map<FindeksCreditRate>(request);
            FindeksCreditRate deletedFindeksCreditRate =
                await _findeksCreditRateRepository.DeleteAsync(mappedFindeksCreditRate);
            DeletedFindeksCreditRateDto deletedFindeksCreditRateDto =
                _mapper.Map<DeletedFindeksCreditRateDto>(deletedFindeksCreditRate);
            return deletedFindeksCreditRateDto;
        }
    }
}