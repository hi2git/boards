using System;

using Boards.Front.Application.Commands.Boards;
using Boards.Front.Application.Queries.Boards;
using Boards.Front.Infrastructure.Jwt;
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

services.AddHttpContextAccessor();
services.AddWeb(builder.Logging, assemblies: assemblies);

services.AddJwtAuth(builder.Configuration);
services.AddSpaStaticFiles(configuration => configuration.RootPath = "ClientApp/build");

var app = builder.Build();

if (builder.Environment.IsDevelopment()) {
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

	if (builder.Environment.IsDevelopment()) {
		spa.UseReactDevelopmentServer(npmScript: "start");
	}
});

app.Run();
