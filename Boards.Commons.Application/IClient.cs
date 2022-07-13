using System;
using System.Linq;

using Boards.Domain.Contracts;

namespace Boards.Commons.Application {
	public interface IClient<TMsg, TResponse> where TMsg : AbstractMsg where TResponse : AbstractResponse {

		//Task<TResponse> Get(TMsg msg, CancellationToken token);

		Task<TResponse> Send(TMsg msg, CancellationToken token);

	}
}
