using NArchitecture.Core.Persistence.Repositories;
using NArchitecture.Core.Security.Entities;

namespace Application.Services.Repositories;

public interface IEmailAuthenticatorRepository
    : IAsyncRepository<EmailAuthenticator<int, int>, int>,
        IRepository<EmailAuthenticator<int, int>, int> { }
