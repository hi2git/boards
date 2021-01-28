using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Board.Domain.DTO;
using Board.Domain.Models;
using Board.Domain.Repos;

using Microsoft.AspNetCore.Mvc;

namespace Board.Web.Controllers {
	public class UsersController : AbstractApiController {

		private readonly IRepo<User> _repo;

		public UsersController(IUserRepo repo) => _repo = repo;

		[HttpGet]
		public async Task<IEnumerable<IdNameDTO>> GetAll() => (await _repo.GetAll()).Select(n => new IdNameDTO { Id = n.Id, Name = n.Name });

	}
}
