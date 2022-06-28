using System;

namespace Boards.Domain.Contracts.Posts {
	public record PostSortedResponse : IResponse {

		public PostSortedResponse() {}

		public PostSortedResponse(string msg) : this() => this.Message = msg;

		public bool IsError => string.IsNullOrEmpty(this.Message);


		public string Message { get; set; }
	}

	public interface IResponse {

		bool IsError { get; }
		
		string Message { get; set; }

	}
}
