using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Board.Domain.DTO.BoardItems;
using Board.Domain.Models;
using Board.Domain.Repos;

using FluentValidation;

using MediatR;

namespace Boards.Application.Commands.BoardItems {
	public class BoardSortAllCommand : IRequest {

		public BoardSortAllCommand(Guid id, IEnumerable<BoardItemDTO> items) {
			this.Id = id;
			this.Items = items;
		}

		public Guid Id { get; }

		public IEnumerable<BoardItemDTO> Items { get; }

	}

	public class BoardSortAllCommandValidator : AbstractValidator<BoardSortAllCommand> {

		public BoardSortAllCommandValidator() {
			RuleFor(n => n.Id).NotEmpty();
			RuleFor(n => n.Items).NotEmpty();
		}

	}

	internal class BoardSortAllCommandHandler : IRequestHandler<BoardSortAllCommand> {
		private readonly IUnitOfWork _unitOfWork;
		private readonly IBoardItemRepo _repo;

		public BoardSortAllCommandHandler(IUnitOfWork unitOfWork, IBoardItemRepo repo) {
			_unitOfWork = unitOfWork;
			_repo = repo;
		}

		public async Task<Unit> Handle(BoardSortAllCommand request, CancellationToken cancellationToken) {// TODO add user check
			var origins = await _repo.GetAll(request.Id);//_userMgr.CurrentUserId);
			var items = request.Items.OrderBy(n => n.OrderNumber).Select((n, i) => this.Map(n.Id.Value, i, origins));

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
