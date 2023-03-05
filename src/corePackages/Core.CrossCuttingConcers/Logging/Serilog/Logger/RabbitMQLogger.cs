using Core.CrossCuttingConcerns.Logging.Serilog.ConfigurationModels;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Formatting.Json;
using Serilog.Sinks.RabbitMQ;
using Serilog.Sinks.RabbitMQ.Sinks.RabbitMQ;

namespace Core.CrossCuttingConcerns.Logging.Serilog.Logger;

public class RabbitMQLogger : LoggerServiceBase
{
    public RabbitMQLogger(IConfiguration configuration)
    {
        RabbitMQConfiguration rabbitMQConfiguration = configuration
            .GetSection("SeriLogConfigurations:RabbitMQConfiguration")
            .Get<RabbitMQConfiguration>();

        RabbitMQClientConfiguration config =
            new()
            {
                Port = rabbitMQConfiguration.Port,
                DeliveryMode = RabbitMQDeliveryMode.Durable,
                Exchange = rabbitMQConfiguration.Exchange,
                Username = rabbitMQConfiguration.Username,
                Password = rabbitMQConfiguration.Password,
                ExchangeType = rabbitMQConfiguration.ExchangeType,
                RouteKey = rabbitMQConfiguration.RouteKey
            };
        rabbitMQConfiguration.Hostnames.ForEach(x => config.Hostnames.Add(x));

        Logger = new LoggerConfiguration().WriteTo
            .RabbitMQ(
                (clientConfiguration, sinkConfiguration) =>
                {
                    clientConfiguration.From(config);
                    sinkConfiguration.TextFormatter = new JsonFormatter();
                }
            )
            .CreateLogger();
    }
}
