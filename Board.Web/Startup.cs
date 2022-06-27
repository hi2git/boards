using System;

using Board.Application;
using Board.Infrastructure.Files;
using Board.Infrastructure.Jwt;
using Board.Infrastructure.Repository;

using Boards.Infrastructure;
using Boards.Infrastructure.Web;

using BotDetect.Web;


using MassTransit;


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

			services
				.AddInfrastructure()
				.AddInfrastructureRepos(Configuration)
				.AddInfrastructureFiles()
				.AddInfrastructureWeb();

			services.Configure<AppSettings>(Configuration.GetSection("appSettings"));

			services.AddJwtAuth(Configuration);

			// In production, the React files will be served from this directory
			services.AddSpaStaticFiles(configuration => configuration.RootPath = "ClientApp/build");

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

			app.UseInfrastructureWeb();

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

		
	}
}
