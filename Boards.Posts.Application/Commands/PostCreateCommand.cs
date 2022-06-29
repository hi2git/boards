using System;

using Board.Domain.DTO.Posts;
using Board.Domain.Models;
using Board.Domain.Repos;
using Board.Domain.Services;

using Boards.Commons.Application;
using Boards.Domain.Contracts.Images;
using Boards.Domain.Contracts.Posts;

using FluentValidation;

using MediatR;

namespace Boards.Posts.Application.Commands {
	public record PostCreateCommand : PostCreateMsg, IRequest {

		public PostCreateCommand(PostCreateMsg msg) {
			this.Id = msg.Id;
			this.Item = msg.Item;
		}
	}

	public class PostCreateCommandValidator : AbstractValidator<PostCreateCommand> {

		public PostCreateCommandValidator() {
			RuleFor(n => n.Id).NotEmpty();
			RuleFor(n => n.Item).NotEmpty();
			RuleFor(n => n.Item.Id).Empty();
			//RuleFor(n => n.Item.OrderNumber).GreaterThan(0);
			//RuleFor(n => n.Item.Description).NotEmpty();
			//RuleFor(n => n.Item.IsDone).NotEmpty();
			RuleFor(n => n.Item.Content).NotEmpty();
		}

	}

	internal class PostCreateCommandHandler : IRequestHandler<PostCreateCommand> {
		private readonly IUnitOfWork _unitOfWork;
		private readonly IBoardRepo _boardRepo;
		private readonly IBoardItemRepo _repo;
		private readonly IClient<ImageUpdateMsg, ImageUpdateResponse> _client;

		public PostCreateCommandHandler(IUnitOfWork unitOfWork, IBoardRepo boardRepo, IBoardItemRepo repo, IClient<ImageUpdateMsg, ImageUpdateResponse> client) {
			_unitOfWork = unitOfWork;
			_boardRepo = boardRepo;
			_repo = repo;
			_client = client;
		}

		public async Task<Unit> Handle(PostCreateCommand request, CancellationToken token) {
			var dto = request?.Item ?? throw new ArgumentNullException(nameof(request));
			var board = await _boardRepo.Get(request.Id, token) ?? throw new ArgumentException($"Отсутствует доска {request.Id}");	// TODO use EntityNotFoundException
			var item = new BoardItem(Guid.NewGuid(), board, dto.OrderNumber, dto.Description); // TODO: check user // _userMgr.CurrentUserId

			await _repo.Create(item);
			await _unitOfWork.Commit();

			await _client.Send(new (item.Id, dto.Content), token);	// TODO: use PostCreatedEvent
			return Unit.Value;
		}
	}
}
