using System;

using Board.Infrastructure.Jwt.Interfaces;
using Board.Infrastructure.Jwt.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Board.Infrastructure.Jwt.Implementation {
	internal class CookieService : ICookieService {
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly AuthSettings _authOptions;

		public CookieService(IHttpContextAccessor httpContextAccessor, IOptions<AuthSettings> authSettings) {
			_httpContextAccessor = httpContextAccessor;
			_authOptions = authSettings?.Value;
		}

		public void Add(JwtTokenResult token) {
			if (token != null) {
				_httpContextAccessor?.HttpContext?.Response?.Cookies?.Append(AuthCookieName, token.AccessToken, new CookieOptions { HttpOnly = true }); //MaxAge = token.Expires
			}
		}

		//public void Add(string name, string value, bool httpOnly = true) {
		//	if (!string.IsNullOrEmpty(name)) {
		//		_httpContextAccessor?.HttpContext?.Response?.Cookies?.Append(name, value, new CookieOptions { HttpOnly = httpOnly }); //MaxAge = token.Expires
		//	}
		//}

		public string AuthCookieName => $"SID_{_authOptions?.Issuer}";

		public void Remove() => _httpContextAccessor?.HttpContext?.Response?.Cookies?.Delete(AuthCookieName);
	}
}
