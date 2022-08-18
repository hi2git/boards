using System;
using System.Collections.Generic;
using System.Linq;

namespace Boards.Commons.Domain.DTOs.Posts {
	public record PostFilter {

		public Guid BoardId { get; set; }

		public int Index { get; set; } = 1;

		public PageSizeEnum Size { get; set; } = PageSizeEnum.S;

		public int RealIndex => this.Index - 1;

		public int SizeInt => (int)this.Size;

	}
}
