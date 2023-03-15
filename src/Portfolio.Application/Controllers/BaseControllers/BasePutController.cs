using System.Threading;
using System.Threading.Tasks;
using Portfolio.Application.Controllers.BaseControllers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MediatR;

namespace Portfolio.Application.Controllers.BaseControllers
{
    public class BasePutController<CommandEntity, LoggerObject> :
        BaseController<LoggerObject>,
        IBasePutController<CommandEntity>
        where CommandEntity : class
        where LoggerObject : class
    {
        public BasePutController(
            IMediator mediator,
            ILogger<LoggerObject> logger
            )
        : base(mediator, logger)
        {
        }

        public async Task<IActionResult> PutAsync([FromBody] CommandEntity value, CancellationToken cancellationToken)
        {
            await mediator.Send(value, cancellationToken);
            return Ok();
        }

    }
}
