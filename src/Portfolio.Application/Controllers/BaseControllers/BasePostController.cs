using Portfolio.Application.Controllers.BaseControllers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace Portfolio.Application.Controllers.BaseControllers
{
    public class BasePostController<CommandEntity, LoggerObject>(
        IMediator mediator,
        ILogger<LoggerObject> logger
    ) :
        BaseController<LoggerObject>(mediator, logger),
        IBasePostController<CommandEntity>
        where CommandEntity : class
        where LoggerObject : class
    {
        public async Task<IActionResult> PostAsync([FromBody] CommandEntity value, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(value, cancellationToken);
            return Created("/get", result);
        }
    }
}
