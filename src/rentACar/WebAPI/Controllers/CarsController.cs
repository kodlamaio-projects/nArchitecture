using Application.Features.Cars.Commands.Create;
using Application.Features.Cars.Commands.Delete;
using Application.Features.Cars.Commands.DeliverRental;
using Application.Features.Cars.Commands.Maintain;
using Application.Features.Cars.Commands.Update;
using Application.Features.Cars.Queries.GetById;
using Application.Features.Cars.Queries.GetList;
using Application.Features.Cars.Queries.GetListByDynamic;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CarsController : BaseController
{
    [HttpGet("{Id}")]
    public async Task<IActionResult> GetById([FromRoute] GetByIdCarQuery getByIdCarQuery)
    {
        GetByIdCarResponse result = await Mediator.Send(getByIdCarQuery);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListCarQuery getListCarQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListCarListItemDto> result = await Mediator.Send(getListCarQuery);
        return Ok(result);
    }

    [HttpPost("GetList/ByDynamic")]
    public async Task<IActionResult> GetListByDynamic([FromQuery] PageRequest pageRequest, [FromBody] DynamicQuery? dynamicQuery = null)
    {
        GetListByDynamicCarQuery getListCarByDynamicQuery = new() { PageRequest = pageRequest, DynamicQuery = dynamicQuery };
        GetListResponse<GetListByDynamicCarListItemDto> result = await Mediator.Send(getListCarByDynamicQuery);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateCarCommand createCarCommand)
    {
        CreatedCarResponse result = await Mediator.Send(createCarCommand);
        return Created(uri: "", result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateCarCommand updateCarCommand)
    {
        UpdatedCarResponse result = await Mediator.Send(updateCarCommand);
        return Ok(result);
    }

    [HttpPut("Maintain")]
    public async Task<IActionResult> MaintainCar([FromBody] MaintainCarCommand maintainCarCommand)
    {
        MaintainedCarResponse result = await Mediator.Send(maintainCarCommand);
        return Ok(result);
    }

    [HttpPut("DeliverRentalCar")]
    public async Task<IActionResult> DeliverRentalCarCommand([FromBody] DeliverRentalCarCommand deliverRentalCarCommand)
    {
        DeliveredCarResponse result = await Mediator.Send(deliverRentalCarCommand);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteCarCommand deleteCarCommand)
    {
        DeletedCarResponse result = await Mediator.Send(deleteCarCommand);
        return Ok(result);
    }
}
