using FluentValidation;
using MediatR;

namespace Application.Behaviours
    //Pipeline
    //Esto se ejecuta mucho antes que el mediator redirija el comando
{
    //entra un request--- sale un response
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        //valida todo el contenido que entre en nuestra api
        //se canaliza antes de que entre al manejador principal
         private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        //se ejectua antes del mediator
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (_validators.Any())
            {
                //recopila los errores
                var context = new FluentValidation.ValidationContext<TRequest>(request);
                var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
                var failures=validationResults.SelectMany(r=>r.Errors).Where(f=>f!=null).ToList();
                if (failures.Count != 0)
                    throw new Exceptions.ValidationException(failures);
            }
            return await next();

        }
    }
}
