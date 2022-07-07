﻿using System;
using System.Linq;

using Board.Domain.DTO;

namespace Boards.Domain.Contracts.Boards {
	public record BoardUpdateMsg : AbstractMsg {
		public BoardUpdateMsg() { }

		public BoardUpdateMsg(IdNameDTO item, Guid userId) : this(item.Id, item.Name, userId) { }

		public BoardUpdateMsg(Guid id, string name, Guid userId) : base(id) {
			this.Name = name;
			this.UserId = userId;
		}

		public string Name { get; set; } = string.Empty;
		public Guid UserId { get; }
	}

	public record BoardUpdateResponse : AbstractResponse { }
}
