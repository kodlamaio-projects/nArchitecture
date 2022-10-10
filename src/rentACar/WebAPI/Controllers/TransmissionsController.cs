using Application.Features.Transmissions.Commands.CreateTransmission;
using Application.Features.Transmissions.Commands.DeleteTransmission;
using Application.Features.Transmissions.Commands.UpdateTransmission;
using Application.Features.Transmissions.Dtos;
using Application.Features.Transmissions.Models;
using Application.Features.Transmissions.Queries.GetByIdTransmission;
using Application.Features.Transmissions.Queries.GetListTransmission;
using Core.Application.Requests;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TransmissionsController : BaseController
{
    [HttpGet("{Id}")]
    public async Task<IActionResult> GetById([FromRoute] GetByIdTransmissionQuery getByIdTransmissionQuery)
    {
        TransmissionDto result = await Mediator.Send(getByIdTransmissionQuery);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListTransmissionQuery getListTransmissionQuery = new() { PageRequest = pageRequest };
        TransmissionListModel result = await Mediator.Send(getListTransmissionQuery);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateTransmissionCommand createTransmissionCommand)
    {
        CreatedTransmissionDto result = await Mediator.Send(createTransmissionCommand);
        return Created("", result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateTransmissionCommand updateTransmissionCommand)
    {
        UpdatedTransmissionDto result = await Mediator.Send(updateTransmissionCommand);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteTransmissionCommand deleteTransmissionCommand)
    {
        DeletedTransmissionDto result = await Mediator.Send(deleteTransmissionCommand);
        return Ok(result);
    }
}