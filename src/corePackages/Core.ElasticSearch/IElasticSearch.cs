using Core.ElasticSearch.Models;
using Nest;

namespace Core.ElasticSearch;

public interface IElasticSearch
{
    Task<IElasticSearchResult> CreateNewIndexAsync(IndexModel indexModel);
    Task<IElasticSearchResult> InsertAsync(ElasticSearchInsertUpdateModel model);
    Task<IElasticSearchResult> InsertManyAsync(string indexName, object[] items);
    IReadOnlyDictionary<IndexName, IndexState> GetIndexList();

    Task<List<ElasticSearchGetModel<T>>> GetAllSearch<T>(SearchParameters parameters)
        where T : class;

    Task<List<ElasticSearchGetModel<T>>> GetSearchByField<T>(SearchByFieldParameters fieldParameters)
        where T : class;

    Task<List<ElasticSearchGetModel<T>>> GetSearchBySimpleQueryString<T>(SearchByQueryParameters queryParameters)
        where T : class;

    Task<IElasticSearchResult> UpdateByElasticIdAsync(ElasticSearchInsertUpdateModel model);
    Task<IElasticSearchResult> DeleteByElasticIdAsync(ElasticSearchModel model);
}
