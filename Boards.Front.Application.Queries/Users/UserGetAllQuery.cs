﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Boards.Commons.Application;
//using Boards.Commons.Application.Services;
using Boards.Commons.Domain.DTOs;
using Boards.Domain.Contracts.Users;

using FluentValidation;

using MediatR;

namespace Boards.Front.Application.Queries.Users {
	public record UserGetAllQuery : UserGetAllMsg, IRequest<IEnumerable<IdNameDTO>> { }

	public class UserGetAllQueryValidator : AbstractValidator<UserGetAllQuery> { }

	internal class UserGetAllQueryHandler : IRequestHandler<UserGetAllQuery, IEnumerable<IdNameDTO>> {
		private readonly IClient<UserGetAllMsg, UserGetAllResponse> _client;
		//private readonly ICacheService _cache;

		public UserGetAllQueryHandler(IClient<UserGetAllMsg, UserGetAllResponse> client/*, ICacheService cache*/) {
			_client = client;
			//_cache = cache;
		}

		//public Task<IEnumerable<IdNameDTO>> Handle(UserGetAllQuery request, CancellationToken token) => _cache.GetOrRequest($"all_users", () => this.Request(request, token), token);

		//public async Task<IEnumerable<IdNameDTO>> Request(UserGetAllQuery request, CancellationToken token) {
		public async Task<IEnumerable<IdNameDTO>> Handle(UserGetAllQuery request, CancellationToken token) { 
			var response = await _client.Send(request, token);
			return response.Items;
		}

	}
}
