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
    public async Task<IActionResult> GetById([FromRoute] GetByIdColorQuery getByIdColorQuery)
    {
        ColorDto result = await Mediator.Send(getByIdColorQuery);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListColorQuery getListColorQuery = new() { PageRequest = pageRequest };
        ColorListModel result = await Mediator.Send(getListColorQuery);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateColorCommand createColorCommand)
    {
        CreatedColorDto result = await Mediator.Send(createColorCommand);
        return Created("", result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateColorCommand updateColorCommand)
    {
        UpdatedColorDto result = await Mediator.Send(updateColorCommand);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteColorCommand deleteColorCommand)
    {
        DeletedColorDto result = await Mediator.Send(deleteColorCommand);
        return Ok(result);
    }
}