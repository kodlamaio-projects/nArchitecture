using Application.Features.RentalBranches.Commands.Create;
using Application.Features.RentalBranches.Commands.Delete;
using Application.Features.RentalBranches.Commands.Update;
using Application.Features.RentalBranches.Queries.GetById;
using Application.Features.RentalBranches.Queries.GetList;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RentalBranchesController : BaseController
{
    [HttpGet("{Id}")]
    public async Task<IActionResult> GetById([FromRoute] GetByIdRentalBranchQuery getByIdRentalBranchQuery)
    {
        GetByIdRentalBranchResponse result = await Mediator.Send(getByIdRentalBranchQuery);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListRentalBranchQuery getListRentalBranchQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListRentalBranchListItemDto> result = await Mediator.Send(getListRentalBranchQuery);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateRentalBranchCommand createRentalBranchCommand)
    {
        CreatedRentalBranchResponse result = await Mediator.Send(createRentalBranchCommand);
        return Created("", result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateRentalBranchCommand updateRentalBranchCommand)
    {
        UpdatedRentalBranchResponse result = await Mediator.Send(updateRentalBranchCommand);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteRentalBranchCommand deleteRentalBranchCommand)
    {
        DeletedRentalBranchResponse result = await Mediator.Send(deleteRentalBranchCommand);
        return Ok(result);
    }
}