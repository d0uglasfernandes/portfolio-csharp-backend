using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Portfolio.Application.Controllers.BaseControllers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MediatR;

namespace Portfolio.Application.Controllers.BaseControllers
{
    public class BaseQueryController<QueryEntity, LoggerObject> :
        BaseController<LoggerObject>,
        IBaseQueryController<QueryEntity>
        where QueryEntity : class
        where LoggerObject : class
    {

        public BaseQueryController(
            IMediator mediator,
            ILogger<LoggerObject> logger)
        : base(mediator, logger)
        { }


        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetAsync([FromQuery] QueryEntity query, CancellationToken cancellationToken) =>
            Ok(await mediator.Send(query, cancellationToken));
    }
}
