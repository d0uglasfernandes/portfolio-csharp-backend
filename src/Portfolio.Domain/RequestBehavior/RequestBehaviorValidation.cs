﻿using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using FluentValidation;
using MediatR;

namespace Portfolio.Domain.RequestBehavior
{
    public class RequestBehaviorValidation<RequestCommand, TResponse> : IPipelineBehavior<RequestCommand, TResponse>
        where RequestCommand : IRequest<TResponse>
    {

        private readonly IValidator<RequestCommand> _validator;

        private readonly IHttpContextAccessor _httpAcessor;

        public RequestBehaviorValidation(IValidator<RequestCommand> validator, IHttpContextAccessor httpContextAccessor)
        {
            this._validator = validator;
            this._httpAcessor = httpContextAccessor;
        }

        public async Task<TResponse> Handle(RequestCommand request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);

            var response = await next();

            return response;
        }
    }
}
