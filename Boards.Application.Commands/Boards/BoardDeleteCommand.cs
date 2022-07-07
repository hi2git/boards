using System;
using System.Linq;


using Boards.Commons.Application;
using Boards.Domain.Contracts.Boards;

using FluentValidation;


using MediatR;

namespace Boards.Application.Commands.Boards {
	
	public record BoardDeleteCommand : BoardDeleteMsg, IRequest {

		public BoardDeleteCommand(Guid id) : base(id) { }

	}

	public class BoardDeleteCommandValidator : AbstractValidator<BoardDeleteCommand> {

		public BoardDeleteCommandValidator() {
			RuleFor(n => n.Id).NotEmpty();
		}

	}

	internal class BoardDeleteCommandHandler : AbstractHandler<BoardDeleteCommand, BoardDeleteMsg, BoardDeleteResponse> {  //IRequestHandler<BoardDeleteCommand> {
																														   //private readonly IUnitOfWork _unitOfWork;
																														   //private readonly IBoardRepo _repo;
																														   //private readonly IPublishEndpoint _publish;

		public BoardDeleteCommandHandler(IClient<BoardDeleteMsg, BoardDeleteResponse> client) : base(client) { }

		//public BoardDeleteCommandHandler(IUnitOfWork unitOfWork, IBoardRepo repo,  IPublishEndpoint publish) {
		//	_unitOfWork = unitOfWork;
		//	_repo = repo;
		//	_publish = publish;
		//}

		//public async Task<Unit> Handle(BoardDeleteCommand request, CancellationToken token) {
		//	var id = request?.Id ?? throw new ArgumentNullException(nameof(request));
		//	var board = await _repo.Get(id, token) ?? throw new ArgumentException($"Отсутствует доска {id}");

		//	await _repo.Delete(board);
		//	await _unitOfWork.Commit(() => _publish.Publish<BoardDeletedEvent>(new (id)));

		//	return Unit.Value;
		//}

	}
}
