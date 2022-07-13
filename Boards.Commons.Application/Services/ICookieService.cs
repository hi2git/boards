using System;

using Board.Domain.DTO.Jwt;

namespace Boards.Commons.Application.Services {
	public interface ICookieService {

		string AuthCookieName { get; }

		void Add(JwtTokenDTO token);

		void Remove();


	}
}
