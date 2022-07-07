using System;
using System.Linq;

namespace Boards.Domain.Contracts.Boards {
	public record BoardCreateMsg : AbstractMsg {
		public BoardCreateMsg() {}

		public BoardCreateMsg(Guid id, string name) : base(id) => this.Name = name;

		public string Name { get; set; } = string.Empty;
	}

	public record BoardCreateResponse : AbstractResponse { }

}
