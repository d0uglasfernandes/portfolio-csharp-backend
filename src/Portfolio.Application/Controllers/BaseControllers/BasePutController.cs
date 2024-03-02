using Portfolio.Application.Controllers.BaseControllers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace Portfolio.Application.Controllers.BaseControllers
{
    public class BasePutController<CommandEntity, LoggerObject>(
        IMediator mediator,
        ILogger<LoggerObject> logger
    ) :
        BaseController<LoggerObject>(mediator, logger),
        IBasePutController<CommandEntity>
        where CommandEntity : class
        where LoggerObject : class
    {
        public async Task<IActionResult> PutAsync([FromBody] CommandEntity value, CancellationToken cancellationToken)
        {
            await _mediator.Send(value, cancellationToken);
            return Ok();
        }
    }
}
