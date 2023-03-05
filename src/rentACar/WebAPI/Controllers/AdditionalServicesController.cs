using Application.Features.AdditionalServices.Commands.Create;
using Application.Features.AdditionalServices.Commands.Delete;
using Application.Features.AdditionalServices.Commands.Update;
using Application.Features.AdditionalServices.Queries.GetById;
using Application.Features.AdditionalServices.Queries.GetList;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AdditionalServicesController : BaseController
{
    [HttpGet("{Id}")]
    public async Task<IActionResult> GetById([FromRoute] GetByIdAdditionalServiceQuery getByIdAdditionalServiceQuery)
    {
        GetByIdAdditionalServiceResponse result = await Mediator.Send(getByIdAdditionalServiceQuery);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListAdditionalServiceQuery getListAdditionalServiceQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListAdditionalServiceListItemDto> result = await Mediator.Send(getListAdditionalServiceQuery);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateAdditionalServiceCommand createAdditionalServiceCommand)
    {
        CreatedAdditionalServiceResponse result = await Mediator.Send(createAdditionalServiceCommand);
        return Created(uri: "", result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateAdditionalServiceCommand updateAdditionalServiceCommand)
    {
        UpdatedAdditionalServiceResponse result = await Mediator.Send(updateAdditionalServiceCommand);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteAdditionalServiceCommand deleteAdditionalServiceCommand)
    {
        DeletedAdditionalServiceResponse result = await Mediator.Send(deleteAdditionalServiceCommand);
        return Ok(result);
    }
}
