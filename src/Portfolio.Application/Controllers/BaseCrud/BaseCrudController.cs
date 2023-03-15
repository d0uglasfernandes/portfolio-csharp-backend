using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Portfolio.Application.Controllers.BaseControllers;
using Portfolio.Application.Controllers.BaseControllers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MediatR;

namespace Portfolio.Application.Controllers.BaseCrud
{

    public class BaseCrudController<Query, CreateCommand, UpdateCommand, DeleteCommand> 
        : BaseController<BaseCrudController<Query, CreateCommand, UpdateCommand, DeleteCommand>>
        where Query : class
        where CreateCommand : class
        where UpdateCommand : class
        where DeleteCommand : class
    {

        private readonly IBaseQueryController<Query> _baseQueryController;
        private readonly IBasePostController<CreateCommand> _basePostController;
        private readonly IBasePutController<UpdateCommand> _basePutController;
        private readonly IBaseDeleteController<DeleteCommand> _baseDeleteController;

        public BaseCrudController(
            IMediator mediator,
            ILogger<BaseCrudController<Query, CreateCommand, UpdateCommand, DeleteCommand>> logger
        ) : base(mediator, logger)
        {
            _baseQueryController =
                new BaseQueryController<Query, BaseCrudController<Query, CreateCommand, UpdateCommand, DeleteCommand>>(mediator, logger);

            _basePostController =
                new BasePostController<CreateCommand, BaseCrudController<Query, CreateCommand, UpdateCommand, DeleteCommand>>(mediator, logger);

            _basePutController =
                new BasePutController<UpdateCommand, BaseCrudController<Query, CreateCommand, UpdateCommand, DeleteCommand>>(mediator, logger);

            _baseDeleteController =
                new BaseDeleteController<DeleteCommand, BaseCrudController<Query, CreateCommand, UpdateCommand, DeleteCommand>>(mediator, logger);
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetAsync([FromQuery] Query query, CancellationToken cancellationToken) =>
            await _baseQueryController.GetAsync(query, cancellationToken);

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> PostAsync([FromBody] CreateCommand value, CancellationToken cancellationToken) =>
            await _basePostController.PostAsync(value, cancellationToken);

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateCommand value, CancellationToken cancellationToken) =>
            await _basePutController.PutAsync(value, cancellationToken);

        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> Delete([FromQuery] DeleteCommand value, CancellationToken cancellationToken) =>
            await _baseDeleteController.DeleteAsync(value, cancellationToken);

    }
}
