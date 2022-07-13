using System;
using System.Collections.Generic;
using System.Linq;

using Board.Domain.DTO;

namespace Boards.Domain.Contracts.Users {
	public record UserGetAllMsg : AbstractMsg { }

	public record UserGetAllResponse : AbstractResponse { 
		public IEnumerable<IdNameDTO> Items { get; set; } = Enumerable.Empty<IdNameDTO>();
	}
}
