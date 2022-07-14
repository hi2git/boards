using System;
using System.Collections.Generic;
using System.Linq;

using Boards.Commons.Domain.DTOs.Posts;

namespace Boards.Domain.Contracts.Posts {
	public record PostSortAllMsg : AbstractMsg {

		public IEnumerable<PostDTO> Items { get; set;  } = Enumerable.Empty<PostDTO>();

	}

	public record PostSortedResponse : AbstractResponse { }
}
