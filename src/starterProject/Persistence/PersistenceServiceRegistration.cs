using Application.Services.Repositories;
using Core.Persistence.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;
using Persistence.Repositories;

namespace Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        _ = services.AddDbContext<BaseDbContext>(options => options.UseInMemoryDatabase("BaseDb"));
        _ = services.AddDbMigrationApplier(buildServices => buildServices.GetRequiredService<BaseDbContext>());

        _ = services.AddScoped<IEmailAuthenticatorRepository, EmailAuthenticatorRepository>();
        _ = services.AddScoped<IOperationClaimRepository, OperationClaimRepository>();
        _ = services.AddScoped<IOtpAuthenticatorRepository, OtpAuthenticatorRepository>();
        _ = services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        _ = services.AddScoped<IUserRepository, UserRepository>();
        _ = services.AddScoped<IUserOperationClaimRepository, UserOperationClaimRepository>();

        return services;
    }
}
