using System;
using System.Linq;

using Board.Domain.Models;

namespace Board.Domain.Repos {
	public interface IEventRepo : IRepo<IntegrationEvent> { }
}
