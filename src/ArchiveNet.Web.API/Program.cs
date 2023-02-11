using ArchiveNet.Domain;
using ArchiveNet.Repository;
using ArchiveNet.Web.Api;

var builder = GetApplicationBuilderWithChangedContentRootPath(args);

// Add services to the container.
builder.Configuration.AddEnvironmentVariables(prefix: "ARCHIVENET_API_");
var corsPolicyName = AddCors(builder);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();//https://127.0.0.1:6124/swagger/index.html

// builder.Configuration.AddEnvironmentVariables(prefix: "ARCHIVENET_API_");

//builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));

RegisterRepositoryServices(builder);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(corsPolicyName);
app.UseAuthorization();

app.MapControllers();

app.Run();

static void RegisterArtQuery(WebApplicationBuilder builder, ArchiveDbConfig archiveDbConfig)
{
	var artQuery = new ArtQuery(archiveDbConfig);
	var decryptedArtQuery = new DecryptedArtQueryDecorator(artQuery, new Cryptor());
	builder.Services.AddSingleton<IArtQuery>(decryptedArtQuery);
}

static void RegisterArtCommand(WebApplicationBuilder builder, ArchiveDbConfig archiveDbConfig)
{
	var artCommand = new ArtCommand(archiveDbConfig);
	var encryptedArtCommand = new EncryptedArtCommandDecorator(artCommand, new Cryptor());
	builder.Services.AddSingleton<IArtCommand>(encryptedArtCommand);
}

static void RegisterArtistQuery(WebApplicationBuilder builder, ArchiveDbConfig archiveDbConfig)
{
	var artistQuery = new ArtistQuery(archiveDbConfig);
	var decryptedArtistQuery = new DecryptedArtistQueryDecorator(artistQuery, new Cryptor());
	builder.Services.AddSingleton<IArtistQuery>(decryptedArtistQuery);
}

static void RegisterArtistCommand(WebApplicationBuilder builder, ArchiveDbConfig archiveDbConfig)
{
	var artistCommand = new ArtistCommand(archiveDbConfig);
	var encryptedArtistCommand = new EncryptedArtistCommandDecorator(artistCommand, new Cryptor());
	builder.Services.AddSingleton<IArtistCommand>(encryptedArtistCommand);
}

static WebApplicationBuilder GetApplicationBuilderWithChangedContentRootPath(string[] appArgs)
{
	//default:
	//var builder = WebApplication.CreateBuilder(args);
	return WebApplication.CreateBuilder(new WebApplicationOptions
	{
		Args = appArgs,
		//This is to be able to access appsettings.json from the executable location.
		//This can also be done using the --contentRoot command line argument
		ContentRootPath = AppContext.BaseDirectory
		//ApplicationName = typeof(Program).Assembly.FullName,
		//ContentRootPath = Directory.GetCurrentDirectory(),
		//EnvironmentName = Environments.Staging,
		//WebRootPath = "customwwwroot"
	});
}

static void RegisterRepositoryServices(WebApplicationBuilder builder)
{
	var archiveDbConfig = new ArchiveDbConfig(
		builder.Configuration["DatabaseSettings:Url"]!,
		builder.Configuration["DatabaseSettings:Region"]!);

	RegisterArtQuery(builder, archiveDbConfig);
	RegisterArtCommand(builder, archiveDbConfig);

	RegisterArtistQuery(builder, archiveDbConfig);
	RegisterArtistCommand(builder, archiveDbConfig);
}

static string AddCors(WebApplicationBuilder builder)
{
	var myAllowSpecificOrigins = "_myAllowSpecificOrigins";
	var guiCORSOrigin = builder.Configuration["CORSOrigins:Gui"];
	if (guiCORSOrigin == null)
		throw new Exception("CORSOrigins:Gui not found.");
	builder.Services.AddCors(options =>
	{
		options.AddPolicy(name: myAllowSpecificOrigins,
						  policy =>
						  {
							  policy
							  	.WithOrigins(guiCORSOrigin)
								.AllowAnyHeader()
								.AllowAnyMethod();
						  });
	});
	return myAllowSpecificOrigins;
}