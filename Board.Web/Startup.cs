using System;
using System.Reflection;

using Board.Application;
using Board.Infrastructure.Files;
using Board.Infrastructure.Jwt;
using Board.Infrastructure.Repository;
using Board.Web.Middlewares;

using Boards.Application.Commands.Boards;
using Boards.Application.Queries.Boards;
using Boards.Infrastructure;

using BotDetect.Web;

using FluentValidation;

using MassTransit;

using MediatR;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Board.Web {
	public class Startup {
		public Startup(IConfiguration configuration) {
			this.Configuration = configuration;
			//var builder = new ConfigurationBuilder()
			//	.SetBasePath(env.ContentRootPath)
			//	.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
			//	.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
			//	.AddEnvironmentVariables();
			//Configuration = builder.Build();
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services) {
			services.Configure<IISServerOptions>(options => { options.AllowSynchronousIO = true; });

			//services.AddControllersWithViews();
			services.AddHttpContextAccessor();

			services.AddInfrastructureRepos(Configuration);
			services
				.AddInfrastructure()
				.AddInfrastructureFiles();

			services.AddMassTransit(n => {
				n.UsingRabbitMq((context, cfg) => {
					cfg.Host("192.168.1.127", "/", x => { x.Username("rabbitmq"); x.Password("rabbitmq"); });
				});
			});

			services.AddOptions<MassTransitHostOptions>()
				.Configure(options => {
					options.WaitUntilStarted = true;
				});

			services.Configure<AppSettings>(Configuration.GetSection("appSettings"));

			services.AddValidatorsFromAssemblies(ASSEMBLIES); //AddValidatorsFromAssemblyContaining<BoardCreateCommand>();
															  //services.AddValidatorsFromAssemblyContaining<BoardGetAllQuery>();

			services.AddMediatR(ASSEMBLIES);
			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

			services.AddJwtAuth(Configuration);
			//services.AddAuthorization();
			//services.AddControllers();

			// In production, the React files will be served from this directory
			services.AddSpaStaticFiles(configuration => configuration.RootPath = "ClientApp/build");

			//services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

			services.AddMvc(); // opts => opts.EnableEndpointRouting = false
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
			}
			else {
				app.UseExceptionHandler("/Error");
			}

			app.UseStaticFiles();
			app.UseSpaStaticFiles();

			app.UseExceptionMiddleware();

			app.UseCookiePolicy(new CookiePolicyOptions {
				MinimumSameSitePolicy = SameSiteMode.Strict,
				Secure = CookieSecurePolicy.SameAsRequest
			});
			app.UseSecureJwt();
			app.UseAuthentication();

			app.UseSimpleCaptcha(Configuration.GetSection("BotDetect"));
			//app.UseMvc();

			app.UseRouting();
			app.UseAuthorization();

			app.UseEndpoints(endpoints => {
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
				endpoints.MapControllers(); // Map attribute-routed API controllers //.RequireAuthorization();
				endpoints.MapDefaultControllerRoute(); // Map conventional MVC controllers using the default route
													   //endpoints.MapRazorPages();
			});

			app.UseSpa(spa => {
				spa.Options.SourcePath = "ClientApp";

				if (env.IsDevelopment()) {
					spa.UseReactDevelopmentServer(npmScript: "start");
				}
			});
		}

		private static readonly Assembly[] ASSEMBLIES = new[] {
			typeof(BoardCreateCommand).Assembly,
			typeof(BoardGetAllQuery).Assembly,
		};
	}
}
