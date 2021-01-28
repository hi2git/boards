using Board.Application;
using Board.Infrastructure.Files;
using Board.Infrastructure.Jwt;
using Board.Infrastructure.Repository;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Board.Web {
	public class Startup {
		public Startup(IConfiguration configuration) => this.Configuration = configuration;

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services) {
			services.AddControllersWithViews();
			services.AddHttpContextAccessor();

			services.AddDbContext(Configuration);
			services.AddFiles();

			services.Configure<AppSettings>(Configuration.GetSection("appSettings"));

			services.AddJwtAuth(Configuration);

			// In production, the React files will be served from this directory
			services.AddSpaStaticFiles(configuration => configuration.RootPath = "ClientApp/build");
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


			app.UseCookiePolicy(new CookiePolicyOptions {
				MinimumSameSitePolicy = SameSiteMode.Strict,
				Secure = CookieSecurePolicy.SameAsRequest
			});
			app.UseSecureJwt();
			app.UseAuthentication();
			app.UseRouting();
			app.UseAuthorization();

			app.UseEndpoints(endpoints => {
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
