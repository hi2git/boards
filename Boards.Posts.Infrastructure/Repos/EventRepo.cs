using System;
using System.Linq;

using Board.Domain.Models;
using Board.Domain.Repos;
using Board.Infrastructure.Repository.Implementation;

namespace Boards.Posts.Infrastructure.Repos {
	internal class EventRepo : AbstractRepo<IntegrationEvent>, IEventRepo {
		public EventRepo(PostsContext context) : base(context) { }
	}
}
