using Application.Features.Models.Commands.CreateModel;
using Application.Features.Models.Dtos;
using Core.ElasticSearch;
using Core.ElasticSearch.Models;
using Microsoft.AspNetCore.Mvc;
using Nest;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ElasticTestController : BaseController
{
    private readonly IElasticSearch _elasticSearch;

    public ElasticTestController(IElasticSearch elasticSearch)
    {
        _elasticSearch = elasticSearch;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        IElasticSearchResult result = await _elasticSearch.CreateNewIndexAsync(new IndexModel
        {
            IndexName = "models",
            AliasName = "amodels",
            NumberOfReplicas = 1,
            NumberOfShards = 1
        });

        ElasticSearchInsertUpdateModel model = new()
        {
            IndexName = "models",
            Item = new CreateModelCommand
            { BrandId = 1, FuelId = 1, TransmissionId = 1, DailyPrice = 1000, Name = "BMW" }
        };

        IElasticSearchResult result2 = await _elasticSearch.InsertAsync(model);

        IEnumerable<IndexName> result3 = _elasticSearch.GetIndexList().Keys;

        List<ElasticSearchGetModel<ModelListDto>> result4 = await
                                                                _elasticSearch.GetSearchByField<ModelListDto>(
                                                                    new SearchByFieldParameters
                                                                    {
                                                                        IndexName = "models",
                                                                        FieldName = "Name",
                                                                        Value = "BMW"
                                                                    });

        return Ok(result4);
    }
}