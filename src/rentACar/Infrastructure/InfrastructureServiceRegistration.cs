using Application.Services.FindeksService;
using Application.Services.ImageService;
using Application.Services.PersonService;
using Application.Services.POSService;
using Infrastructure.Adapters.FakeFindeksService;
using Infrastructure.Adapters.FakePOSService;
using Infrastructure.Adapters.ImageService;
using Infrastructure.Adapters.PersonService;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IFindeksService, FakeFindeksServiceAdapter>();
        services.AddScoped<IPOSService, FakePOSServiceAdapter>();
        services.AddScoped<IImageService, CloudinaryImageServiceAdapter>();
        services.AddScoped<IPersonService, PersonServiceAdapter>();
        return services;
    }
}