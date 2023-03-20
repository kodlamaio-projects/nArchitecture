using Application.Features.CorporateCustomers.Commands.Create;
using Application.Features.CorporateCustomers.Commands.Delete;
using Application.Features.CorporateCustomers.Commands.Update;
using Application.Features.CorporateCustomers.Queries.GetByCustomerId;
using Application.Features.CorporateCustomers.Queries.GetById;
using Application.Features.CorporateCustomers.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CorporateCustomersController : BaseController
{
    [HttpGet("{Id}")]
    public async Task<IActionResult> GetById([FromRoute] GetByIdCorporateCustomerQuery getByIdCorporateCustomerQuery)
    {
        GetByIdCorporateCustomerResponse result = await Mediator.Send(getByIdCorporateCustomerQuery);
        return Ok(result);
    }

    [HttpGet("ByCustomerId/{CustomerId}")]
    public async Task<IActionResult> GetById([FromRoute] GetByCustomerIdCorporateCustomerQuery getByCustomerIdCorporateCustomerQuery)
    {
        GetByCustomerIdCorporateCustomerResponse result = await Mediator.Send(getByCustomerIdCorporateCustomerQuery);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListCorporateCustomerQuery getListCorporateCustomerQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListCorporateCustomerListItemDto> result = await Mediator.Send(getListCorporateCustomerQuery);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateCorporateCustomerCommand createCorporateCustomerCommand)
    {
        CreatedCorporateCustomerResponse result = await Mediator.Send(createCorporateCustomerCommand);
        return Created(uri: "", result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateCorporateCustomerCommand updateCorporateCustomerCommand)
    {
        UpdatedCorporateCustomerResponse result = await Mediator.Send(updateCorporateCustomerCommand);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteCorporateCustomerCommand deleteCorporateCustomerCommand)
    {
        DeletedCorporateCustomerResponse result = await Mediator.Send(deleteCorporateCustomerCommand);
        return Ok(result);
    }
}
