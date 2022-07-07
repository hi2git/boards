using System;
using System.Collections.Generic;

namespace Board.Domain.Models {
	public class Board : Nameable {

		protected Board() { }

		public Board(Guid id, string name, User user) : base(id, name) {
			this.UserId = user?.Id ?? throw new ArgumentNullException(nameof(user));
		}

		#region Props

		public Guid UserId { get; protected set; }

		#endregion

		#region Navigation

		public User User { get; protected set; }

		#endregion



	}
}
