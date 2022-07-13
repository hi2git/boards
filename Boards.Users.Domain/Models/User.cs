using System;
using System.Collections.Generic;

using Board.Domain.Enums;
using Board.Domain.Models;

namespace Boards.Users.Domain.Models {
	public class User : Nameable {

		protected User() { }

		public User(Guid id, string name, string passwordHash, RoleEnum role, string email) : base(id, name) {
			this.Password = !string.IsNullOrEmpty(passwordHash) ? passwordHash : throw new ArgumentNullException(nameof(passwordHash));
			this.RoleId = role;
			this.Email = !string.IsNullOrWhiteSpace(email) ? email : throw new ArgumentNullException(nameof(email));
		}

		#region Props

		public RoleEnum RoleId { get; protected set; }

		public string Password { get; set; }

		public string Email { get; set; }

		#endregion
	}
}
