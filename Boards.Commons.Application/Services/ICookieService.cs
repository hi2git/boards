using System;

using Boards.Commons.Domain.DTOs.Jwt;

namespace Boards.Commons.Application.Services {
	public interface ICookieService {

		string AuthCookieName { get; }

		void Add(JwtTokenDTO token);

		void Remove();
	}
}
