using Application.Features.Rentals.Commands.CreateRental;
using Application.Features.Rentals.Commands.DeleteRental;
using Application.Features.Rentals.Commands.PickUpRental;
using Application.Features.Rentals.Commands.UpdateRental;
using Application.Features.Rentals.Dtos;
using Application.Features.Rentals.Models;
using Application.Features.Rentals.Queries.GetByIdRental;
using Application.Features.Rentals.Queries.GetListRental;
using Core.Application.Requests;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RentalsController : BaseController
{
    [HttpGet("{Id}")]
    public async Task<IActionResult> GetById([FromRoute] GetByIdRentalQuery getByIdRentalQuery)
    {
        RentalDto result = await Mediator.Send(getByIdRentalQuery);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListRentalQuery getListRentalQuery = new() { PageRequest = pageRequest };
        RentalListModel result = await Mediator.Send(getListRentalQuery);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateRentalCommand createRentalCommand)
    {
        CreatedRentalDto result = await Mediator.Send(createRentalCommand);
        return Created("", result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateRentalCommand updateRentalCommand)
    {
        UpdatedRentalDto result = await Mediator.Send(updateRentalCommand);
        return Ok(result);
    }

    [HttpPut("PickUp")]
    public async Task<IActionResult> PickUp([FromBody] PickUpRentalCommand pickUpRentalCommand)
    {
        UpdatedRentalDto result = await Mediator.Send(pickUpRentalCommand);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteRentalCommand deleteRentalCommand)
    {
        DeletedRentalDto result = await Mediator.Send(deleteRentalCommand);
        return Ok(result);
    }
}