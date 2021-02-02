using System;
using System.Threading.Tasks;

using Board.Domain.DTO;
using Board.Domain.Models;
using Board.Domain.Repos;
using Board.Domain.Services;

using Microsoft.AspNetCore.Mvc;

namespace Board.Web.Controllers {
	public class BoardItemController : AbstractApiController {
		private readonly IUserManager _userMgr;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IBoardRepo _boardRepo;
		private readonly IBoardItemRepo _repo;
		private readonly IFileStorage _fileStorage;

		public BoardItemController(IUserManager userMgr, IUnitOfWork unitOfWork, IBoardRepo boardRepo, IBoardItemRepo repo, IFileStorage fileStorage) {
			_userMgr = userMgr;
			_unitOfWork = unitOfWork;
			_boardRepo = boardRepo;
			_repo = repo;
			_fileStorage = fileStorage;
		}

		[HttpPost]
		public async Task Create([FromBody] IdItemDTO dto) {
			var id = Guid.NewGuid();

			var board = await _boardRepo.Get(dto.Id) ?? throw new ArgumentException($"Отсутствует доска {dto.Id}");
			var item = new BoardItem(id, board, dto.Item.OrderNumber, dto.Item.Description); // TODO: check user // _userMgr.CurrentUserId

			await _repo.Create(item);
			await _unitOfWork.Commit();

			await _fileStorage.Write(id, dto.Item.Content);
		}

		[HttpPut("content")]
		public Task UpdateContent([FromBody] BoardItemDTO item) => _fileStorage.Write(item.Id.Value, item.Content); // TODO: check user before modify

		[HttpPut]
		public async Task Update([FromBody] BoardItemDTO item) { // TODO: check user before modify
			var entity = await _repo.Get(item.Id.Value);
			entity = this.Map(item, entity);
			await _repo.Update(entity);
			await _unitOfWork.Commit();
		}

		[HttpDelete]
		public async Task Delete([FromQuery] Guid id) { // TODO: check user before modify
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

		public class IdItemDTO {
			public Guid Id { get; set; }

			public BoardItemDTO Item { get; set; }
		}

	}
}
