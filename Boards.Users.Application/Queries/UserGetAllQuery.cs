using System;
using System.Linq;

using Board.Domain.DTO;

using Boards.Domain.Contracts.Users;
using Boards.Users.Domain.Models;
using Boards.Users.Domain.Repos;

using FluentValidation;

using MediatR;

namespace Boards.Users.Application.Queries {
	public record UserGetAllQuery : UserGetAllMsg, IRequest<IEnumerable<IdNameDTO>> { }

	public class UserGetAllQueryValidator : AbstractValidator<UserGetAllQuery> { }

	internal class UserGetAllQueryHandler : IRequestHandler<UserGetAllQuery, IEnumerable<IdNameDTO>> {
		private readonly IUserRepo _repo;

		public UserGetAllQueryHandler(IUserRepo repo) => _repo = repo;

		public async Task<IEnumerable<IdNameDTO>> Handle(UserGetAllQuery request, CancellationToken token) =>
			(await _repo.GetAll(token)).Select(this.Map);

		private IdNameDTO Map(User user) => new() { Id = user.Id, Name = user.Name };
	}
}
