using System.Threading;
using System.Threading.Tasks;
using Portfolio.Domain.Entities.Base;
using Portfolio.Domain.Interfaces.Repository;
using Portfolio.Domain.Interfaces.DataModule;
using AutoMapper;
using FluentValidation;
using MediatR;
using Portfolio.Domain.Dto.Result;

namespace Portfolio.Domain.CommandHandler.Base
{
    public class CreateCommandHandlerBase<RequestCommand, EntityBase>(
        IDataModuleDBPortfolio dataModule,
        IMapper mapper,
        IValidator<RequestCommand> validator,
        IRepository<EntityBase> repository)
        : CommandHandlerBase<RequestCommand, ResultDto>(dataModule, mapper, validator), IRequestHandler<RequestCommand, ResultDto> 
            where RequestCommand : IRequest<ResultDto>
            where EntityBase : BaseEntity
    {
        public IRepository<EntityBase> repository = repository;

        public override async Task<ResultDto> Handle(RequestCommand request, CancellationToken cancellationToken)
        {
            await base.Handle(request, cancellationToken);

            var objEntity = _mapper.Map<EntityBase>(request);
            
            var dbEntity = await repository.InsertAsync(objEntity);

            return new ResultDto()
            {
                IsSuccess = true,
                Result = dbEntity.Id
            };
        }
    }
}
