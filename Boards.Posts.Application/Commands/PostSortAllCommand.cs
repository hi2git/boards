﻿using System;
using System.Linq;

using Board.Domain.DTO.Posts;
using Board.Domain.Models;
using Board.Domain.Repos;

using Boards.Domain.Contracts.Posts;

using FluentValidation;

using MediatR;

namespace Boards.Posts.Application.Commands {
	public record PostSortAllCommand : IRequest {

		public PostSortAllCommand(Guid id) => this.Id = id;

		public PostSortAllCommand(PostSortAllMsg msg) : this(msg.Id) => this.Items = msg.Items;

		public Guid Id { get; }

		public IEnumerable<PostDTO> Items { get; } = Enumerable.Empty<PostDTO>();

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
			var origin = origins.FirstOrDefault(n => n.Id == id) ?? throw new Exception($"Отсутствует пост {id}");
			origin.OrderNumber = orderNumber;
			return origin;
		}
	}
}