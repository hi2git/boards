using System;
using System.Linq;

using Board.Domain.Models;
using Board.Domain.Repos;
using Board.Infrastructure.Repository;

using Boards.Posts.Infrastructure;

namespace Boards.Users.Infrastructure.Repos {
	internal class EventRepo : AbstractRepo<IntegrationEvent>, IEventRepo {
		public EventRepo(UsersContext context) : base(context) { }
	}
}
