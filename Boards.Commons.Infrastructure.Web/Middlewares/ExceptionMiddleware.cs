using System;
using System.Net;

using FluentValidation;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using System.Text.Json;


namespace Boards.Infrastructure.Web.Middlewares {

	//public static class ExceptionMiddlewareExtensions {

	//	public static void UseExceptionMiddleware(this IApplicationBuilder app) => app.UseMiddleware<ExceptionMiddleware>();
	//}

	internal class ExceptionMiddleware {
		private readonly RequestDelegate _next;
		private readonly ILogger<ExceptionMiddleware> _logger;

		public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger) {
			_logger = logger;
			_next = next;
		}

		public async Task InvokeAsync(HttpContext httpContext) {
			try {
				await _next(httpContext);
			}
			catch (OperationCanceledException e) {
				_logger.LogDebug($"Cancelled operation: {e.Message}");
			}
			catch (Exception ex) {
				//_logger.LogError(ex, $"Something went wrong:");
				await HandleExceptionAsync(httpContext, ex);
				throw;
			}
		}

		private Task HandleExceptionAsync(HttpContext context, Exception ex) {
			context.Response.ContentType = "application/json";

			context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
			var message = $"Внутренняя ошибка сервера: {ex.Message}";

			switch (ex) {
				case ValidationException ve:
					_logger.LogInformation(ex, "Validation message");
					context.Response.StatusCode = (int)HttpStatusCode.NotAcceptable;
					message = ve.Message;
					break;
				case ArgumentException ae:
					_logger.LogInformation(ex, "Argument message");
					context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
					message = ae.Message;
					break;
				case MassTransit.RequestTimeoutException:
					_logger.LogError(ex, $"Request error");
					context.Response.StatusCode = (int)HttpStatusCode.RequestTimeout;
					message = $"Сервис временно недоступен. Попробуйте повторить попытку позже";
					break;
				case MassTransit.MassTransitException ce:
					_logger.LogError(ex, $"MassTransit error");
					context.Response.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
					message = $"Невозможно выполнить запрос: {ce.Message}";
					break;
				default:
					_logger.LogError(ex, $"Something went wrong:");
					break;
			}

			var result = JsonSerializer.Serialize(new { message }); //.SerializeObject(new { message });
			return context.Response.WriteAsync(result);
		}
	}


}
