using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Board.Domain.DTO.Posts;
using Board.Domain.Models;
using Board.Domain.Repos;

using FluentValidation;

using MediatR;

namespace Boards.Application.Commands.Posts {
	public class PostSortAllCommand : IRequest {

		public PostSortAllCommand(Guid id, IEnumerable<PostDTO> items = null) {
			this.Id = id;
			this.Items = items;
		}

		public Guid Id { get; }

		public IEnumerable<PostDTO> Items { get; }

	}

	public class PostSortAllCommandValidator : AbstractValidator<PostSortAllCommand> {

		public PostSortAllCommandValidator() {
			RuleFor(n => n.Id).NotEmpty();
			//RuleFor(n => n.Items).NotEmpty();
		}

	}

	internal class PostSortAllCommandHandler : IRequestHandler<PostSortAllCommand> {
		private readonly IUnitOfWork _unitOfWork;
		private readonly IBoardItemRepo _repo;

		public PostSortAllCommandHandler(IUnitOfWork unitOfWork, IBoardItemRepo repo) {
			_unitOfWork = unitOfWork;
			_repo = repo;
		}

		public async Task<Unit> Handle(PostSortAllCommand request, CancellationToken token) {// TODO add user check
			var origins = await _repo.GetAll(request.Id, token);//_userMgr.CurrentUserId);
			var dtos = request.Items?.Cast<IdOrderableDTO>() ?? origins.Select(n => new IdOrderableDTO { Id = n.Id, OrderNumber = n.OrderNumber });
			var items = dtos.OrderBy(n => n.OrderNumber).Select((n, i) => this.Map(n.Id.Value, i, origins));

			foreach (var item in items) {
				await _repo.Update(item);
			}
			await _unitOfWork.Commit();
			return Unit.Value;
		}

		private BoardItem Map(Guid id, int orderNumber, IEnumerable<BoardItem> origins) {
			var origin = origins.FirstOrDefault(n => n.Id == id);
			origin.OrderNumber = orderNumber;
			return origin;
		}
	}
}
