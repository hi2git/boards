using System;
using System.Collections.Generic;
using System.Linq;

using Boards.Commons.Domain.DTOs;

namespace Boards.Domain.Contracts.Boards {
	public record BoardGetAllMsg : AbstractMsg {
		public BoardGetAllMsg() {}

		public BoardGetAllMsg(Guid id) : base(id) {}
	}

	public record BoardGetAllResponse : AbstractResponse {
		public IEnumerable<IdNameDTO> Items { get; set; } = Enumerable.Empty<IdNameDTO>();
	}
}
