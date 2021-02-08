using System;
using System.Collections.Generic;

using Board.Domain.Enums;

namespace Board.Domain.Models {
	public class User : Nameable {

		protected User() { }

		public User(Guid id, string name, string passwordHash, Role role, string email) : base(id, name) {
			this.Password = !string.IsNullOrEmpty(passwordHash) ? passwordHash : throw new ArgumentNullException(nameof(passwordHash));
			this.RoleId = role?.Id ?? throw new ArgumentNullException(nameof(role));
			this.Role = role;
			this.Email = !string.IsNullOrWhiteSpace(email) ? email : throw new ArgumentNullException(nameof(email));
		}

		#region Props

		public RoleEnum RoleId { get; protected set; }

		public string Password { get; set; }
		public string Email { get; set; }

		#endregion

		#region Navigation

		public Role Role { get; protected set; }


		public ICollection<Board> Boards { get; protected set; } = new HashSet<Board>();


		#endregion
	}
}
