using Application.Services.FindeksService;
using Application.Services.ImageService;
using Application.Services.POSService;
using Infrastructure.Adapters.FakeFindeksService;
using Infrastructure.Adapters.FakePOSService;
using Infrastructure.Adapters.ImageService;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IFindeksService, FakeFindeksServiceAdapter>();
        services.AddScoped<IPOSService, FakePOSServiceAdapter>();
        services.AddScoped<ImageServiceBase, CloudinaryImageServiceAdapter>();
        return services;
    }
}
