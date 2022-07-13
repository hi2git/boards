using System;

using Boards.Domain.Models;

namespace Boards.Posts.Domain.Models {
	public class Post : Entity {

		protected Post() { }

		public Post(Guid id, Guid boardId, int orderNumber, string? description = null) : base(id) {
			this.BoardId = boardId != default ? boardId : throw new ArgumentNullException(nameof(boardId));
			this.OrderNumber = orderNumber;
			this.Description = description;
		}

		#region Props

		public Guid BoardId { get; set; }

		public int OrderNumber { get; set; }

		public string? Description { get; set; }

		public bool IsDone { get; set; }

		#endregion
	}
}
