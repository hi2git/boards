using System;

namespace Board.Domain.Models {
	public class BoardItem : Entity {

		protected BoardItem() { }

		public BoardItem(Guid id, Guid userId, int orderNumber, string description = null) : base(id) {
			this.UserId = userId;
			OrderNumber = orderNumber;
			Description = description;
		}

		#region Props

		public Guid UserId { get; protected set; }

		public int OrderNumber { get; set; }

		public string Description { get; set; }

		public bool IsDone { get; set; }

		#endregion

		#region Navigation

		public User User { get; protected set; }

		#endregion
	}
}
