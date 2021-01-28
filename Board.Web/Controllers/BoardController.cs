using System;
using System.Threading.Tasks;

using Board.Domain.DTO;
using Board.Domain.Models;
using Board.Domain.Repos;
using Board.Domain.Services;

using Microsoft.AspNetCore.Mvc;

namespace Board.Web.Controllers {
	public class BoardController : AbstractApiController {
		private readonly IUserManager _userMgr;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IBoardItemRepo _repo;
		private readonly IFileStorage _fileStorage;

		public BoardController(IUserManager userMgr, IUnitOfWork unitOfWork, IBoardItemRepo repo, IFileStorage fileStorage) {
			_userMgr = userMgr;
			_unitOfWork = unitOfWork;
			_repo = repo;
			_fileStorage = fileStorage;
		}

		[HttpPost]
		public async Task Create([FromBody] BoardItemDTO item) {
			var id = Guid.NewGuid();

			var newItem = new BoardItem(id, _userMgr.CurrentUserId, item.OrderNumber, item.Description);
			await _repo.Create(newItem);
			await _unitOfWork.Commit();

			await _fileStorage.Write(id, item.Content);
		}

		[HttpPut("content")]
		public Task UpdateContent([FromBody] BoardItemDTO item) {
			// TODO: check user before modify

			return _fileStorage.Write(item.Id.Value, item.Content);
		}

		[HttpPut]
		public async Task Update([FromBody] BoardItemDTO item) {
			// TODO: check user before modify
			var entity = await _repo.Get(item.Id.Value);
			entity = this.Map(item, entity);
			await _repo.Update(entity);
			await _unitOfWork.Commit();
		}

		[HttpDelete]
		public async Task Delete([FromQuery] Guid id) {
			// TODO: check user before modify
			var entity = await _repo.Get(id);
			await _repo.Delete(entity);
			await _unitOfWork.Commit();

			await _fileStorage.Delete(id);
		}

		private BoardItem Map(BoardItemDTO dto, BoardItem entity) {
			entity.IsDone = dto.IsDone;
			entity.Description = dto.Description;
			return entity;
		}

		//public class BoardItemFullDTO {
		//	public Guid UserId { get; set; }

		//	public BoardItemDTO Item { get; set; }
		//}

		//public class BoardItemDeleteDTO {
		//	public Guid UserId { get; set; }

		//	public Guid Id { get; set; }
		//}

	}
}
