using System;
using System.Threading.Tasks;

using Board.Domain.DTO;
using Board.Domain.Repos;
using Board.Domain.Services;

using Boards.Application.Commands.Boards;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace Board.Web.Controllers {
	public class BoardController : AbstractApiController {
		private readonly IMediator _mediator;
		private readonly IUserManager _userMgr;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IUserRepo _userRepo;
		private readonly IBoardRepo _boardRepo;
		private readonly IBoardItemRepo _itemRepo;
		private readonly IFileStorage _fileStorage;

		public BoardController(IMediator mediator, IUserManager userMgr, IUnitOfWork unitOfWork, IUserRepo userRepo, IBoardRepo boardRepo, IBoardItemRepo itemRepo, IFileStorage fileStorage) {
			_mediator = mediator;
			_userMgr = userMgr;
			_unitOfWork = unitOfWork;
			_userRepo = userRepo;
			_boardRepo = boardRepo;
			_itemRepo = itemRepo;
			_fileStorage = fileStorage;
		}

		[HttpPost]
		public Task<Guid> Create([FromBody] Valueable<string> dto) => _mediator.Send(new BoardCreateCommand(dto.Value));
		//{
		//	var user = await _userRepo.Get(_userMgr.CurrentUserId);
		//	var board = new Domain.Models.Board(Guid.NewGuid(), dto.Value, user);

		//	await _boardRepo.Create(board);
		//	await _unitOfWork.Commit();

		//	return board.Id;
		//}

		[HttpPut]
		public async Task Update([FromBody] IdNameDTO dto) {
			var board = await _boardRepo.Get(dto.Id) ?? throw new ArgumentException($"Отсутствует доска {dto.Id}");
			board.Name = dto.Name;
			await _boardRepo.Update(board);
			await _unitOfWork.Commit();
		}

		[HttpDelete]
		public async Task Delete([FromQuery] Guid id) {
			var board = await _boardRepo.GetWithItems(id) ?? throw new ArgumentException($"Отсутствует доска {id}");

			foreach (var item in board.BoardItems) {
				await _fileStorage.Delete(item.Id);
				await _itemRepo.Delete(item);
			}

			await _boardRepo.Delete(board);
			await _unitOfWork.Commit();
		}

		public class Valueable<T> {
			public T Value { get; set; }
		}

	}
}
