using System;

using Board.Infrastructure.Jwt.Models;

namespace Board.Infrastructure.Jwt.Interfaces {
	internal interface ICookieService {

		string AuthCookieName { get; }

		void Add(JwtTokenResult token);

		//void Add(string name, string value, bool httpOnly = true);

		void Remove();


	}
}
