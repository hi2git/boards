using System;

namespace Board.Domain.DTO.Posts {
	public class PostDTO : IdOrderableDTO {

		public string Description { get; set; }
		public string Content { get; set; }
		public bool IsDone { get; set; }

	}

	public class IdOrderableDTO {
		public Guid? Id { get; set; }

		public int OrderNumber { get; set; }
	}
}
