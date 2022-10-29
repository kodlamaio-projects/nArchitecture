using System.Reflection;
using System.Runtime.CompilerServices;
using Application.Services.Repositories;
using Core.Persistence;
using Core.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;
using Persistence.Repositories;

namespace Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
                                                            IConfiguration configuration)
    {
        services.AddDbContext<BaseDbContext>(options =>
                                                 options.UseSqlServer(
                                                     configuration.GetConnectionString("RentACarConnectionString")));

        services.AddRepositoryServices(Assembly.GetExecutingAssembly(), typeof(EfRepositoryBase<,>));

        return services;
    }

}