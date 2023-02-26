using Application.Features.Colors.Commands.Create;
using Application.Features.Colors.Commands.Delete;
using Application.Features.Colors.Commands.Update;
using Application.Features.Colors.Queries.GetById;
using Application.Features.Colors.Queries.GetList;
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