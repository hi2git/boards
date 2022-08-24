using System;
using System.Linq;

using Board.Domain.Models;
using Board.Domain.Repos;

using Boards.Commons.Infrastructure.Repos;

namespace Boards.Posts.Infrastructure.Repos {
	internal class EventRepo : AbstractRepo<IntegrationEvent>, IEventRepo {
		public EventRepo(PostsContext context) : base(context) { }
	}
}
