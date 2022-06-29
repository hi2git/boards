using System;

namespace Boards.Domain.Contracts {
	public abstract record AbstractMsg {

		public AbstractMsg() {}

		public AbstractMsg(Guid id) : base() => this.Id = id;

		public Guid Id { get; set;  }
	}
}
