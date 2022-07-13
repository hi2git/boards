using System;
using System.Linq;

using Board.Infrastructure.Repository;

namespace Boards.Boards.Infrastructure.Repos {
	internal class UnitOfWork : AbstractUnitOfWork {

		public UnitOfWork(BoardsContext context) : base(context) { }

	}
}
