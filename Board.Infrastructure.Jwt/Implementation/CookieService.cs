using System;

using Board.Domain.DTO.Jwt;

using Boards.Commons.Application.Services;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Board.Infrastructure.Jwt.Implementation {
	internal class CookieService : ICookieService {
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly AuthSettings _auth;

		public CookieService(IHttpContextAccessor httpContextAccessor, IOptions<AuthSettings> auth) {
			_httpContextAccessor = httpContextAccessor;
			_auth = auth.Value;
		}

		public string AuthCookieName => $"SID_{_auth.Issuer}";

		public void Add(JwtTokenResult token) {
			token = token ?? throw new ArgumentNullException(nameof(token));
			_httpContextAccessor?.HttpContext?.Response?.Cookies?.Append(AuthCookieName, token.AccessToken, new CookieOptions { HttpOnly = true }); //MaxAge = token.Expires
		}

		public void Remove() => _httpContextAccessor?.HttpContext?.Response?.Cookies?.Delete(AuthCookieName);
	}
}
