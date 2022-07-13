using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Board.Domain.DTO;

using Boards.Domain.Contracts.Users;

using FluentValidation;

using MassTransit;

using MediatR;

namespace Boards.Application.Queries.Users {
	public record UserGetAllQuery : UserGetAllMsg, IRequest<IEnumerable<IdNameDTO>> { }

	public class UserGetAllQueryValidator : AbstractValidator<UserGetAllQuery> { }

	internal class UserGetAllQueryHandler : IRequestHandler<UserGetAllQuery, IEnumerable<IdNameDTO>> {
		private readonly IRequestClient<UserGetAllMsg> _client;

		public UserGetAllQueryHandler(IRequestClient<UserGetAllMsg> client) => _client = client;

		public async Task<IEnumerable<IdNameDTO>> Handle(UserGetAllQuery request, CancellationToken token) {
			var response = await _client.GetResponse<UserGetAllResponse>(request, token);
			return response.Message.Items;
		}

	}
}
