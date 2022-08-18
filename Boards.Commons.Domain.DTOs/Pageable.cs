using System;
using System.Collections.Generic;
using System.Linq;

namespace Boards.Commons.Domain.DTOs {
	public record Pageable<T> (IEnumerable<T> Items, int Total);
	
}
