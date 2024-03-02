using System.Threading;
using System.Threading.Tasks;
using Portfolio.Domain.Interfaces.DataModule;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Portfolio.Domain.CommandHandler.Base
{
    public class CommandHandlerBase<RequestCommand, Response>(
        IDataModuleDBPortfolio dataModule,
        IMapper mapper,
        IValidator<RequestCommand> validator) : IRequestHandler<RequestCommand, Response> where RequestCommand : IRequest<Response>
    {
        public readonly IDataModuleDBPortfolio _dataModule = dataModule;
        public readonly IMapper _mapper = mapper;
        public readonly IValidator<RequestCommand> _validator = validator;

        public virtual async Task<Response> Handle(RequestCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);

            return default;
        }
    }
}
