using Application.Features.Colors.Commands.CreateColor;
using Application.Features.Colors.Commands.DeleteColor;
using Application.Features.Colors.Commands.UpdateColor;
using Application.Features.Colors.Dtos;
using Application.Features.Colors.Models;
using Application.Features.Colors.Queries.GetByIdColor;
using Application.Features.Colors.Queries.GetListColor;
using Core.Application.Requests;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ColorsController : BaseController
{
    [HttpGet("{Id}")]
    public async Task<IActionResult> GetById([FromRoute] GetByIdSpeedQuery getByIdColorQuery)
    {
        SpeedDto result = await Mediator.Send(getByIdColorQuery);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListSpeedQuery getListColorQuery = new() { PageRequest = pageRequest };
        SpeedListModel result = await Mediator.Send(getListColorQuery);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateSpeedCommand createColorCommand)
    {
        CreatedSpeedDto result = await Mediator.Send(createColorCommand);
        return Created("", result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateColorCommand updateColorCommand)
    {
        UpdatedSpeedDto result = await Mediator.Send(updateColorCommand);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteColorCommand deleteColorCommand)
    {
        DeletedSpeedDto result = await Mediator.Send(deleteColorCommand);
        return Ok(result);
    }
}