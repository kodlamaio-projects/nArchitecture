using Application.Features.FindeksCreditRates.Commands.Create;
using Application.Features.FindeksCreditRates.Commands.Delete;
using Application.Features.FindeksCreditRates.Commands.Update;
using Application.Features.FindeksCreditRates.Commands.UpdateByUserIdFromService;
using Application.Features.FindeksCreditRates.Commands.UpdateFromService;
using Application.Features.FindeksCreditRates.Queries.GetByCustomerIdFindeksCreditRate;
using Application.Features.FindeksCreditRates.Queries.GetByIdFindeksCreditRate;
using Application.Features.FindeksCreditRates.Queries.GetListFindeksCreditRate;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers.Dtos;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FindeksCreditRatesController : BaseController
{
    [HttpGet("{Id}")]
    public async Task<IActionResult> GetById([FromRoute] GetByIdFindeksCreditRateQuery getByIdFindeksCreditRateQuery)
    {
        GetByIdFindeksCreditRateResponse result = await Mediator.Send(getByIdFindeksCreditRateQuery);
        return Ok(result);
    }

    [HttpGet("ByCustomerId/{CustomerId}")]
    public async Task<IActionResult> GetById([FromRoute] GetByCustomerIdFindeksCreditRateQuery getByCustomerIdFindeksCreditRateQuery)
    {
        GetByCustomerIdFindeksCreditRateResponse result = await Mediator.Send(getByCustomerIdFindeksCreditRateQuery);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListFindeksCreditRateQuery getListFindeksCreditRateQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListFindeksCreditRateListItemDto> result = await Mediator.Send(getListFindeksCreditRateQuery);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateFindeksCreditRateCommand createFindeksCreditRateCommand)
    {
        CreatedFindeksCreditRateResponse result = await Mediator.Send(createFindeksCreditRateCommand);
        return Created(uri: "", result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateFindeksCreditRateCommand updateFindeksCreditRateCommand)
    {
        UpdatedFindeksCreditRateResponse result = await Mediator.Send(updateFindeksCreditRateCommand);
        return Ok(result);
    }

    [HttpPut("FromService")]
    public async Task<IActionResult> UpdateFromService(
        [FromBody] UpdateFindeksCreditRateFromServiceCommand findeksCreditRateFromServiceCommand
    )
    {
        UpdateFindeksCreditRateFromServiceResponse result = await Mediator.Send(findeksCreditRateFromServiceCommand);
        return Ok(result);
    }

    [HttpPut("ByAuth/FromService")]
    public async Task<IActionResult> UpdateByAuthFromService([FromBody] UpdateByAuthFromServiceRequestDto updateByAuthFromServiceRequestDto)
    {
        UpdateByUserIdFindeksCreditRateFromServiceCommand updateByUserIdFindeksCreditRateFromServiceCommand =
            new() { UserId = getUserIdFromRequest(), IdentityNumber = updateByAuthFromServiceRequestDto.IdentityNumber };

        UpdateByUserIdFindeksCreditRateFromServiceResponse result = await Mediator.Send(updateByUserIdFindeksCreditRateFromServiceCommand);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteFindeksCreditRateCommand deleteFindeksCreditRateCommand)
    {
        DeletedFindeksCreditRateResponse result = await Mediator.Send(deleteFindeksCreditRateCommand);
        return Ok(result);
    }
}
