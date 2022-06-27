using System;
using System.Threading;
using System.Threading.Tasks;

using Board.Domain.DTO.BoardItems;
using Board.Domain.Models;
using Board.Domain.Repos;
using Board.Domain.Services;

using FluentValidation;

using MediatR;

namespace Boards.Application.Commands.BoardItems {
	public class BoardItemCreateCommand : IRequest {

		public BoardItemCreateCommand(Guid id, BoardItemDTO item) {
			this.Id = id;
			this.Item = item;
		}

		public Guid Id { get; }
		public BoardItemDTO Item { get; }
	}

	public class BoardItemCreateCommandValidator : AbstractValidator<BoardItemCreateCommand> {

		public BoardItemCreateCommandValidator() {
			RuleFor(n => n.Id).NotEmpty();
			RuleFor(n => n.Item).NotEmpty();
			RuleFor(n => n.Item.Id).Empty();
			//RuleFor(n => n.Item.OrderNumber).GreaterThan(0);
			//RuleFor(n => n.Item.Description).NotEmpty();
			//RuleFor(n => n.Item.IsDone).NotEmpty();
			RuleFor(n => n.Item.Content).NotEmpty();
		}

	}

	internal class BoardItemCreateCommandHandler : IRequestHandler<BoardItemCreateCommand> {
		private readonly IUnitOfWork _unitOfWork;
		private readonly IBoardRepo _boardRepo;
		private readonly IBoardItemRepo _repo;
		private readonly IFileStorage _fileStorage;

		public BoardItemCreateCommandHandler(IUnitOfWork unitOfWork, IBoardRepo boardRepo, IBoardItemRepo repo, IFileStorage fileStorage) {
			_unitOfWork = unitOfWork;
			_boardRepo = boardRepo;
			_repo = repo;
			_fileStorage = fileStorage;
		}

		public async Task<Unit> Handle(BoardItemCreateCommand request, CancellationToken token) {
			var dto = request?.Item ?? throw new ArgumentNullException(nameof(request));
			var board = await _boardRepo.Get(request.Id, token) ?? throw new ArgumentException($"Отсутствует доска {request.Id}");
			var item = new BoardItem(Guid.NewGuid(), board, dto.OrderNumber, dto.Description); // TODO: check user // _userMgr.CurrentUserId

			await _repo.Create(item);
			await _unitOfWork.Commit();

			await _fileStorage.Write(item.Id, dto.Content);
			return Unit.Value;
		}
	}
}
