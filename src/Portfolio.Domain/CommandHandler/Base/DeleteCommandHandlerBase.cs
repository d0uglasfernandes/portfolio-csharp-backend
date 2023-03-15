using System.Threading;
using System.Threading.Tasks;
using Portfolio.Domain.Entities.Base;
using Portfolio.Domain.Interfaces.DataModule;
using Portfolio.Domain.Interfaces.Repository;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Portfolio.Domain.CommandHandler.Base
{
    public class DeleteCommandHandlerBase<RequestCommand, EntityBase>
        : CommandHandlerBase<RequestCommand, bool>, IRequestHandler<RequestCommand, bool>
            where RequestCommand : IRequest<bool>
            where EntityBase : BaseEntity
    {

        public IRepository<EntityBase> repository;

        public DeleteCommandHandlerBase(
            IDataModuleDBPortfolio dataModule,
            IMapper mapper,
            IValidator<RequestCommand> validator,
            IRepository<EntityBase> repository)
        : base(dataModule, mapper, validator)
        {
            this.repository = repository;
        }
        
        public RequestRepositoryData<EntityBase, RequestCommand> OnRequestRepositoryData { get; set; }

        public override async Task<bool> Handle(RequestCommand request, CancellationToken cancellationToken)
        {
            await base.Handle(request, cancellationToken);

            var objEntity = _mapper.Map<EntityBase>(request);

            if (OnRequestRepositoryData != null)
            {
                await OnRequestRepositoryData(request);
            }

            await this.repository.DeleteAsync(objEntity.Id);

            return true;
        }

    }
}
