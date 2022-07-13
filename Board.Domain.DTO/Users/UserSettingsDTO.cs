using System;

namespace Boards.Commons.Domain.DTOs.Users {
	public class UserSettingsDTO {

		public string OldPassword { get; set; }
		public string NewPassword { get; set; }
		public bool IsPasswordChanged { get; set; }

	}
}
