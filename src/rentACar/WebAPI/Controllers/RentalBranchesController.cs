using Application.Features.RentalBranches.Commands.CreateRentalBranch;
using Application.Features.RentalBranches.Commands.DeleteRentalBranch;
using Application.Features.RentalBranches.Commands.UpdateRentalBranch;
using Application.Features.RentalBranches.Dtos;
using Application.Features.RentalBranches.Models;
using Application.Features.RentalBranches.Queries.GetByIdRentalBranch;
using Application.Features.RentalBranches.Queries.GetListRentalBranch;
using Core.Application.Requests;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RentalBranchesController : BaseController
{
    [HttpGet("{Id}")]
    public async Task<IActionResult> GetById([FromRoute] GetByIdRentalBranchQuery getByIdRentalBranchQuery)
    {
        RentalBranchDto result = await Mediator.Send(getByIdRentalBranchQuery);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListRentalBranchQuery getListRentalBranchQuery = new() { PageRequest = pageRequest };
        RentalBranchListModel result = await Mediator.Send(getListRentalBranchQuery);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateRentalBranchCommand createRentalBranchCommand)
    {
        CreatedRentalBranchDto result = await Mediator.Send(createRentalBranchCommand);
        return Created("", result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateRentalBranchCommand updateRentalBranchCommand)
    {
        UpdatedRentalBranchDto result = await Mediator.Send(updateRentalBranchCommand);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteRentalBranchCommand deleteRentalBranchCommand)
    {
        DeletedRentalBranchDto result = await Mediator.Send(deleteRentalBranchCommand);
        return Ok(result);
    }
}