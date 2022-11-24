namespace Core.CrossCuttingConcerns.Logging.Serilog.ConfigurationModels
{
    public class PostgreSqlConfiguration
    {
        public string ConnectionString { get; set; }
        public string TableName { get; set; }
        public bool NeedAutoCreateTable { get; set; }
    }
}
