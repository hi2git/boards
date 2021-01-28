using System;

namespace Board.Infrastructure.Jwt.Models {
	internal class JwtTokenResult {

		public string AccessToken { get; set; }

		public TimeSpan Expires { get; set; }
	}
}
