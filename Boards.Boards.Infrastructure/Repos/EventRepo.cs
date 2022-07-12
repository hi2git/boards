using System;
using System.Linq;

using Board.Domain.Models;
using Board.Domain.Repos;
using Board.Infrastructure.Repository.Implementation;

namespace Boards.Boards.Infrastructure.Repos {
	internal class EventRepo : AbstractRepo<IntegrationEvent>, IEventRepo {
		public EventRepo(BoardsContext context) : base(context) { }
	}
}
