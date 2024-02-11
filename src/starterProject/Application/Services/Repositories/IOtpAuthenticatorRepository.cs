using NArchitecture.Core.Persistence.Repositories;
using NArchitecture.Core.Security.Entities;

namespace Application.Services.Repositories;

public interface IOtpAuthenticatorRepository
    : IAsyncRepository<OtpAuthenticator<int, int>, int>,
        IRepository<OtpAuthenticator<int, int>, int> { }
