using Domain.Entities;

namespace Application.Services.RentalsIAdditionalServiceService;

public interface IRentalsAdditionalServiceService
{
    public Task<IList<RentalsAdditionalService>> AddManyByRentalIdAndAdditionalServices(int rentalId, IList<AdditionalService> additionalServices);
}