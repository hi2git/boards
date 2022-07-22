using System;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using Serilog.Context;

namespace Boards.Infrastructure.Web.Middlewares {

	internal class CorellationMiddleware {
		private readonly RequestDelegate _next;
		private readonly ILogger<CorellationMiddleware> _log;

		public CorellationMiddleware(RequestDelegate next, ILogger<CorellationMiddleware> log) {
			_next = next;
			_log = log;
		}

		public async Task InvokeAsync(HttpContext httpContext) {
			var id = httpContext.TraceIdentifier;
			using (LogContext.PushProperty("CorrelationId", id))  { 
				await _next(httpContext);
			}
		}

	}


}
