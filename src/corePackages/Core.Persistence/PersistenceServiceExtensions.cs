using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Core.Persistence
{
    public static class PersistenceServiceExtensions
    {
        public static IServiceCollection AddRepositoryServices(this IServiceCollection services, Assembly assembly,
            Type type, string interfacePrefix="I")
        {
            Type[] repositories = assembly.GetTypes()
                .Where(t => t.BaseType!.IsGenericType && t.BaseType.GetGenericTypeDefinition()==type).ToArray();

            foreach (Type repository in repositories)
            {
                string repositoryName = repository.Name;
                
                Type? repositoryInterface =
                    repository.GetInterfaces().FirstOrDefault(e => e.Name == $"{interfacePrefix}{repositoryName}");

                if (repositoryInterface==null)
                {
                    throw new NotFoundException($"Reflection could not find the interface '{interfacePrefix}{repositoryName}' for '{repositoryName}'");
                }
                services.AddScoped(repositoryInterface, repository);
            }

            return services;
        }
    }
}