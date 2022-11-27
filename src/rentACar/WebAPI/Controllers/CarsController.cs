using Application.Features.Cars.Commands.CreateCar;
using Application.Features.Cars.Commands.DeleteCar;
using Application.Features.Cars.Commands.DeliverRentalCar;
using Application.Features.Cars.Commands.MaintainCar;
using Application.Features.Cars.Commands.UpdateCar;
using Application.Features.Cars.Dtos;
using Application.Features.Cars.Models;
using Application.Features.Cars.Queries.GetByIdCar;
using Application.Features.Cars.Queries.GetListCar;
using Application.Features.Cars.Queries.GetListCarByDynamic;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CarsController : BaseController
{
    [HttpGet("{Id}")]
    public async Task<IActionResult> GetById([FromRoute] GetByIdCarQuery getByIdCarQuery)
    {
        CarDto result = await Mediator.Send(getByIdCarQuery);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListCarQuery getListCarQuery = new()
        { PageRequest = pageRequest };
        CarListModel result = await Mediator.Send(getListCarQuery);
        return Ok(result);
    }

    [HttpPost("GetList/ByDynamic")]
    public async Task<IActionResult> GetListByDynamic([FromQuery] PageRequest pageRequest,
                                                      [FromBody] Dynamic? dynamic = null)
    {
        GetListCarByDynamicQuery getListCarByDynamicQuery = new() { PageRequest = pageRequest, Dynamic = dynamic };
        CarListModel result = await Mediator.Send(getListCarByDynamicQuery);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateCarCommand createCarCommand)
    {
        CreatedCarDto result = await Mediator.Send(createCarCommand);
        return Created("", result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateCarCommand updateCarCommand)
    {
        UpdatedCarDto result = await Mediator.Send(updateCarCommand);
        return Ok(result);
    }

    [HttpPut("Maintain")]
    public async Task<IActionResult> MaintainCar([FromBody] MaintainCarCommand maintainCarCommand)
    {
        UpdatedCarDto result = await Mediator.Send(maintainCarCommand);
        return Ok(result);
    }

    [HttpPut("DeliverRentalCar")]
    public async Task<IActionResult> DeliverRentalCarCommand([FromBody] DeliverRentalCarCommand deliverRentalCarCommand)
    {
        UpdatedCarDto result = await Mediator.Send(deliverRentalCarCommand);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteCarCommand deleteCarCommand)
    {
        DeletedCarDto result = await Mediator.Send(deleteCarCommand);
        return Ok(result);
    }
}