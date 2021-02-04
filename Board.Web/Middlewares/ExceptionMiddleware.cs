using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using FluentValidation;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

namespace Board.Web.Middlewares {

	public static class ExceptionMiddlewareExtensions {

		public static void UseExceptionMiddleware(this IApplicationBuilder app) => app.UseMiddleware<ExceptionMiddleware>();
	}

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
			catch (Exception ex) {
				_logger.LogError($"Something went wrong: {ex}");
				await HandleExceptionAsync(httpContext, ex);
				throw;
			}
		}

		private static Task HandleExceptionAsync(HttpContext context, Exception exception) {
			context.Response.ContentType = "application/json";

			context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
			var message = $"Внутренняя ошибка сервера: {exception.Message}";

			switch (exception) {
				case ValidationException ve:
					context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
					message = ve.Message;
					break;
				case ArgumentException ae:
					context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
					message = ae.Message;
					break;
			}

			var result = JsonConvert.SerializeObject(new { message });
			return context.Response.WriteAsync(result);
		}
	}


}
