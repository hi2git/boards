using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Boards.Application.Commands.Boards;
using Boards.Application.Queries.Boards;
using Boards.Infrastructure.Web.Middlewares;

using FluentValidation;

using MassTransit;

using MediatR;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Boards.Infrastructure.Web {
	public static  class ServiceExt {

		public static IServiceCollection AddInfrastructureWeb(this IServiceCollection services, params Assembly[] consumers) {
			services.AddMediatR(ASSEMBLIES);
			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
			services.AddValidatorsFromAssemblies(ASSEMBLIES);

			services.AddHealthChecks();

			services.AddMassTransit(n => {
				if (consumers.Any() == true) n.AddConsumers(consumers);

				n.UsingRabbitMq((context, cfg) => {
					cfg.Host("rabbitmq", "/", x => { x.Username("rabbitmq"); x.Password("rabbitmq"); });
					cfg.ConfigureEndpoints(context);
				});
			});

			services.AddOptions<MassTransitHostOptions>()
				.Configure(options => {
					options.WaitUntilStarted = true;
				});

			return services;
		}

		public static IApplicationBuilder UseInfrastructureWeb(this IApplicationBuilder app) { 
			app.UseExceptionMiddleware();
			return app;
		}

		private static readonly Assembly[] ASSEMBLIES = new[] {
			typeof(BoardCreateCommand).Assembly,
			typeof(BoardGetAllQuery).Assembly,
		};

	}
}
