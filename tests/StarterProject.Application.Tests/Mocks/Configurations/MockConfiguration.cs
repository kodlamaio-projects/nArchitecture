using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterProject.Application.Tests.Mocks.Configurations
{
    public static class MockConfiguration
    {
        public static IConfiguration GetConfigurationMock()
        {
            var mockConfiguration = new Dictionary<string, string>
            {
                { "TokenOptions:Audience", "Kodlamaio Users" },
                { "TokenOptions:Issuer", "Kodlamaio" },
                { "TokenOptions:AccessTokenExpiration", "10" },
                { "TokenOptions:SecurityKey", "StrongAndSecretKeyStrongAndSecretKeyStrongAndSecretKeyStrongAndSecretKey" },
                { "TokenOptions:RefreshTokenExpiration", "1440" },
                { "TokenOptions:RefreshTokenTTL", "180" },
                { "MailSettings:Server", "127.0.0.1" },
            };
            var configuration = new ConfigurationBuilder().AddInMemoryCollection(mockConfiguration);
            return configuration.Build();
        }
    }
}
