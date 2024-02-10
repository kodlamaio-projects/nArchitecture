using Core.Persistence.Repositories;
using Core.Security.Entities;

namespace Application.Services.Repositories;

public interface IOtpAuthenticatorRepository
    : IAsyncRepository<OtpAuthenticator<int, int>, int>,
        IRepository<OtpAuthenticator<int, int>, int> { }
