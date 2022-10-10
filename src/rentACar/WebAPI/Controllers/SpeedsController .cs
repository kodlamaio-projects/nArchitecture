using Application.Features.Speeds.Commands.CreateSpeed;
using Application.Features.Speeds.Commands.DeleteSpeed;
using Application.Features.Speeds.Commands.UpdateSpeed;
using Application.Features.Speeds.Dtos;
using Application.Features.Speeds.Models;
using Application.Features.Speeds.Queries.GetByIdSpeed;
using Application.Features.Speeds.Queries.GetListSpeed;
using Core.Application.Requests;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SpeedsController : BaseController
{
    [HttpGet("{Id}")]
    public async Task<IActionResult> GetById([FromRoute] GetByIdSpeedQuery getByIdSpeedQuery)
    {
        SpeedDto result = await Mediator.Send(getByIdSpeedQuery);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListSpeedQuery getListSpeedQuery = new() { PageRequest = pageRequest };
        SpeedListModel result = await Mediator.Send(getListSpeedQuery);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateSpeedCommand createSpeedCommand)
    {
        CreatedSpeedDto result = await Mediator.Send(createSpeedCommand);
        return Created("", result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateSpeedCommand updateSpeedCommand)
    {
        UpdatedSpeedDto result = await Mediator.Send(updateSpeedCommand);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteSpeedCommand deleteSpeedCommand)
    {
        DeletedSpeedDto result = await Mediator.Send(deleteSpeedCommand);
        return Ok(result);
    }
}