using System;
using System.Linq;

using Board.Domain.Repos;

using Boards.Commons.Application.Services;
using Boards.Commons.Domain.DTOs.Posts;
using Boards.Domain.Contracts.Posts;
using Boards.Posts.Domain.Models;
using Boards.Posts.Domain.Repos;

using FluentValidation;

using MediatR;

namespace Boards.Posts.Application.Commands {
	public record PostSortAllCommand : PostSortAllMsg, IRequest {

		public PostSortAllCommand(Guid boardId) : base(boardId) { }

		public PostSortAllCommand(PostSortAllMsg msg) : this(msg.Id) => this.Items = msg.Items;


	}

	public class PostSortAllCommandValidator : AbstractValidator<PostSortAllCommand> {

		public PostSortAllCommandValidator() {
			RuleFor(n => n.Id).NotEmpty();
			//RuleFor(n => n.Items).NotEmpty();
		}

	}

	internal class PostSortAllCommandHandler : IRequestHandler<PostSortAllCommand> {
		private readonly IUnitOfWork _unitOfWork;
		private readonly IPostRepo _repo;
		private readonly ICacheService _cache;

		public PostSortAllCommandHandler(IUnitOfWork unitOfWork, IPostRepo repo, ICacheService cache) {
			_unitOfWork = unitOfWork;
			_repo = repo;
			_cache = cache;
		}

		public async Task<Unit> Handle(PostSortAllCommand request, CancellationToken token) {// TODO add user check
			var origins = await _repo.GetAll(request.Id, token);//_userMgr.CurrentUserId);
			var dtos = request.Items?.Cast<IdOrderableDTO>() ?? origins.Select(n => new IdOrderableDTO { Id = n.Id, OrderNumber = n.OrderNumber });
			var items = dtos.OrderBy(n => n.OrderNumber).Select((n, i) => this.Map(n.Id.Value, i, origins));

			foreach (var item in items) {
				await _repo.Update(item);
			}
			await _unitOfWork.Commit();

			await _cache.RemoveBoard(request.Id);

			return Unit.Value;
		}

		private Post Map(Guid id, int orderNumber, IEnumerable<Post> origins) {
			var origin = origins.FirstOrDefault(n => n.Id == id) ?? throw new Exception($"Отсутствует пост {id}");
			origin.OrderNumber = orderNumber;
			return origin;
		}
	}
}
