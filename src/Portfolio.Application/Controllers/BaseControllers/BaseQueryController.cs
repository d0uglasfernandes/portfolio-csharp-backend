using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Portfolio.Application.Controllers.BaseControllers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MediatR;

namespace Portfolio.Application.Controllers.BaseControllers
{
    public class BaseQueryController<QueryEntity, LoggerObject>(
        IMediator mediator,
        ILogger<LoggerObject> logger
    ) :
        BaseController<LoggerObject>(mediator, logger),
        IBaseQueryController<QueryEntity>
        where QueryEntity : class
        where LoggerObject : class
    {
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetAsync([FromQuery] QueryEntity query, CancellationToken cancellationToken) =>
            Ok(await _mediator.Send(query, cancellationToken));
    }
}
