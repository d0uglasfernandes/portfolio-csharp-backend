using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using Portfolio.Domain.Dto.ValidationError;

namespace Portfolio.Domain.RequestBehavior
{
    public class RequestBehaviorValidation<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators = validators;

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);

            var validationFailures = await Task.WhenAll(
                _validators.Select(validator => validator.ValidateAsync(context)));

            var errors = validationFailures
                .Where(validationResult => !validationResult.IsValid)
                .SelectMany(validationResult => validationResult.Errors)
                .Select(validationFailure => new ValidationErrorDto
                {
                    Property = validationFailure.PropertyName,
                    ErrorMessage = validationFailure.ErrorMessage
                })
                .ToList();

            if (errors.Count != 0)
            {
                throw new Exceptions.ValidationException(errors);
            }

            var response = await next();

            return response;
        }
    }
}
