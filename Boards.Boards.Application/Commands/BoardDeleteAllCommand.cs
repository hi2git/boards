using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Board.Domain.Repos;

using Boards.Boards.Domain.Repos;
using Boards.Commons.Application.Utils;
using Boards.Domain.Contracts.Boards;
using Boards.Domain.Contracts.Users;

using FluentValidation;

using MassTransit;

using MediatR;

namespace Boards.Boards.Application.Commands {
	public record BoardDeleteAllCommand : UserDeletedEvent, IRequest {
		public BoardDeleteAllCommand(Guid id) : base(id) {}
	}

	public class BoardDeleteAllCommandValidator : AbstractValidator<BoardDeleteAllCommand> {

		public BoardDeleteAllCommandValidator() {
			RuleFor(n => n.Id).NotEmpty();
		}

	}

	internal class BoardDeleteAllCommandHandler : IRequestHandler<BoardDeleteAllCommand> {
		private readonly IBoardRepo _repo;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IPublishEndpoint _publish;

		public BoardDeleteAllCommandHandler(IBoardRepo repo, IUnitOfWork unitOfWork, IPublishEndpoint publish) {
			_repo = repo;
			_unitOfWork = unitOfWork;
			_publish = publish;
		}

		public async Task<Unit> Handle(BoardDeleteAllCommand request, CancellationToken token) {
			var items = await _repo.GetAll(request.Id, token);
			await items.ForEachAsync(_repo.Delete);
			await _unitOfWork.Commit(() => items.WhenAll(n => _publish.Publish<BoardDeletedEvent>(new(n.Id))));
			return Unit.Value;
		}
	}
}
