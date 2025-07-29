using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BaseApiController : ControllerBase
{ 
    protected readonly IMediator _mediator;
    // protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

    public BaseApiController(IMediator mediator)
    {
        _mediator = mediator;
    }

    protected ActionResult HandleResult<T>(Result<T> result)
    {
        if (!result.IsSuccess && result.Code == 404) return NotFound();

        if (result.IsSuccess && result.Value != null) return Ok(result.Value);

        return BadRequest(result.Error);
    }
}