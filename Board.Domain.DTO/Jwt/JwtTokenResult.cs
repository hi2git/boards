using System;

namespace Board.Domain.DTO.Jwt {
	public class JwtTokenResult {

		public string AccessToken { get; set; }

		public TimeSpan Expires { get; set; }
	}
}
