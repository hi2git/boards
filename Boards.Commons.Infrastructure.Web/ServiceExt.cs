using System;
using System.Linq;

using Boards.Commons.Application;
using Boards.Commons.Infrastructure.Web.Filters;
using Boards.Infrastructure.Web.Clients;
using Boards.Infrastructure.Web.Middlewares;

using FluentValidation;

using MassTransit;

using MediatR;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

using Serilog;
using Serilog.Exceptions;

namespace Boards.Infrastructure.Web {
	public static  class ServiceExt {

		public static WebApplicationBuilder Configure(
			this WebApplicationBuilder builder, 
			string name, 
			Type[] handlers,
			Type[]? filters = null,
			params Type[] consumers
		) {
			var services = builder.Services;
			var logging = builder.Logging;
			
			var log = new LoggerConfiguration()
				.ReadFrom.Configuration(builder.Configuration)
				.WriteTo.Http("http://logstash:28080", null)   // TODO: configure logger
				.Enrich.FromLogContext()
				.Enrich.WithExceptionDetails()
				.Enrich.WithProperty("Service", name)
				.CreateLogger();

			logging.AddSerilog(log);

			var assemblies = handlers.Select(n => n.Assembly).ToArray();
			services.AddMediatR(assemblies);
			services.AddValidatorsFromAssemblies(assemblies);
			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

			services.AddControllers();

			services.AddEndpointsApiExplorer().AddSwaggerGen();
			services.AddHealthChecks();

			services.AddMassTransit(n => {
				if (consumers?.Any() == true) {
					n.AddConsumers(consumers.Select(n => n.Assembly).ToArray());
				}

				n.UsingRabbitMq((context, cfg) => {
					cfg.Host("rabbitmq", "/", x => { x.Username("rabbitmq"); x.Password("rabbitmq"); });
					//cfg.UseMessageRetry(x => x.None());

					cfg.UseConsumeFilter(typeof(AddCorrelationConsumeFilter<>), context);
					//filters?.ToList().ForEach(n => cfg.UseSendFilter(n, context));
					foreach (var filter in filters ?? Enumerable.Empty<Type>()) {
						cfg.UsePublishFilter(filter, context);
					}

					//cfg.ConfigureSend(x => x.UseSendExecute(c => c.Headers.Set("CorrId", "123")));

					cfg.ConfigureEndpoints(context);
				});
			});

			services.AddOptions<MassTransitHostOptions>()
				.Configure(options => options.WaitUntilStarted = true);

			services.AddTransient(typeof(IClient<,>), typeof(RequestClient<,>));

			return builder;
		}

		public static WebApplication UseInfrastructureWeb(this WebApplication app) {
			app.UseMiddleware<CorellationMiddleware>();
			app.UseMiddleware<ExceptionMiddleware>();

			app.UseSwagger();
			app.UseSwaggerUI();

			app.MapControllers();

			app.MapHealthChecks("/hc");

			return app;
		}

	}
}
