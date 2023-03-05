using Application.Features.Rentals.Commands.Create;
using Application.Features.Rentals.Commands.Delete;
using Application.Features.Rentals.Commands.PickUp;
using Application.Features.Rentals.Commands.Update;
using Application.Features.Rentals.Queries.GetById;
using Application.Features.Rentals.Queries.GetList;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RentalsController : BaseController
{
    [HttpGet("{Id}")]
    public async Task<IActionResult> GetById([FromRoute] GetByIdRentalQuery getByIdRentalQuery)
    {
        GetByIdRentalResponse result = await Mediator.Send(getByIdRentalQuery);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListRentalQuery getListRentalQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListRentalListItemDto> result = await Mediator.Send(getListRentalQuery);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateRentalCommand createRentalCommand)
    {
        CreatedRentalResponse result = await Mediator.Send(createRentalCommand);
        return Created(uri: "", result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateRentalCommand updateRentalCommand)
    {
        UpdatedRentalResponse result = await Mediator.Send(updateRentalCommand);
        return Ok(result);
    }

    [HttpPut("PickUp")]
    public async Task<IActionResult> PickUp([FromBody] PickUpRentalCommand pickUpRentalCommand)
    {
        PickUpRentalResponse result = await Mediator.Send(pickUpRentalCommand);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteRentalCommand deleteRentalCommand)
    {
        DeletedRentalResponse result = await Mediator.Send(deleteRentalCommand);
        return Ok(result);
    }
}
