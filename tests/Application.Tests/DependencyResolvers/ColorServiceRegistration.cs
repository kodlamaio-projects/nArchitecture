using Application.Features.Colors.Commands.CreateColor;
using Application.Features.Colors.Commands.DeleteColor;
using Application.Features.Colors.Commands.UpdateColor;
using Application.Features.Colors.Queries.GetByIdColor;
using Application.Features.Colors.Queries.GetListColor;
using Application.Tests.Mocks.FakeData;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Tests.DependencyResolvers
{
    public static class ColorServiceRegistration
    {
        public static void AddColorServices(this IServiceCollection services)
        {
            services.AddTransient<ColorFakeData>();
            services.AddTransient<CreateColorCommand>();
            services.AddTransient<UpdateColorCommand>();
            services.AddTransient<DeleteColorCommand>();
            services.AddTransient<GetByIdColorQuery>();
            services.AddTransient<GetListColorQuery>();
            services.AddSingleton<CreateColorCommandValidator>();
            services.AddSingleton<UpdateColorCommandValidator>();
        }
    }
}