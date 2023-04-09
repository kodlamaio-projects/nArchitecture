using Application.Features.Users.Commands.Create;
using Application.Features.Users.Commands.Delete;
using Application.Features.Users.Commands.Update;
using Application.Features.Users.Queries.GetById;
using Application.Features.Users.Queries.GetList;
using Microsoft.Extensions.DependencyInjection;
using StarterProject.Tests.Mocks.FakeDatas;

namespace StarterProject.Tests.DependencyResolvers
{
    public static class AuthServiceRegistrations
    {
        public static void AddAuthServices(this IServiceCollection services) { }
    }
}
