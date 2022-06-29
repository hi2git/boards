using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Boards.Domain.Contracts;

using MassTransit;

using MediatR;

namespace Boards.Application.Commands {
	internal class AbstractHandler<TRequest, TMsg, TResponse> : IRequestHandler<TRequest> 
		where TRequest: IRequest 
		where TMsg : class 
		where TResponse : class, IResponse
	{
		private readonly IRequestClient<TMsg> _client;

		public AbstractHandler(IRequestClient<TMsg> client) => _client = client;

		public async Task<Unit> Handle(TRequest request, CancellationToken token) {
			var response = await _client.GetResponse<TResponse>(request, token);
			return ThrowIfError(response.Message);
		}

		private static Unit ThrowIfError(IResponse response) => !response.IsError ? Unit.Value : throw new CommandException(response.Message);
	}
}
