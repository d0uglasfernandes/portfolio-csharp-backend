using System;
using System.Collections.Generic;
using Portfolio.Domain.Interfaces.DataModule;
using System.Threading.Tasks;
using System.Threading;
using Portfolio.Domain.Entities.Base;
using Portfolio.Domain.Interfaces.Repository;
using AutoMapper;
using FluentValidation;
using MediatR;
using Portfolio.Domain.Dto.Result;

namespace Portfolio.Domain.CommandHandler.Base
{
    public class IntegracaoCommandHandlerBase<RequestCommand, EntityBase>(
        IDataModuleDBPortfolio dataModule,
        IMapper mapper,
        IValidator<RequestCommand> validator,
        IRepository<EntityBase> repository)
        : CommandHandlerBase<RequestCommand, ResultDto>(dataModule, mapper, validator), IRequestHandler<RequestCommand, ResultDto> 
            where RequestCommand : IRequest<ResultDto>
            where EntityBase : BaseEntity
    {
        public readonly IMapper mapper = mapper; 
        public readonly IDataModuleDBPortfolio dataModule = dataModule;
        public readonly IRepository<EntityBase> repository = repository;

        public override async Task<ResultDto> Handle(RequestCommand request, CancellationToken cancellationToken)
        {
            await base.Handle(request, cancellationToken);
            return new ResultDto()
            {
                IsSuccess = true,
                Result = true
            };
        }

        public virtual async Task<ResultDto> HandleList(List<EntityBase> request, CancellationToken cancellationToken)
        {
            await repository.UpsertListAsync(request);
            
            return new ResultDto()
            {
                IsSuccess = true,
                Result = true
            };
        }

        public static bool PublicInstancePropertiesEqual(EntityBase self, EntityBase to, params string[] ignore)
        {
            if (self != null && to != null)
            {
                Type type = typeof(EntityBase);
                List<string> ignoreList = new(ignore);
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
