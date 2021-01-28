using System;
using System.Collections.Generic;

using Board.Domain.Enums;

namespace Board.Domain.Models {
	public class User : Nameable {

		protected User() { }

		public User(Guid id, string name) : base(id, name) { }

		#region Props

		public RoleEnum RoleId { get; protected set; }

		public string Password { get; set; }

		#endregion

		#region Navigation

		public Role Role { get; protected set; }

		public ICollection<BoardItem> BoardItems { get; protected set; }

		#endregion
	}
}
