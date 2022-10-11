﻿using Application.Features.Rentals.Constants;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Rentals.Rules;

public class RentalBusinessRules
{
    private readonly IRentalRepository _rentalRepository;

    public RentalBusinessRules(IRentalRepository rentalRepository, ICarRepository carRepository)
    {
        _rentalRepository = rentalRepository;
    }

    public async Task RentalIdShouldExistWhenSelected(int id)
    {
        Rental? result = await _rentalRepository.GetAsync(b => b.Id == id);
        if (result == null) throw new BusinessException(RentalExceptionMessages.RentalNotExistsMessage);
    }

    public async Task RentalCanNotBeUpdateWhenThereIsARentedCarInDate(int id, int carId, DateTime rentStartDate,
                                                                      DateTime rentEndDate)
    {
        IPaginate<Rental> rentals = await _rentalRepository.GetListAsync(
                                        r => r.Id != id && r.CarId == carId &&
                                             r.RentEndDate >= rentStartDate &&
                                             r.RentStartDate <= rentEndDate);
        if (rentals.Items.Any())
            throw new BusinessException(RentalExceptionMessages.RentalAnotherRentedCarForTheDateMessage);
    }

    public Task RentalCanNotBeCreatedWhenCustomerFindeksScoreLowerThanCarMinFindeksScore(
        short customerFindeksCreditRate, short carMinFindeksCreditRate)
    {
        if (customerFindeksCreditRate < carMinFindeksCreditRate)
            throw new BusinessException(RentalExceptionMessages.RentalCanNotBeCreatedScorLowerMessage);
        return Task.CompletedTask;
    }
}