using System;
using System.Threading.Tasks;

using MassTransit;

using Microsoft.AspNetCore.Http;

namespace Boards.Front.API.Filters {
	internal class CorrelationPublishFilter<T> : IFilter<PublishContext<T>> where T: class {
		private readonly IHttpContextAccessor _http;

		public CorrelationPublishFilter(IHttpContextAccessor http) {
			_http = http;
		}

		public void Probe(ProbeContext context) {}

		public async Task Send(PublishContext<T> context, IPipe<PublishContext<T>> next) {
			var id = _http.HttpContext?.TraceIdentifier ?? throw new InvalidOperationException($"Couldn't get HttpContext");    // TODO: move to userMgr
			context.Headers.Set("CorrId", id);

				await next.Send(context);
		}
	}
}
