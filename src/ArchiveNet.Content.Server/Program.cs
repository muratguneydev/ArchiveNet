using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables(prefix: "ARCHIVENET_CONTENT_");

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

UseStaticFiles(builder, app);

app.UseAuthorization();

app.MapControllers();

app.Run();

static void UseStaticFiles(WebApplicationBuilder builder, WebApplication app)
{
	//https://learn.microsoft.com/en-us/aspnet/core/fundamentals/static-files?view=aspnetcore-7.0
	app.UseStaticFiles(new StaticFileOptions
	{
		FileProvider = new PhysicalFileProvider(
			   //Path.Combine(builder.Environment.ContentRootPath, "Content", "Artist", "Images")),
			   //Path.GetFullPath(Path.Combine(builder.Environment.ContentRootPath,@"../Content/Artist/Images"))),
			builder.Configuration["ARTIST_IMAGE_DIRECTORY"]!),
		RequestPath = "/Artist/Images",
		OnPrepareResponse = ctx =>
		{
			ApplyContentCaching(ctx);
		}
	});
}

static void ApplyContentCaching(StaticFileResponseContext ctx)
{
	var cacheMaxAgeOneWeek = (60 * 60 * 24 * 7).ToString();
	ctx.Context.Response.Headers.Append("Cache-Control", $"public, max-age={cacheMaxAgeOneWeek}");
}