using Application.Features.Rentals.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using static Application.Features.Rentals.Constants.RentalsOperationClaims;

namespace Application.Features.Rentals.Commands.Delete;

public class DeleteRentalCommand : IRequest<DeletedRentalResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Domain.Constants.OperationClaims.Admin, Admin, Write, RentalsOperationClaims.Delete };

    public class DeleteRentalCommandHandler : IRequestHandler<DeleteRentalCommand, DeletedRentalResponse>
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IMapper _mapper;

        public DeleteRentalCommandHandler(IRentalRepository rentalRepository, IMapper mapper)
        {
            _rentalRepository = rentalRepository;
            _mapper = mapper;
        }

        public async Task<DeletedRentalResponse> Handle(DeleteRentalCommand request, CancellationToken cancellationToken)
        {
            Rental mappedRental = _mapper.Map<Rental>(request);
            Rental deletedRental = await _rentalRepository.DeleteAsync(mappedRental);
            DeletedRentalResponse deletedRentalDto = _mapper.Map<DeletedRentalResponse>(deletedRental);
            return deletedRentalDto;
        }
    }
}
