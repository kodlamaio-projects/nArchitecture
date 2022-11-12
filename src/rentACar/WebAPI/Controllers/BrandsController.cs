using Application.Features.Brands.Commands.CreateBrand;
using Application.Features.Brands.Commands.DeleteBrand;
using Application.Features.Brands.Commands.UpdateBrand;
using Application.Features.Brands.Dtos;
using Application.Features.Brands.Models;
using Application.Features.Brands.Queries.GetByIdBrand;
using Application.Features.Brands.Queries.GetListBrand;
using Core.Application.Requests;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BrandsController : BaseController
{
    [HttpGet("{Id}")]
    public async Task<IActionResult> GetById([FromRoute] GetByIdBrandQuery getByIdBrandQuery)
    {
        BrandDto result = await Mediator.Send(getByIdBrandQuery);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListBrandQuery getListBrandQuery = new() { PageRequest = pageRequest };
        BrandListModel result = await Mediator.Send(getListBrandQuery);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateBrandCommand createBrandCommand)
    {
        CreatedBrandDto result = await Mediator.Send(createBrandCommand);
        return Created("", result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateBrandCommand updateBrandCommand)
    {
        UpdatedBrandDto result = await Mediator.Send(updateBrandCommand);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteBrandCommand deleteBrandCommand)
    {
        DeletedBrandDto result = await Mediator.Send(deleteBrandCommand);
        return Ok(result);
    }

    [HttpPost("BulkInsert")]
    public async Task<IActionResult> BulkInsert([FromBody] CreateBulkBrandCommand bulkBrandCommand)
    {
        List<CreatedBrandDto> result = await Mediator.Send(bulkBrandCommand);
        return Created("", result);

    }
}