using System;

namespace Boards.Commons.Domain.DTOs.Posts {
	public class IdItemDTO : IdItemDTO<PostDTO> { }

	public class IdItemDTO<T> {
		public Guid Id { get; set; }

		public T Item { get; set; }
	}
}
