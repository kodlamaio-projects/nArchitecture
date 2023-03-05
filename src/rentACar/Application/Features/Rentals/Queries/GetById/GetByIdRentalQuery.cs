using Application.Features.Rentals.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Rentals.Queries.GetById;

public class GetByIdRentalQuery : IRequest<GetByIdRentalResponse>
{
    public int Id { get; set; }

    public class GetByIdRentalQueryHandler : IRequestHandler<GetByIdRentalQuery, GetByIdRentalResponse>
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IMapper _mapper;
        private readonly RentalBusinessRules _rentalBusinessRules;

        public GetByIdRentalQueryHandler(IRentalRepository rentalRepository, RentalBusinessRules rentalBusinessRules, IMapper mapper)
        {
            _rentalRepository = rentalRepository;
            _rentalBusinessRules = rentalBusinessRules;
            _mapper = mapper;
        }

        public async Task<GetByIdRentalResponse> Handle(GetByIdRentalQuery request, CancellationToken cancellationToken)
        {
            await _rentalBusinessRules.RentalIdShouldExistWhenSelected(request.Id);

            Rental? rental = await _rentalRepository.GetAsync(b => b.Id == request.Id);
            GetByIdRentalResponse rentalDto = _mapper.Map<GetByIdRentalResponse>(rental);
            return rentalDto;
        }
    }
}
