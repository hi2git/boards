using System;
using System.Net;

using FluentValidation;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using System.Text.Json;
using Serilog.Context;

namespace Boards.Infrastructure.Web.Middlewares {

	//public static class ExceptionMiddlewareExtensions {

	//	public static void UseExceptionMiddleware(this IApplicationBuilder app) => app.UseMiddleware<ExceptionMiddleware>();
	//}

	internal class CorellationMiddleware {
		private readonly RequestDelegate _next;
		private readonly ILogger<CorellationMiddleware> _log;

		public CorellationMiddleware(RequestDelegate next, ILogger<CorellationMiddleware> log) {
			_next = next;
			_log = log;
		}

		public async Task InvokeAsync(HttpContext httpContext) {
			var id = httpContext.TraceIdentifier;
			using (LogContext.PushProperty("CorellationId", id))  { 
				_log.LogDebug($"Starting request: {id}");
				await _next(httpContext);
			}
		}

	}


}
