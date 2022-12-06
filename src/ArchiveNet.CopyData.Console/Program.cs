// See https://aka.ms/new-console-template for more information
using System.Data.SQLite;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using ArchiveNet.Domain;
using ArchiveNet.Repository;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, World!");

var connectionStringBuilder = new SQLiteConnectionStringBuilder
	{
		DataSource = "/home/vscode/.aws/archive.db"// Configuration["ArchiveApiSettings:DatabaseConnectionDataSource"]
	};
var sourceConnectionString = connectionStringBuilder.ToString();

var destinationDatabaseConfig = new AmazonDynamoDBConfig()
{
	ServiceURL = "http://192.168.5.166:8000",
	AuthenticationRegion = "eu-west-2"
};
var amazonDynamoDbClient = new AmazonDynamoDBClient(destinationDatabaseConfig);

using (var amazonDynamoDBContext = new DynamoDBContext(amazonDynamoDbClient))
using (var context = new ArchiveDbContext(sourceConnectionString))
{
    var allScenes = context.Scenes!
				.AsNoTracking()
				.Include(s => s.SceneStars)
				.ThenInclude(ss => ss.Star);

	var artCommand = new ArtCommand(amazonDynamoDBContext);

	var hashSet = new HashSet<string>();
	foreach (var scene in allScenes)
	{
		foreach (var sceneStar in scene.SceneStars)
		{
			string sceneName = PopulateUniqueName(hashSet, scene, sceneStar);

			await artCommand.Insert(
				new ArtRecord(
							new Art(
								new Artist(new Name(sceneStar.Star.Name), EmptyAlsoKnownAs.AlsoKnownAs),
								sceneName,
								scene.Rating,
								scene.EntryDateTime,
								scene.Url)));
		}
	}
}

static string PopulateUniqueName(HashSet<string> hashSet, Scene scene, SceneStar sceneStar)
{
	var sceneName = scene.Name;
	var key = $"{sceneStar.Star.Name}-{sceneName}";
	int i = 1;
	while (hashSet.Contains(key))
	{
		sceneName = $"{scene.Name}{i++}";
		key = $"{sceneStar.Star.Name}-{sceneName}";
	}
	hashSet.Add(key);
	return sceneName;
}