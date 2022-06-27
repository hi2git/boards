using System;
using System.Collections.Generic;
using System.Linq;

using Board.Domain.DTO.Posts;

namespace Boards.Domain.Contracts.BoardItems {
	public record BoardItemGetAllResponse {
		public IEnumerable<PostDTO> Items { get; set; }
	}
}
