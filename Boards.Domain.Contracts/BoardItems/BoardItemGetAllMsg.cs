using System;
using System.Collections.Generic;
using System.Linq;

namespace Boards.Domain.Contracts.BoardItems {
	public record BoardItemGetAllMsg {
		public Guid Id { get; set; }
	}
}
