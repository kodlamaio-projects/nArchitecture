namespace Core.CrossCuttingConcerns.Logging.Serilog.ConfigurationModels
{
    public class ElasticSearchConfiguration
    {
        public string ConnectionString { get; set; }

        public ElasticSearchConfiguration()
        {
            this.ConnectionString = String.Empty;
        }
    }
}