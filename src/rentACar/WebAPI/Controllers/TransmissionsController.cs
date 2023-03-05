using Application.Features.Transmissions.Commands.Create;
using Application.Features.Transmissions.Commands.Delete;
using Application.Features.Transmissions.Commands.Update;
using Application.Features.Transmissions.Queries.GetById;
using Application.Features.Transmissions.Queries.GetList;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TransmissionsController : BaseController
{
    [HttpGet("{Id}")]
    public async Task<IActionResult> GetById([FromRoute] GetByIdTransmissionQuery getByIdTransmissionQuery)
    {
        GetByIdTransmissionResponse result = await Mediator.Send(getByIdTransmissionQuery);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListTransmissionQuery getListTransmissionQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListTransmissionListItemDto> result = await Mediator.Send(getListTransmissionQuery);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateTransmissionCommand createTransmissionCommand)
    {
        CreatedTransmissionResponse result = await Mediator.Send(createTransmissionCommand);
        return Created(uri: "", result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateTransmissionCommand updateTransmissionCommand)
    {
        UpdatedTransmissionResponse result = await Mediator.Send(updateTransmissionCommand);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteTransmissionCommand deleteTransmissionCommand)
    {
        DeletedTransmissionResponse result = await Mediator.Send(deleteTransmissionCommand);
        return Ok(result);
    }
}
