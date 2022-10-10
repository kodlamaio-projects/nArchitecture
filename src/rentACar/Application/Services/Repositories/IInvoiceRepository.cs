using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories;

public interface IInvoiceRepository : IAsyncRepository<Invoice>, IRepository<Invoice>
{
}