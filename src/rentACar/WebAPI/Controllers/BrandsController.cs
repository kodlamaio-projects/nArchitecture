using Application.Features.Brands.Commands.Create;
using Application.Features.Brands.Commands.Delete;
using Application.Features.Brands.Commands.Update;
using Application.Features.Brands.Queries.GetById;
using Application.Features.Brands.Queries.GetList;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BrandsController : BaseController
{
    [HttpGet("{Id}")]
    public async Task<IActionResult> GetById([FromRoute] GetByIdBrandQuery getByIdBrandQuery)
    {
        GetByIdBrandResponse result = await Mediator.Send(getByIdBrandQuery);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListBrandQuery getListBrandQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListBrandListItemDto> result = await Mediator.Send(getListBrandQuery);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateBrandCommand createBrandCommand)
    {
        CreatedBrandResponse result = await Mediator.Send(createBrandCommand);
        return Created("", result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateBrandCommand updateBrandCommand)
    {
        UpdatedBrandResponse result = await Mediator.Send(updateBrandCommand);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteBrandCommand deleteBrandCommand)
    {
        DeletedBrandResponse result = await Mediator.Send(deleteBrandCommand);
        return Ok(result);
    }

    [HttpPost("BulkInsert")]
    public async Task<IActionResult> BulkInsert([FromBody] CreateBulkBrandCommand bulkBrandCommand)
    {
        List<CreatedBrandResponse> result = await Mediator.Send(bulkBrandCommand);
        return Created("", result);

    }
}