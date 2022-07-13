using System;

using Boards.Domain.Models;

namespace Boards.Boards.Domain.Models {
	public class Board : Nameable {

		protected Board() { }

		public Board(Guid id, string name, Guid userId) : base(id, name) => this.UserId = userId != default ? userId : throw new ArgumentNullException(nameof(userId));

		#region Props

		public Guid UserId { get; protected set; }

		#endregion

		//#region Navigation

		//public User User { get; protected set; }

		//#endregion



	}
}
