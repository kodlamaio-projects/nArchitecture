using Application.Services.ImageService;
using Application.Services.TranslateService;
using Infrastructure.Adapters.ImageService;
using Infrastructure.Adapters.TranslateService;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<ImageServiceBase, CloudinaryImageServiceAdapter>();
        services.AddScoped<ITranslateService, AmazonTranslateServiceAdapter>();

        return services;
    }
}
