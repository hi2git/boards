using System;
using System.Threading.Tasks;

using Board.Domain.Enums;
using Board.Domain.Models;
using Board.Domain.Repos;

using Microsoft.EntityFrameworkCore;

namespace Board.Infrastructure.Repository.Implementation {
	internal class RoleRepo : AbstractRepo<Role>, IRoleRepo {
		public RoleRepo(BoardContext context) : base(context) { }

		public Task<Role> Get(RoleEnum id) => this.Query
			.FirstOrDefaultAsync(n => n.Id == id);
	}
}
