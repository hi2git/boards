using System;
using System.Linq;

using Boards.Commons.Application;
using Boards.Commons.Application.Services;
using Boards.Domain.Contracts;

using MediatR;

namespace Boards.Front.Application.Commands.Posts {
	internal abstract class AbstractPostCommandHandler<TRequest, TMsg, TResponse> : AbstractHandler<TRequest, TMsg, TResponse> 
		where TRequest : AbstractMsg, IRequest
		where TMsg : AbstractMsg
		where TResponse : AbstractResponse {

		protected AbstractPostCommandHandler(IClient<TMsg, TResponse> client, ICacheService cache) : base(client, cache) { }

		protected override string CacheKey(TRequest request) => $"board_{request.Id}";

	}
}
