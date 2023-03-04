using Application.Features.Invoices.Commands.Create;
using Application.Features.Invoices.Commands.Delete;
using Application.Features.Invoices.Commands.Update;
using Application.Features.Invoices.Queries.GetById;
using Application.Features.Invoices.Queries.GetList;
using Application.Features.Invoices.Queries.GetListByCustomer;
using Application.Features.Invoices.Queries.GetListByDates;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InvoicesController : BaseController
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] GetByIdInvoiceQuery request)
    {
        GetByIdInvoiceResponse result = await Mediator.Send(request);
        return Ok(result);
    }

    [HttpGet("ByDates")]
    public async Task<IActionResult> GetByDates([FromQuery] GetListByDatesInvoiceQuery request)
    {
        GetListResponse<GetListByDatesInvoiceListItemDto> result = await Mediator.Send(request);
        return Ok(result);
    }

    [HttpGet("ByCustomerId")]
    public async Task<IActionResult> GetByCustomerId([FromQuery] GetListByCustomerInvoiceQuery request)
    {
        GetListResponse<GetListByCustomerInvoiceListItemDto> result = await Mediator.Send(request);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest request)
    {
        GetListInvoiceQuery getListInvoiceQuery = new() { PageRequest = request };
        GetListResponse<GetListInvoiceListItemDto> result = await Mediator.Send(getListInvoiceQuery);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateInvoiceCommand createInvoiceCommand)
    {
        CreatedInvoiceResponse result = await Mediator.Send(createInvoiceCommand);
        return Created("", result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateInvoiceCommand updateInvoiceCommand)
    {
        UpdatedInvoiceResponse result = await Mediator.Send(updateInvoiceCommand);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteInvoiceCommand deleteInvoiceCommand)
    {
        DeletedInvoiceResponse result = await Mediator.Send(deleteInvoiceCommand);
        return Ok(result);
    }
}