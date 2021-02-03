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
	public class BoardItemsController : AbstractApiController {
		private readonly IUserManager _userMgr;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IBoardItemRepo _repo;
		private readonly IFileStorage _fileStorage;

		public BoardItemsController(IUserManager userMgr, IUnitOfWork unitOfWork, IBoardItemRepo repo, IFileStorage fileStorage) {
			_userMgr = userMgr;
			_unitOfWork = unitOfWork;
			_repo = repo;
			_fileStorage = fileStorage;
		}

		[HttpGet]
		public async Task<IEnumerable<BoardItemDTO>> GetAll([FromQuery] Guid id) { // TODO add user check
			var items = await _repo.GetAll(id);//_userMgr.CurrentUserId);

			var dtos = new List<BoardItemDTO>();
			foreach (var n in items) {
				dtos.Add(await this.Map(n));
			}
			return dtos;
		}

		[HttpPut]
		public async Task Sort([FromBody] SortDTO dto) { // TODO add user check
			var origins = await _repo.GetAll(dto.Id);//_userMgr.CurrentUserId);
			var items = dto.Items.OrderBy(n => n.OrderNumber).Select((n, i) => this.Map(n.Id.Value, i, origins));

			foreach (var item in items) {
				await _repo.Update(item);
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

		public class SortDTO {
			public Guid Id { get; set; }

			public IEnumerable<BoardItemDTO> Items { get; set; }
		}

	}


}
