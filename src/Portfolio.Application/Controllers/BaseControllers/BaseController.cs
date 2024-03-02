using MediatR;
//using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Portfolio.Application.Controllers.BaseControllers
{
    //[Authorize]
    [Route("/api/v1/[controller]")]
    [ApiController]
    public class BaseController<LoggerObject> : ControllerBase
        where LoggerObject : class
    {
        public IMediator mediator;
        public ILogger<LoggerObject> logger;

        public BaseController(
            IMediator mediator,
            ILogger<LoggerObject> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
        }

    }
}
