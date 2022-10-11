using Application.Features.IndividualCustomers.Dtos;

namespace Application.Services.PersonService;

public interface IPersonService
{
    Task<bool> VerifyNationalId(CitizenDto citizenDto);
}