using Application.Features.CorporateCustomers.Commands.CreateCorporateCustomer;
using Application.Features.CorporateCustomers.Commands.DeleteCorporateCustomer;
using Application.Features.CorporateCustomers.Commands.UpdateCorporateCustomer;
using Application.Features.CorporateCustomers.Dtos;
using Application.Features.CorporateCustomers.Models;
using Application.Features.CorporateCustomers.Queries.GetByCustomerIdCorporateCustomer;
using Application.Features.CorporateCustomers.Queries.GetByIdCorporateCustomer;
using Application.Features.CorporateCustomers.Queries.GetListCorporateCustomer;
using Core.Application.Requests;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CorporateCustomersController : BaseController
{
    [HttpGet("{Id}")]
    public async Task<IActionResult> GetById([FromRoute] GetByIdCorporateCustomerQuery getByIdCorporateCustomerQuery)
    {
        CorporateCustomerDto result = await Mediator.Send(getByIdCorporateCustomerQuery);
        return Ok(result);
    }

    [HttpGet("ByCustomerId/{CustomerId}")]
    public async Task<IActionResult> GetById(
        [FromRoute] GetByCustomerIdCorporateCustomerQuery getByCustomerIdCorporateCustomerQuery)
    {
        CorporateCustomerDto result = await Mediator.Send(getByCustomerIdCorporateCustomerQuery);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListCorporateCustomerQuery getListCorporateCustomerQuery = new() { PageRequest = pageRequest };
        CorporateCustomerListModel result = await Mediator.Send(getListCorporateCustomerQuery);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateCorporateCustomerCommand createCorporateCustomerCommand)
    {
        CreatedCorporateCustomerDto result = await Mediator.Send(createCorporateCustomerCommand);
        return Created("", result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateCorporateCustomerCommand updateCorporateCustomerCommand)
    {
        UpdatedCorporateCustomerDto result = await Mediator.Send(updateCorporateCustomerCommand);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteCorporateCustomerCommand deleteCorporateCustomerCommand)
    {
        DeletedCorporateCustomerDto result = await Mediator.Send(deleteCorporateCustomerCommand);
        return Ok(result);
    }
}