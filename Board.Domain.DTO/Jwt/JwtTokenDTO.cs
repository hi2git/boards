using System;

namespace Boards.Commons.Domain.DTOs.Jwt {
	public class JwtTokenDTO {

		public string AccessToken { get; set; }

		public TimeSpan Expires { get; set; }
	}
}
