using System;
using System.Collections.Generic;

using Board.Domain.Enums;

namespace Board.Domain.Models {
	public class Role : Nameable<RoleEnum> {

		protected Role() { }

		public Role(RoleEnum id, string name) : base(id, name) { }

		#region Navigation

		public virtual ICollection<User> Users { get; protected set; } = new HashSet<User>();

		#endregion

	}
}
