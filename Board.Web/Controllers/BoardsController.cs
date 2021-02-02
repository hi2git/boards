using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Board.Domain.DTO;
using Board.Domain.Repos;
using Board.Domain.Services;

using Microsoft.AspNetCore.Mvc;

namespace Board.Web.Controllers {
	public class BoardsController : AbstractApiController {
		private readonly IUserManager _userMgr;
		private readonly IBoardRepo _repo;

		public BoardsController(IUserManager userMgr, IBoardRepo repo) {
			_userMgr = userMgr;
			_repo = repo;
		}

		[HttpGet]
		public async Task<IEnumerable<IdNameDTO>> GetAll() {
			var boards = await _repo.GetAll(_userMgr.CurrentUserId);
			return boards.Select(this.Map);
		}

		private IdNameDTO Map(Domain.Models.Board entity) => new IdNameDTO { Id = entity.Id, Name = entity.Name };

	}
}
