using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Board.Domain.Repos;
using Board.Domain.Services;

using Boards.Commons.Application;
using Boards.Domain.Contracts.Images;

using FluentValidation;

using MassTransit;

using MediatR;

namespace Boards.Application.Commands.Boards {
	public class BoardDeleteCommand : IRequest {

		public BoardDeleteCommand(Guid id) => this.Id = id;

		public Guid Id { get; }
	}

	public class BoardDeleteCommandValidator : AbstractValidator<BoardDeleteCommand> {

		public BoardDeleteCommandValidator() {
			RuleFor(n => n.Id).NotEmpty();
		}

	}

	internal class BoardDeleteCommandHandler : IRequestHandler<BoardDeleteCommand> {
		private readonly IUnitOfWork _unitOfWork;
		private readonly IBoardRepo _boardRepo;
		private readonly IBoardItemRepo _itemRepo;
		private readonly IClient<ImageDeleteMsg, ImageDeleteResponse> _imageDelClient;	// TODO use specific client

		public BoardDeleteCommandHandler(IUnitOfWork unitOfWork, IBoardRepo boardRepo, IBoardItemRepo itemRepo, IClient<ImageDeleteMsg, ImageDeleteResponse> imageDelClient) {
			_unitOfWork = unitOfWork;
			_boardRepo = boardRepo;
			_itemRepo = itemRepo;
			_imageDelClient = imageDelClient;
		}

		public async Task<Unit> Handle(BoardDeleteCommand request, CancellationToken token) {
			var id = request?.Id ?? throw new ArgumentNullException(nameof(request));
			var board = await _boardRepo.GetWithItems(id) ?? throw new ArgumentException($"Отсутствует доска {id}");

			foreach (var item in board.BoardItems) {	// TODO: publish BoardDeletedEvent
				await _imageDelClient.Send( new(item.Id), token);
				await _itemRepo.Delete(item);
			}
			await _boardRepo.Delete(board);
			await _unitOfWork.Commit();

			return Unit.Value;
		}
	}
}
