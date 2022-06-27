using System;
using System.Text;

using FluentValidation;
using FluentValidation.Results;

using MediatR;

namespace Boards.Infrastructure.Web.Middlewares {
	public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse> {
		private readonly IEnumerable<IValidator<TRequest>> _validators;

		public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators) => _validators = validators ?? new List<IValidator<TRequest>>();

		public async Task<TResponse> Handle(TRequest request, CancellationToken token, RequestHandlerDelegate<TResponse> next) {
			var results = new List<ValidationResult>();
			foreach (var validator in _validators) {
				results.Add(await validator.ValidateAsync(request, token));
			}

			var errors = results.SelectMany(result => result.Errors).Where(error => error != null);

			if (errors.Any()) {
				var errorBuilder = new StringBuilder();

				foreach (var error in errors) {
					errorBuilder.AppendLine(error.ErrorMessage);
				}


				throw new ValidationException(errorBuilder.ToString());
			}

			return await next();
		}
	}
}
