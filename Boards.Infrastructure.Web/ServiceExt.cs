using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Boards.Commons.Application;
using Boards.Infrastructure.Web.Clients;
using Boards.Infrastructure.Web.Middlewares;

using FluentValidation;

using MassTransit;

using MediatR;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Boards.Infrastructure.Web {
	public static  class ServiceExt {

		public static IServiceCollection AddInfrastructureWeb(this IServiceCollection services, Assembly[] assemblies, Action<IBusRegistrationConfigurator>? register = null) {
			services.AddMediatR(assemblies);
			services.AddValidatorsFromAssemblies(assemblies);
			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

			services.AddEndpointsApiExplorer().AddSwaggerGen();
			services.AddHealthChecks();

			services.AddMassTransit(n => {
				//if (consumers.Any() == true) n.AddConsumers(consumers);
				register?.Invoke(n);

				n.UsingRabbitMq((context, cfg) => {
					cfg.Host("rabbitmq", "/", x => { x.Username("rabbitmq"); x.Password("rabbitmq"); });
					//cfg.UseMessageRetry(x => x.None());
					cfg.ConfigureEndpoints(context);
				});
			});

			services.AddOptions<MassTransitHostOptions>()
				.Configure(options => options.WaitUntilStarted = true);

			services.AddTransient(typeof(IClient<,>), typeof(RequestClient<,>));

			return services;
		}

		public static WebApplication UseInfrastructureWeb(this WebApplication app) { 
			app.UseExceptionMiddleware();

			app.UseSwagger();
			app.UseSwaggerUI();

			app.MapControllers();

			app.MapHealthChecks("/hc");

			return app;
		}

	}
}
