using Application.Features.Invoices.Commands.CreateInvoice;
using Application.Features.Invoices.Commands.DeleteInvoice;
using Application.Features.Invoices.Commands.UpdateInvoice;
using Application.Features.Invoices.Dtos;
using Application.Features.Invoices.Models;
using Application.Features.Invoices.Queries.GetByIdInvoice;
using Application.Features.Invoices.Queries.GetListByCustomerInvoice;
using Application.Features.Invoices.Queries.GetListByDatesInvoice;
using Application.Features.Invoices.Queries.GetListInvoice;
using Core.Application.Requests;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InvoicesController : BaseController
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] GetByIdInvoiceQuery request)
    {
        InvoiceDto result = await Mediator.Send(request);
        return Ok(result);
    }

    [HttpGet("ByDates")]
    public async Task<IActionResult> GetById([FromQuery] GetListByDatesInvoiceQuery request)
    {
        InvoiceListModel result = await Mediator.Send(request);
        return Ok(result);
    }

    [HttpGet("ByCustomerId")]
    public async Task<IActionResult> GetById([FromQuery] GetListByCustomerInvoiceQuery request)
    {
        InvoiceListModel result = await Mediator.Send(request);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest request)
    {
        GetListInvoiceQuery getListInvoiceQuery = new() { PageRequest = request };
        InvoiceListModel result = await Mediator.Send(getListInvoiceQuery);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateInvoiceCommand createInvoiceCommand)
    {
        CreatedInvoiceDto result = await Mediator.Send(createInvoiceCommand);
        return Created("", result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateInvoiceCommand updateInvoiceCommand)
    {
        UpdatedInvoiceDto result = await Mediator.Send(updateInvoiceCommand);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteInvoiceCommand deleteInvoiceCommand)
    {
        DeletedInvoiceDto result = await Mediator.Send(deleteInvoiceCommand);
        return Ok(result);
    }
}