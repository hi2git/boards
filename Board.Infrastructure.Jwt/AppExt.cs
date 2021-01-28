using System;

using Board.Infrastructure.Jwt.Middlewares;

using Microsoft.AspNetCore.Builder;

namespace Board.Infrastructure.Jwt {
	public static class AppExt {

		public static IApplicationBuilder UseSecureJwt(this IApplicationBuilder builder) => builder.UseMiddleware<JwtMiddleware>();

	}
}
