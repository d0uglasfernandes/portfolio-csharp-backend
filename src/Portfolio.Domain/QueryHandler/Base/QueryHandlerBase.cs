using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Portfolio.Domain.Helpers;
using Portfolio.Domain.Interfaces.DataModule;
using Portfolio.Domain.Query.Base;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Portfolio.Domain.QueryHandler.Base
{
    public delegate IQueryable<D> RequestQueryable<D>(BaseQuery request);

    public class QueryHandlerBase<Entity, Request, Response>
        : IRequestHandler<Request, Response> where Request : IRequest<Response>
    {
        
        public readonly IDataModuleDBPortfolio dataModule;

        public readonly IMapper mapper;
        
        public readonly IValidator<Request> _validator;

        public QueryHandlerBase(
            IDataModuleDBPortfolio dataModule,
            IMapper mapper,
            IValidator<Request> validator)
        {
            this.dataModule = dataModule;
            this.mapper = mapper;
            this._validator = validator;
        }

        public RequestQueryable<Entity> OnRequestData { get; set; }

        public virtual async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);

            var tempData = OnRequestData(request as BaseQuery);

            var dataResult = tempData.ToPaginated(request as BaseQuery);

            var dataFinal = await dataResult.ToListAsync();

            return mapper.Map<Response>(dataFinal);
        }
    }
}
