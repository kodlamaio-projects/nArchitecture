using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Persistence.MigrationConfigurations.Services;

namespace Persistence.MigrationConfigurations.Extensions;
public static class MigrationCreatorExtensions
{
    public static Task UseMigrationCreator(this IApplicationBuilder app)
    {
        var service = app.ApplicationServices.GetRequiredService<IMigrationCreatorService>();
        service.Initialze();
        return Task.CompletedTask;
    }
}
