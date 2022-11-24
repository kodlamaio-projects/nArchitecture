using Core.CrossCuttingConcerns.Logging.Serilog.ConfigurationModels;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Serilog;

namespace Core.CrossCuttingConcerns.Logging.Serilog.Logger
{
    public class MongoDbLogger : LoggerServiceBase
    {
        public MongoDbLogger(IConfiguration configuration)
        {
            var logConfiguration = configuration.GetSection("SeriLogConfigurations:MongoDbConfiguration").Get<MongoDbConfiguration>();

            Logger = new LoggerConfiguration()
                .WriteTo.MongoDBBson(cfg =>
                {
                    var client = new MongoClient(logConfiguration.ConnectionString);
                    var mongoDbInstance = client.GetDatabase(logConfiguration.Collection);
                    cfg.SetMongoDatabase(mongoDbInstance);
                })
                .CreateLogger();
        }
    }
}