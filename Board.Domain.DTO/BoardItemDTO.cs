using System;

namespace Board.Domain.DTO {
	public class BoardItemDTO {

		public Guid? Id { get; set; }
		public int OrderNumber { get; set; }

		public string Description { get; set; }
		public string Content { get; set; }
		public bool IsDone { get; set; }

	}
}
