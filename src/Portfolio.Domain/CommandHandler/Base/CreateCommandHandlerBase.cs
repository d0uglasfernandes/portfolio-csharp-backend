using System.Threading;
using System.Threading.Tasks;
using Portfolio.Domain.Entities.Base;
using Portfolio.Domain.Interfaces.Repository;
using Portfolio.Domain.Command.Base;
using Portfolio.Domain.Interfaces.DataModule;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Portfolio.Domain.CommandHandler.Base
{
    public class CreateCommandHandlerBase<RequestCommand, EntityBase> 
        : CommandHandlerBase<RequestCommand, CommandBaseResult>, IRequestHandler<RequestCommand, CommandBaseResult> 
            where RequestCommand : IRequest<CommandBaseResult>
            where EntityBase : BaseEntity
    {
        public IRepository<EntityBase> repository;

        public CreateCommandHandlerBase(
            IDataModuleDBPortfolio dataModule,
            IMapper mapper,
            IValidator<RequestCommand> validator,
            IRepository<EntityBase> repository)
        : base(dataModule, mapper, validator)
        {
            this.repository = repository;
        }

        public override async Task<CommandBaseResult> Handle(RequestCommand request, CancellationToken cancellationToken)
        {
            await base.Handle(request, cancellationToken);

            var objEntity = _mapper.Map<EntityBase>(request);
            
            var dbEnitty = await repository.InsertAsync(objEntity);

            return new CommandBaseResult()
            {
                Result = dbEnitty.Id
            };
        }
    }
}
