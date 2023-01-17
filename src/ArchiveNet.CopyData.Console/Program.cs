using System.Data.SQLite;
using ArchiveNet.Domain;
using ArchiveNet.Repository;
using Microsoft.EntityFrameworkCore;

var connectionStringBuilder = new SQLiteConnectionStringBuilder
	{
		DataSource = "/home/vscode/.aws/archive.db"// Configuration["ArchiveApiSettings:DatabaseConnectionDataSource"]
	};
var sourceConnectionString = connectionStringBuilder.ToString();


using (var artCommand = new ArtCommand(new ArchiveDbConfig("http://192.168.5.166:8000", "eu-west-2")))
using (var artistCommand = new ArtistCommand(new ArchiveDbConfig("http://192.168.5.166:8000", "eu-west-2")))
using (var context = new ArchiveDbContext(sourceConnectionString))
{
    var allScenes = context.Scenes!
				.AsNoTracking()
				.Include(s => s.SceneStars)
				.ThenInclude(ss => ss.Star);

	var hashSetScene = new HashSet<string>();
	var hashSetArtist = new HashSet<Artist>();
	foreach (var scene in allScenes)
	{
		foreach (var sceneStar in scene.SceneStars)
		{
			var artist = await WriteStar(hashSetArtist, sceneStar.Star, artistCommand);
			string sceneName = PopulateUniqueName(hashSetScene, scene, sceneStar);

			await artCommand.Insert(
							new Art(
								artist,
								sceneName,
								scene.Rating,
								scene.EntryDateTime,
								scene.Url));
		}
	}
}

async Task<Artist> WriteStar(HashSet<Artist> hashSetArtist, Star star, IArtistCommand artistCommand)
{
	var artist = new Artist((int)star.Id, new Name(star.Name));
	if (hashSetArtist.TryGetValue(artist, out var existingArtist))
		return existingArtist;
	

	await artistCommand.Insert(artist);
	hashSetArtist.Add(artist);
	return artist;
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