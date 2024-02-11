using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Repositories;
using NArchitecture.Core.Security.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class EmailAuthenticatorRepository
    : EfRepositoryBase<EmailAuthenticator<int, int>, int, BaseDbContext>,
        IEmailAuthenticatorRepository
{
    public EmailAuthenticatorRepository(BaseDbContext context)
        : base(context) { }
}
