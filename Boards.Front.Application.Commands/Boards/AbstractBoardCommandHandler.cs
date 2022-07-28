using System;
using System.Linq;

using Boards.Commons.Application;
using Boards.Commons.Application.Services;
using Boards.Domain.Contracts;

using MediatR;

namespace Boards.Front.Application.Commands.Boards {
	internal abstract class AbstractBoardCommandHandler<TRequest, TMsg, TResponse> : AbstractHandler<TRequest, TMsg, TResponse>
		where TRequest : IRequest
		where TMsg : AbstractMsg
		where TResponse : AbstractResponse {
		private readonly IUserManager _userMgr;

		protected AbstractBoardCommandHandler(IClient<TMsg, TResponse> client, ICacheService cache, IUserManager userMgr) : base(client, cache) => _userMgr = userMgr;

		protected override string CacheKey(TRequest _) => _userMgr.UserKey;

		protected Guid UserId => _userMgr.CurrentUserId;

	}
}
