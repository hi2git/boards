using System;
using System.Linq;

using Boards.Commons.Application;
using Boards.Commons.Application.Services;
using Boards.Commons.Infrastructure.Web.Filters;
using Boards.Commons.Infrastructure.Web.Services;
using Boards.Infrastructure.Web.Clients;
using Boards.Infrastructure.Web.Middlewares;

using FluentValidation;

using MassTransit;

using MediatR;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

using Serilog;
using Serilog.Exceptions;

using StackExchange.Redis;

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
			var config = builder.Configuration;

			var log = new LoggerConfiguration()
				.ReadFrom.Configuration(config)
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

			services.AddStackExchangeRedisCache(n => { 
				n.ConfigurationOptions = new ConfigurationOptions(); 
				n.ConfigurationOptions.EndPoints.Add("redis");
			});

			services.AddMassTransit(n => {
				if (consumers?.Any() == true) {
					n.AddConsumers(consumers.Select(n => n.Assembly).ToArray());
				}

				n.UsingRabbitMq((context, cfg) => {
					cfg.Host("rabbitmq", "/", x => { x.Username("rabbitmq"); x.Password("rabbitmq"); });
					//cfg.UseMessageRetry(x => x.None());

					cfg.UseConsumeFilter(typeof(CorrelationConsumeFilter<>), context);
					cfg.UsePublishFilter(typeof(LogPublishFilter<>), context);

					foreach (var filter in filters ?? Enumerable.Empty<Type>()) {
						cfg.UsePublishFilter(filter, context);
					}

					cfg.ConfigureEndpoints(context);
				});
			});

			services.AddOptions<MassTransitHostOptions>()
				.Configure(options => options.WaitUntilStarted = true);

			services.AddTransient(typeof(IClient<,>), typeof(RequestClient<,>));
			services.AddTransient<IJsonService, JsonService>();
			services.AddTransient<ICacheService, CacheService>();

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
