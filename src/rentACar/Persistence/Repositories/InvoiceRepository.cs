using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class InvoiceRepository : EfRepositoryBase<Invoice, BaseDbContext>, IInvoiceRepository
{
    public InvoiceRepository(BaseDbContext context)
        : base(context) { }
}
