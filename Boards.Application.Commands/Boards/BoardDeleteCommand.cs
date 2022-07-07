using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Board.Domain.Repos;
using Board.Domain.Services;

using Boards.Commons.Application;
using Boards.Domain.Contracts.Boards;
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
		//private readonly IPostRepo _itemRepo;
		//private readonly IClient<ImageDeleteMsg, ImageDeleteResponse> _imageDelClient;  // TODO use specific client
		private readonly IPublishEndpoint _publish;

		public BoardDeleteCommandHandler(IUnitOfWork unitOfWork, IBoardRepo boardRepo,  IPublishEndpoint publish) {
			_unitOfWork = unitOfWork;
			_boardRepo = boardRepo;
			//_itemRepo = itemRepo;
			//_imageDelClient = imageDelClient;
			_publish = publish;
		}

		public async Task<Unit> Handle(BoardDeleteCommand request, CancellationToken token) {
			var id = request?.Id ?? throw new ArgumentNullException(nameof(request));
			var board = await _boardRepo.Get(id, token) ?? throw new ArgumentException($"Отсутствует доска {id}");

			//foreach (var item in board.BoardItems) {
			//	await _imageDelClient.Send( new(item.Id), token);
			//	await _itemRepo.Delete(item);
			//}

			//throw new NotImplementedException($"Couldn't delete board {request.Id}. Method is not implemented");

			await _boardRepo.Delete(board);
			await _unitOfWork.Commit();

			await _publish.Publish(new BoardDeletedEvent(id)); // TODO: place inside transaction

			return Unit.Value;
		}
	}
}
