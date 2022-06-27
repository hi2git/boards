using System;

namespace Board.Domain.DTO.Posts {
	public class IdItemDTO : IdItemDTO<PostDTO> { }

	public class IdItemDTO<T> {
		public Guid Id { get; set; }

		public T Item { get; set; }
	}
}
