using Application.Features.CarDamages.Commands.Create;
using Application.Features.CarDamages.Commands.Delete;
using Application.Features.CarDamages.Commands.Update;
using Application.Features.CarDamages.Queries.GetById;
using Application.Features.CarDamages.Queries.GetList;
using Application.Features.CarDamages.Queries.GetListByCarId;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CarDamagesController : BaseController
{
    [HttpGet("{Id}")]
    public async Task<IActionResult> GetById([FromRoute] GetByIdCarDamageQuery getByIdCarDamageQuery)
    {
        GetByIdCarDamageResponse result = await Mediator.Send(getByIdCarDamageQuery);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListCarDamageQuery getListCarDamageQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListCarDamageListItemDto> result = await Mediator.Send(getListCarDamageQuery);
        return Ok(result);
    }

    [HttpGet("ByCarId/{carId}")]
    public async Task<IActionResult> GetListByCarId([FromRoute] int carId, [FromQuery] PageRequest pageRequest)
    {
        GetListByCarIdCarDamageQuery getListCarDamageQuery = new() { CarId = carId, PageRequest = pageRequest };
        GetListResponse<GetListByCarIdCarDamageListItemDto> result = await Mediator.Send(getListCarDamageQuery);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateCarDamageCommand createCarDamageCommand)
    {
        CreatedCarDamageResponse result = await Mediator.Send(createCarDamageCommand);
        return Created(uri: "", result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateCarDamageCommand updateCarDamageCommand)
    {
        UpdatedCarDamageResponse result = await Mediator.Send(updateCarDamageCommand);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteCarDamageCommand deleteCarDamageCommand)
    {
        DeletedCarDamageResponse result = await Mediator.Send(deleteCarDamageCommand);
        return Ok(result);
    }
}
