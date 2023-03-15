using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Portfolio.Domain.Interfaces.DataModule;
using Portfolio.Domain.Entities.Base;
using Portfolio.Domain.Interfaces.Repository;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Portfolio.Domain.CommandHandler.Base
{
    public delegate Task<EntityBase> RequestRepositoryData<EntityBase, RequestCommand>(RequestCommand request);
    public class UpdateCommandHandlerBase<RequestCommand, EntityBase> 
        : CommandHandlerBase<RequestCommand, bool>, IRequestHandler<RequestCommand, bool> 
            where RequestCommand : IRequest<bool>
            where EntityBase : BaseEntity
    {
        public IRepository<EntityBase> repository;

        public UpdateCommandHandlerBase(
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

            await OnRequestRepositoryData(request);

            return true;
        }

        public static bool PublicInstancePropertiesEqual(EntityBase self, EntityBase to, params string[] ignore) 
        {
            if (self != null && to != null)
            {
                Type type = typeof(EntityBase);
                List<string> ignoreList = new List<string>(ignore);
                foreach (System.Reflection.PropertyInfo pi in type.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance))
                {
                if (!ignoreList.Contains(pi.Name))
                {
                    object selfValue = type.GetProperty(pi.Name).GetValue(self, null);
                    object toValue = type.GetProperty(pi.Name).GetValue(to, null);

                    if (selfValue != toValue && (selfValue == null || !selfValue.Equals(toValue)))
                    {
                        return false;
                    }
                }
                }
                return true;
            }
            return self == to;
        }
    }
}
