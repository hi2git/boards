using System;
using System.Linq;

using MassTransit;

namespace Boards.Commons.Infrastructure.Web.Filters {
	internal class AddCorrelationConsumeFilter<T> : IFilter<ConsumeContext<T>> where T : class {
		public void Probe(ProbeContext context) { }

		public async Task Send(ConsumeContext<T> context, IPipe<ConsumeContext<T>> next) {
			var id = context.Headers.Get<string>("CorrId");
			using (Serilog.Context.LogContext.PushProperty("CorrelationId", id)) {
				await next.Send(context);
			}
		}
	}
}
