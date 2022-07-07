using System;
using System.Linq;

using Board.Infrastructure.Repository.Implementation;

namespace Boards.Boards.Infrastructure.Repos {
	internal class UnitOfWork : AbstractUnitOfWork {

		public UnitOfWork(BoardsContext context) : base(context) { }

	}
}
