using System;
using System.Linq;

using Board.Domain.Models;
using Board.Domain.Repos;

namespace Boards.Files.Infrastructure {
	internal class EventRepo : IEventRepo {	// TODO real implementation
		public Task Create(IntegrationEvent entity) => Task.CompletedTask;

		public Task Delete(IntegrationEvent entity) => throw new NotImplementedException();

		public Task<IntegrationEvent> Get(Guid id, CancellationToken token) => throw new NotImplementedException();

		public Task<List<IntegrationEvent>> GetAll(CancellationToken token) => throw new NotImplementedException();

		public Task Update(IntegrationEvent entity) => throw new NotImplementedException();
	}
}
