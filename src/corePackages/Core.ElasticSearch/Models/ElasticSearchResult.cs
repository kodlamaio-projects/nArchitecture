namespace Core.ElasticSearch.Models;

public class ElasticSearchResult : IElasticSearchResult //todo: refactor
{
    public ElasticSearchResult(bool success, string message) : this(success)
    {
        Message = message;
    }

    public ElasticSearchResult(bool success)
    {
        Success = success;
    }

    public bool Success { get; set; }
    public string Message { get; set; }
}