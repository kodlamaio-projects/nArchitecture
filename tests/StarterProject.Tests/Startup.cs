using Application.Services.AuthenticatorService;
using Application.Services.AuthService;
using Core.Security;
using Microsoft.Extensions.DependencyInjection;
using StarterProject.Tests.DependencyResolvers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterProject.Tests
{
    public sealed class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthServices();
        }
    }
}
