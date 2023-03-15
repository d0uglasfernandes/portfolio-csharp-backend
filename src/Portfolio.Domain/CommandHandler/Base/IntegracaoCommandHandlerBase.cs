using System;
using System.Collections.Generic;
using Portfolio.Domain.Interfaces.DataModule;
using System.Threading.Tasks;
using System.Threading;
using Portfolio.Domain.Entities.Base;
using Portfolio.Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Portfolio.Domain.CommandHandler.Base
{
    public class IntegracaoCommandHandlerBase<RequestCommand, EntityBase>
        : CommandHandlerBase<RequestCommand, bool>, IRequestHandler<RequestCommand, bool> 
            where RequestCommand : IRequest<bool>
            where EntityBase : BaseEntity
    {
        public readonly IMapper mapper; 
        public readonly IDataModuleDBPortfolio dataModule;
        public readonly IRepository<EntityBase> repository;

        public IntegracaoCommandHandlerBase(
            IDataModuleDBPortfolio dataModule,
            IMapper mapper,
            IValidator<RequestCommand> validator,
            IRepository<EntityBase> repository)
        : base(dataModule, mapper, validator)
        {
            this.mapper = mapper;
            this.dataModule = dataModule;
            this.repository = repository;
        }

        public override async Task<bool> Handle(RequestCommand request, CancellationToken cancellationToken)
        {
            await base.Handle(request, cancellationToken);
            return true;
        }

        public virtual async Task<bool> HandleList(List<EntityBase> request, CancellationToken cancellationToken)
        {
            List<EntityBase> registrosUpdate = new List<EntityBase>();
            List<EntityBase> registrosInsert = new List<EntityBase>();

            foreach(var entity in request)
            {
                var dbData = await this.repository.DataSet.FirstOrDefaultAsync(x => x.Id.Equals(entity.Id));

                if (dbData != null)
                {
                    if (!PublicInstancePropertiesEqual(dbData, entity))
                    {
                        entity.Id = dbData.Id;
                        registrosUpdate.Add(entity);
                    }
                }
                else
                {
                    registrosInsert.Add(entity);
                }
            }

            await this.repository.InsertListAsync(registrosInsert);
            await this.repository.UpdateListAsync(registrosUpdate);
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
