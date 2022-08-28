using FluentValidation;
using MediatR;

namespace Eskisehirspor.Application.Common.Behaviours
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationResults = await Task.WhenAll(_validators.Select(m => m.ValidateAsync(context, cancellationToken)));
                var failures = validationResults.SelectMany(m => m.Errors).Where(m => m != null).ToList();
                if (failures.Any())
                    throw new ValidationException(failures);
            }
            return await next();
        }
    }
}
