using Application.Tests.DependencyResolvers;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Tests;

public sealed class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddUsersServices();
    }
}
