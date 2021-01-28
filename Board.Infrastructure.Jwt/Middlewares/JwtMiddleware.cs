using System;
using System.Threading.Tasks;

using Board.Infrastructure.Jwt.Interfaces;

using Microsoft.AspNetCore.Http;

namespace Board.Infrastructure.Jwt.Middlewares {
	internal class JwtMiddleware {
		private readonly RequestDelegate _next;

		public JwtMiddleware(RequestDelegate next) => _next = next;

		public async Task InvokeAsync(HttpContext context) {
			if (context != null) {
				var cookieService = (ICookieService)context.RequestServices.GetService(typeof(ICookieService));

				var token = context.Request.Cookies[cookieService.AuthCookieName];

				if (!string.IsNullOrEmpty(token))
					context.Request.Headers.Add("Authorization", "Bearer " + token);

				context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
				context.Response.Headers.Add("X-Xss-Protection", "1");
				context.Response.Headers.Add("X-Frame-Options", "SAMEORIGIN");
			}
			await _next(context);
		}

	}
}
