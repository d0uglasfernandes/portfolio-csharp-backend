using System.Threading;
using System.Threading.Tasks;
using Portfolio.Application.Controllers.BaseControllers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MediatR;

namespace Portfolio.Application.Controllers.BaseControllers
{
    public class BaseDeleteController<DeleteEntity, LoggerObject> :
        BaseController<LoggerObject>,
        IBaseDeleteController<DeleteEntity>
        where DeleteEntity : class
        where LoggerObject : class
    {
        public BaseDeleteController(
            IMediator mediator,
            ILogger<LoggerObject> logger
            )
        : base(mediator, logger)
        {
        }

        public async Task<IActionResult> DeleteAsync([FromQuery] DeleteEntity value, CancellationToken cancellationToken)
        {
            await mediator.Send(value, cancellationToken);
            return Ok();
        }
    }
}
