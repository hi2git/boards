using System;
using System.Linq;

using MassTransit;

using Microsoft.Extensions.Logging;

namespace Boards.Commons.Infrastructure.Web.Filters {
	internal class CorrelationConsumeFilter<T> : IFilter<ConsumeContext<T>> where T : class {
		private readonly ILogger<CorrelationConsumeFilter<T>> _log;

		public CorrelationConsumeFilter(ILogger<CorrelationConsumeFilter<T>> log) => _log = log;

		public void Probe(ProbeContext context) { }

		public async Task Send(ConsumeContext<T> context, IPipe<ConsumeContext<T>> next) {
			var type = typeof(T).Name;
			var action = "Consuming";

			var id = context.Headers.Get<string>("CorrId");
			using (Serilog.Context.LogContext.PushProperty("CorrelationId", id)) 
				using (Serilog.Context.LogContext.PushProperty("Action", "Consuming")) {
					_log.LogDebug("{Action:l} {Type:l} ...", action, type);
					await next.Send(context);
					_log.LogDebug("{Action:l} {Type:l} ... {Result:l}", action, type, "OK");
			}
		}
	}
}
