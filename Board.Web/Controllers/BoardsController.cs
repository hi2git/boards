using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Board.Domain.DTO;
using Board.Domain.Models;
using Board.Domain.Repos;
using Board.Domain.Services;

using Microsoft.AspNetCore.Mvc;

namespace Board.Web.Controllers {
	public class BoardsController : AbstractApiController {
		private readonly IUserManager _userMgr;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IBoardItemRepo _repo;

		public BoardsController(IUserManager userMgr, IUnitOfWork unitOfWork, IBoardItemRepo repo) {
			_userMgr = userMgr;
			_unitOfWork = unitOfWork;
			_repo = repo;
		}

		[HttpGet]
		public async Task<IEnumerable<BoardItemDTO>> GetAll() {
			var items = await _repo.GetAll(_userMgr.CurrentUserId);

			var dtos = new List<BoardItemDTO>();
			foreach (var n in items) {
				dtos.Add(await this.Map(n));
			}
			return dtos;
		}

		[HttpPut]
		public async Task Sort([FromBody] IEnumerable<BoardItemDTO> dtos) {
			var origins = await _repo.GetAll(_userMgr.CurrentUserId);
			var items = dtos.OrderBy(n => n.OrderNumber).Select((n, i) => this.Map(n.Id.Value, i, origins));

			foreach (var itm in items) {
				await _repo.Update(itm);
			}
			await _unitOfWork.Commit();
		}

		private BoardItem Map(Guid id, int orderNumber, IEnumerable<BoardItem> origins) {
			var origin = origins.FirstOrDefault(n => n.Id == id);
			origin.OrderNumber = orderNumber;
			return origin;
		}

		private async Task<BoardItemDTO> Map(BoardItem n) => new BoardItemDTO {
			Id = n.Id,
			OrderNumber = n.OrderNumber,
			Description = n.Description,
			IsDone = n.IsDone
		};

	}


}
