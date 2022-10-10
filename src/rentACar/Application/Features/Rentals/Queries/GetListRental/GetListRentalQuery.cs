using Application.Features.Rentals.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Rentals.Queries.GetListRental;

public class GetListRentalQuery : IRequest<RentalListModel>
{
    public PageRequest PageRequest { get; set; }

    public class GetListRentalQueryHandler : IRequestHandler<GetListRentalQuery, RentalListModel>
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IMapper _mapper;

        public GetListRentalQueryHandler(IRentalRepository rentalRepository, IMapper mapper)
        {
            _rentalRepository = rentalRepository;
            _mapper = mapper;
        }

        public async Task<RentalListModel> Handle(GetListRentalQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Rental> rentals = await _rentalRepository.GetListAsync(include: r =>
                                                                                 r.Include(r => r.Car)
                                                                                     .Include(r => r.Car.Model)
                                                                                     .Include(r => r.Car.Model.Brand)
                                                                                     .Include(r => r.Car.Color)
                                                                                     .Include(r => r.Customer)
                                                                                     .Include(r => r.Customer
                                                                                         .IndividualCustomer)
                                                                                     .Include(r => r.Customer
                                                                                         .CorporateCustomer),
                                                                             index: request.PageRequest.Page,
                                                                             size: request.PageRequest.PageSize);
            RentalListModel mappedRentalListModel = _mapper.Map<RentalListModel>(rentals);
            return mappedRentalListModel;
        }
    }
}