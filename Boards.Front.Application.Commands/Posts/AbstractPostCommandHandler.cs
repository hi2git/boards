using System;
using System.Linq;
//using System.Threading.Tasks;

using Boards.Commons.Application;
//using Boards.Commons.Application.Services;
using Boards.Domain.Contracts;

using MediatR;

namespace Boards.Front.Application.Commands.Posts {
	internal abstract class AbstractPostCommandHandler<TRequest, TMsg, TResponse> : AbstractHandler<TRequest, TMsg, TResponse> 
		where TRequest : AbstractMsg, IRequest
		where TMsg : AbstractMsg
		where TResponse : AbstractResponse {

		protected AbstractPostCommandHandler(IClient<TMsg, TResponse> client/*, ICacheService cache*/) : base(client/*, cache*/) { }

		//protected override string CacheKey(TRequest request) => $"board_{request.Id}"; //TODO: remove as unnecessary

		//protected override Task InvalidateCache(TRequest request) => Cache.RemoveBoard(request.Id);

	}
}
