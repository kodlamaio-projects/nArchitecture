using Core.CrossCuttingConcerns.Logging.Serilog.ConfigurationModels;
using Core.CrossCuttingConcerns.Logging.Serilog.Messages;
using Microsoft.Extensions.Configuration;
using NpgsqlTypes;
using Serilog;
using Serilog.Sinks.PostgreSQL;

namespace Core.CrossCuttingConcerns.Logging.Serilog.Logger
{
    public class PostgreSqlLogger : LoggerServiceBase
    {
        public PostgreSqlLogger(IConfiguration configuration)
        {
            var postgreConfiguration = configuration.GetSection("SeriLogConfigurations:PostgreConfiguration")
                .Get<PostgreSqlConfiguration>() ??
                throw new Exception(SerilogMessages.NullOptionsMessage);

            IDictionary<string, ColumnWriterBase> columnWriters = new Dictionary<string, ColumnWriterBase>
            {
                {"message", new RenderedMessageColumnWriter(NpgsqlDbType.Text) },
                {"message_template", new MessageTemplateColumnWriter(NpgsqlDbType.Text) },
                {"level", new LevelColumnWriter(true, NpgsqlDbType.Varchar) },
                {"raise_date", new TimestampColumnWriter(NpgsqlDbType.Timestamp) },
                {"exception", new ExceptionColumnWriter(NpgsqlDbType.Text) },
                {"properties", new LogEventSerializedColumnWriter(NpgsqlDbType.Jsonb) },
                {"props_test", new PropertiesColumnWriter(NpgsqlDbType.Jsonb) },
                {"machine_name", new SinglePropertyColumnWriter("MachineName", PropertyWriteMethod.ToString, NpgsqlDbType.Text, "l") }
            };

            var loggerConfiguration = new LoggerConfiguration()
                .WriteTo.PostgreSQL(
                connectionString: postgreConfiguration.ConnectionString,
                tableName: postgreConfiguration.TableName,
                columnOptions: columnWriters,
                needAutoCreateTable: postgreConfiguration.NeedAutoCreateTable)
                .CreateLogger();
            Logger = loggerConfiguration;
        }
    }
}