using System;

using Board.Application;
using Board.Infrastructure.Files;
using Board.Infrastructure.Jwt;
using Board.Infrastructure.Repository;

using Boards.Application.Commands.Boards;
using Boards.Application.Queries.Boards;
using Boards.Domain.Contracts.Posts;
using Boards.Infrastructure;
using Boards.Infrastructure.Web;

using BotDetect.Web;

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

services
	.AddInfrastructure()
	.AddInfrastructureRepos(config)
	.AddInfrastructureFiles()
	.AddInfrastructureWeb(assemblies: assemblies); // , n => n.AddRequestClient<PostSortedEvent>(new Uri($"queue:{typeof(PostSortAllMsg).FullName}"))

services.Configure<AppSettings>(config.GetSection("appSettings"));
services.AddJwtAuth(config);
services.AddSpaStaticFiles(configuration => configuration.RootPath = "ClientApp/build");
services.AddMvc(); // opts => opts.EnableEndpointRouting = false


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

app.UseSimpleCaptcha(config.GetSection("BotDetect"));

app.UseAuthorization();


app.UseSpa(spa => {
	spa.Options.SourcePath = "ClientApp";

	if (env.IsDevelopment()) {
		spa.UseReactDevelopmentServer(npmScript: "start");
	}
});

app.Run();
