using Application.Features.Models.Commands.CreateModel;
using Application.Features.Models.Commands.DeleteModel;
using Application.Features.Models.Commands.UpdateModel;
using Application.Features.Models.Dtos;
using Application.Features.Models.Models;
using Application.Features.Models.Queries.GetByIdModel;
using Application.Features.Models.Queries.GetListModel;
using Application.Features.Models.Queries.GetListModelByDynamic;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ModelsController : BaseController
{
    [HttpGet("{Id}")]
    public async Task<IActionResult> GetById([FromRoute] GetByIdModelQuery getByIdModelQuery)
    {
        ModelDto result = await Mediator.Send(getByIdModelQuery);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListModelQuery getListModelQuery = new() { PageRequest = pageRequest };
        ModelListModel result = await Mediator.Send(getListModelQuery);
        return Ok(result);
    }

    [HttpPost("GetList/ByDynamic")]
    public async Task<IActionResult> GetListByDynamic([FromQuery] PageRequest pageRequest,
                                                      [FromBody] Dynamic? dynamic = null)
    {
        GetListModelByDynamicQuery getListModelByDynamicQuery = new() { PageRequest = pageRequest, Dynamic = dynamic };
        ModelListModel result = await Mediator.Send(getListModelByDynamicQuery);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateModelCommand createModelCommand)
    {
        CreatedModelDto result = await Mediator.Send(createModelCommand);
        return Created("", result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateModelCommand updateModelCommand)
    {
        UpdatedModelDto result = await Mediator.Send(updateModelCommand);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteModelCommand deleteModelCommand)
    {
        DeletedModelDto result = await Mediator.Send(deleteModelCommand);
        return Ok(result);
    }
}