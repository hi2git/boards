using System;

using Board.Infrastructure.Jwt;

using Boards.Application.Commands.Boards;
using Boards.Application.Queries.Boards;
using Boards.Infrastructure.Web;


using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


var  assemblies = new[] {
	typeof(BoardCreateCommand).Assembly,
	typeof(BoardGetAllQuery).Assembly,
};

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var config = builder.Configuration;
var env = builder.Environment;

services.AddHttpContextAccessor();
services.AddInfrastructureWeb(assemblies: assemblies);

services.AddJwtAuth(config);
services.AddSpaStaticFiles(configuration => configuration.RootPath = "ClientApp/build");
//services.AddMvc(); 


var app = builder.Build();

if (env.IsDevelopment()) {
	app.UseDeveloperExceptionPage();
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

app.UseAuthorization();


app.UseSpa(spa => {
	spa.Options.SourcePath = "ClientApp";

	if (env.IsDevelopment()) {
		spa.UseReactDevelopmentServer(npmScript: "start");
	}
});

app.Run();
