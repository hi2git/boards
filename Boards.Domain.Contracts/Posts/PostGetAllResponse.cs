using System;
using System.Collections.Generic;
using System.Linq;

using Board.Domain.DTO.Posts;

namespace Boards.Domain.Contracts.Posts {
	public record PostGetAllResponse {
		public IEnumerable<PostDTO> Items { get; set; }
	}
}
