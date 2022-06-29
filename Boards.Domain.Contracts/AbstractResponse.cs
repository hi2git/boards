using System;

namespace Boards.Domain.Contracts {
	public abstract record AbstractResponse : IResponse {

		public AbstractResponse() { }

		public bool IsError => !string.IsNullOrEmpty(this.Message);

		public string Message { get; set; } = string.Empty;

	}

	public interface IResponse {

		bool IsError { get; }

		string Message { get; set; }

	}
}
