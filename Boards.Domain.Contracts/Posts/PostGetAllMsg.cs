using System;
using System.Collections.Generic;
using System.Linq;

namespace Boards.Domain.Contracts.Posts {
	public record PostGetAllMsg { 
		public Guid Id { get; set; }
	}
}
