using System.Threading;
using System.Threading.Tasks;
using Portfolio.Application.Controllers.BaseControllers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MediatR;

namespace Portfolio.Application.Controllers.BaseControllers
{
    public class BasePostController<CommandEntity, LoggerObject> :
        BaseController<LoggerObject>,
        IBasePostController<CommandEntity>
        where CommandEntity : class
        where LoggerObject : class
    {
        public BasePostController(
            IMediator mediator,
            ILogger<LoggerObject> logger
            )
        : base(mediator, logger)
        {
        }

        public async Task<IActionResult> PostAsync([FromBody] CommandEntity value, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(value, cancellationToken);
            return Created("/get", result);
        }

    }
}
