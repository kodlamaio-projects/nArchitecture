using Application.Features.AdditionalServices.Commands.CreateAdditionalService;
using Application.Features.AdditionalServices.Commands.DeleteAdditionalService;
using Application.Features.AdditionalServices.Commands.UpdateAdditionalService;
using Application.Features.AdditionalServices.Dtos;
using Application.Features.AdditionalServices.Models;
using Application.Features.AdditionalServices.Queries.GetByIdAdditionalService;
using Application.Features.AdditionalServices.Queries.GetListAdditionalService;
using Core.Application.Requests;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AdditionalServicesController : BaseController
{
    [HttpGet("{Id}")]
    public async Task<IActionResult> GetById([FromRoute] GetByIdAdditionalServiceQuery getByIdAdditionalServiceQuery)
    {
        AdditionalServiceDto result = await Mediator.Send(getByIdAdditionalServiceQuery);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListAdditionalServiceQuery getListAdditionalServiceQuery = new() { PageRequest = pageRequest };
        AdditionalServiceListModel result = await Mediator.Send(getListAdditionalServiceQuery);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateAdditionalServiceCommand createAdditionalServiceCommand)
    {
        CreatedAdditionalServiceDto result = await Mediator.Send(createAdditionalServiceCommand);
        return Created("", result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateAdditionalServiceCommand updateAdditionalServiceCommand)
    {
        UpdatedAdditionalServiceDto result = await Mediator.Send(updateAdditionalServiceCommand);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteAdditionalServiceCommand deleteAdditionalServiceCommand)
    {
        DeletedAdditionalServiceDto result = await Mediator.Send(deleteAdditionalServiceCommand);
        return Ok(result);
    }
}