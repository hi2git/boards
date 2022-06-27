using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Board.Domain.DTO;
using Board.Domain.Models;
using Board.Domain.Repos;

using FluentValidation;

using MediatR;

namespace Boards.Application.Queries.Users {
	public class UserGetAllQuery : IRequest<IEnumerable<IdNameDTO>> { }

	public class UserGetAllQueryValidator : AbstractValidator<UserGetAllQuery> { }

	internal class UserGetAllQueryHandler : IRequestHandler<UserGetAllQuery, IEnumerable<IdNameDTO>> {
		private readonly IUserRepo _repo;

		public UserGetAllQueryHandler(IUserRepo repo) => _repo = repo;

		public async Task<IEnumerable<IdNameDTO>> Handle(UserGetAllQuery request, CancellationToken token) =>
			(await _repo.GetAll(token)).Select(this.Map);

		private IdNameDTO Map(User user) => new IdNameDTO { Id = user.Id, Name = user.Name };
	}
}
