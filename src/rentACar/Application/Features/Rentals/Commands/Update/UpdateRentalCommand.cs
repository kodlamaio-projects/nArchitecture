using Application.Features.Rentals.Constants;
using Application.Features.Rentals.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using static Application.Features.Rentals.Constants.RentalsOperationClaims;

namespace Application.Features.Rentals.Commands.Update;

public class UpdateRentalCommand : IRequest<UpdatedRentalResponse>, ISecuredRequest
{
    public int Id { get; set; }
    public int CarId { get; set; }
    public int CustomerId { get; set; }
    public int RentStartRentalBranchId { get; set; }
    public int? RentEndRentalBranchId { get; set; }
    public DateTime RentStartDate { get; set; }
    public DateTime RentEndDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public int RentStartKilometer { get; set; }
    public int? RentEndKilometer { get; set; }

    public string[] Roles => new[] { Domain.Constants.OperationClaims.Admin, Admin, Write, RentalsOperationClaims.Update };

    public class UpdateRentalCommandHandler : IRequestHandler<UpdateRentalCommand, UpdatedRentalResponse>
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IMapper _mapper;
        private readonly RentalBusinessRules _rentalBusinessRules;

        public UpdateRentalCommandHandler(IRentalRepository rentalRepository, IMapper mapper, RentalBusinessRules rentalBusinessRules)
        {
            _rentalRepository = rentalRepository;
            _mapper = mapper;
            _rentalBusinessRules = rentalBusinessRules;
        }

        public async Task<UpdatedRentalResponse> Handle(UpdateRentalCommand request, CancellationToken cancellationToken)
        {
            await _rentalBusinessRules.RentalCanNotBeUpdateWhenThereIsARentedCarInDate(
                request.Id,
                request.CarId,
                request.RentStartDate,
                request.RentEndDate
            );

            Rental mappedRental = _mapper.Map<Rental>(request);
            Rental updatedRental = await _rentalRepository.UpdateAsync(mappedRental);
            UpdatedRentalResponse? updatedRentalDto = _mapper.Map<UpdatedRentalResponse>(updatedRental);
            return updatedRentalDto;
        }
    }
}
