namespace Core.ElasticSearch.Models;

public class SearchByFieldParameters : SearchParameters
{
    public string FieldName { get; set; }
    public string Value { get; set; }
}