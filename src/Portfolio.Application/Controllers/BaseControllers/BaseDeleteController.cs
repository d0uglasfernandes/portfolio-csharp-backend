using Portfolio.Application.Controllers.BaseControllers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace Portfolio.Application.Controllers.BaseControllers
{
    public class BaseDeleteController<DeleteEntity, LoggerObject>(
        IMediator mediator,
        ILogger<LoggerObject> logger
    ) :
        BaseController<LoggerObject>(mediator, logger),
        IBaseDeleteController<DeleteEntity>
        where DeleteEntity : class
        where LoggerObject : class
    {
        public async Task<IActionResult> DeleteAsync([FromQuery] DeleteEntity value, CancellationToken cancellationToken)
        {
            await _mediator.Send(value, cancellationToken);
            return Ok();
        }
    }
}
