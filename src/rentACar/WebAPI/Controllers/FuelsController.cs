using Application.Features.Fuels.Commands.CreateFuel;
using Application.Features.Fuels.Commands.DeleteFuel;
using Application.Features.Fuels.Commands.UpdateFuel;
using Application.Features.Fuels.Dtos;
using Application.Features.Fuels.Models;
using Application.Features.Fuels.Queries.GetByIdFuel;
using Application.Features.Fuels.Queries.GetListFuel;
using Core.Application.Requests;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FuelsController : BaseController
{
    [HttpGet("{Id}")]
    public async Task<IActionResult> GetById([FromRoute] GetByIdFuelQuery getByIdFuelQuery)
    {
        FuelDto result = await Mediator.Send(getByIdFuelQuery);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListFuelQuery getListFuelQuery = new() { PageRequest = pageRequest };
        FuelListModel result = await Mediator.Send(getListFuelQuery);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateFuelCommand createFuelCommand)
    {
        CreatedFuelDto result = await Mediator.Send(createFuelCommand);
        return Created("", result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateFuelCommand updateFuelCommand)
    {
        UpdatedFuelDto result = await Mediator.Send(updateFuelCommand);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteFuelCommand deleteFuelCommand)
    {
        DeletedFuelDto result = await Mediator.Send(deleteFuelCommand);
        return Ok(result);
    }
}