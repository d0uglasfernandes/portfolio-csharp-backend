using MediatR;
//using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Portfolio.Application.Controllers.BaseControllers
{
    //[Authorize]
    [Route("/api/v1/[controller]")]
    [ApiController]
    public class BaseController<LoggerObject>(
        IMediator mediator,
        ILogger<LoggerObject> logger) : ControllerBase
        where LoggerObject : class
    {
        public IMediator _mediator = mediator;
        public ILogger<LoggerObject> _logger = logger;
    }
}
