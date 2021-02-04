using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Domain.DTO.BoardItems {
	public class IdItemDTO : IdItemDTO<BoardItemDTO> { }

	public class IdItemDTO<T> {
		public Guid Id { get; set; }

		public T Item { get; set; }
	}
}
