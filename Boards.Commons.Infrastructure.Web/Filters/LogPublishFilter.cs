using System;
using System.Linq;

using Boards.Domain.Contracts;

using MassTransit;

using Microsoft.Extensions.Logging;

namespace Boards.Commons.Infrastructure.Web.Filters {
	internal class LogPublishFilter<T> : IFilter<PublishContext<T>> where T: class {
		private readonly ILogger<LogPublishFilter<T>> _log;

		public LogPublishFilter(ILogger<LogPublishFilter<T>> log) => _log = log;

		public void Probe(ProbeContext context) { }

		public async Task Send(PublishContext<T> context, IPipe<PublishContext<T>> next) {
			var type = typeof(T).Name;
			var shouldLog = type != typeof(AbstractMsg).Name;
			if (shouldLog) _log.LogDebug("{Action:l} {Type:l} ...", "Publishing", type);
			
			await next.Send(context);
		}
	}
}
