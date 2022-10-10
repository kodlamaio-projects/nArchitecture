using Domain.Entities;

namespace Application.Services.CustomerService;

public interface ICustomerService
{
    public Task<Customer?> GetByUserId(int userId);
}