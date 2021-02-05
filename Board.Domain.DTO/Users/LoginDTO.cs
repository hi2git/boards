using System;

namespace Board.Domain.DTO.Users {
	public class LoginDTO {

		public string Login { get; set; }
		public string Password { get; set; }

		public string Email { get; set; }

		public string CaptchaId { get; set; }

		public string CaptchaCode { get; set; }

	}
}
