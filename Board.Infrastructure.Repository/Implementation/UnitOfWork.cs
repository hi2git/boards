using System;



namespace Board.Infrastructure.Repository.Implementation {
	internal class UnitOfWork : AbstractUnitOfWork {
		public UnitOfWork(BoardContext context) : base(context) { }
	}
}
