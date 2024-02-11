using Application.Features.Auth.Commands.Login;
using Microsoft.Extensions.DependencyInjection;
using StarterProject.Application.Tests.Mocks.FakeDatas;

namespace StarterProject.Application.Tests.DependencyResolvers;

public static class AuthServiceRegistrations
{
    public static void AddAuthServices(this IServiceCollection services)
    {
        services.AddTransient<UserFakeData>();
        services.AddTransient<OperationClaimFakeData>();
        services.AddTransient<RefreshTokenFakeData>();
        services.AddTransient<LoginCommand>();
    }
}
