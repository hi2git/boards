using System;
using System.Collections.Generic;
using System.Linq;

using Board.Domain.DTO.BoardItems;

namespace Boards.Domain.Contracts.BoardItems {
	public record BoardItemGetAllResponse {
		public IEnumerable<BoardItemDTO> Items { get; set; }
	}
}
