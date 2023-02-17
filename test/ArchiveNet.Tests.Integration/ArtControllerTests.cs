using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using ArchiveNet.Tests.Comparers;
using ArchiveNet.Web.Api.Dtos;
using AutoFixture;
using AutoFixture.NUnit3;
using Microsoft.AspNetCore.Mvc.Testing;
using Shouldly;

namespace ArchiveNet.Tests.Integration;

public class ArtControllerTests
{
	private static readonly Fixture Fixture = new Fixture();

	[Test, AutoData]
	public async Task ShouldCreateArt(ArtistDto artist)
	{
		TestSetUp.SetDatabaseUrl(); 
            
		var webApp = new WebApplicationFactory<Program>();
		var client = webApp.CreateClient();
		
		await client.PutAsJsonAsync("artist", artist);

		var art = Fixture
					.Build<ArtDto>()
					.With(art => art.Artist, artist)
					.Create();
		var addResult = await client.PostAsJsonAsync("art", art);
		addResult.EnsureSuccessStatusCode();

		var getResult = await client.GetAsync($"art/GetByArtistId/{art.Artist.Id}");
		getResult.EnsureSuccessStatusCode();
		
		var contentStream = await getResult.Content.ReadAsStreamAsync();
		var serializationOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
		var receivedArtDtos = await JsonSerializer.DeserializeAsync<IEnumerable<ArtDto>>(contentStream, serializationOptions);

		receivedArtDtos!.ShouldBe(
			new[] { art }, new ArtDtoComparer(), ignoreOrder: true
		);
	}

	[Test, AutoData]
	public async Task ShouldUpdateArt(ArtistDto artist)
	{
		TestSetUp.SetDatabaseUrl(); 
            
		var webApp = new WebApplicationFactory<Program>();
		var client = webApp.CreateClient();
		
		await client.PutAsJsonAsync("artist", artist);

		var art = Fixture
					.Build<ArtDto>()
					.With(art => art.Artist, artist)
					.Create();
		var addResult = await client.PostAsJsonAsync("art", art);
		addResult.EnsureSuccessStatusCode();

		var getResult = await client.GetAsync($"art/GetByArtistId/{art.Artist.Id}");
		getResult.EnsureSuccessStatusCode();
		
		var contentStream = await getResult.Content.ReadAsStreamAsync();
		var serializationOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
		var receivedArtDtos = await JsonSerializer.DeserializeAsync<IEnumerable<ArtDto>>(contentStream, serializationOptions);

		receivedArtDtos!.ShouldBe(
			new[] { art }, new ArtDtoComparer(), ignoreOrder: true
		);
	}

	[Test, AutoData]
	public async Task ShouldDeleteArt(ArtistDto artist)
	{
		TestSetUp.SetDatabaseUrl(); 
        
		var webApp = new WebApplicationFactory<Program>();
		var client = webApp.CreateClient();
		
		await client.PutAsJsonAsync("artist", artist);

		var art = Fixture
					.Build<ArtDto>()
					.With(art => art.Artist, artist)
					.Create();
		var addResult = await client.PostAsJsonAsync("art", art);
		addResult.EnsureSuccessStatusCode();

		var getResult = await client.GetAsync($"art/GetByArtistId/{art.Artist.Id}");
		getResult.EnsureSuccessStatusCode();
		
		var request = new HttpRequestMessage
							{
								Content = JsonContent.Create(art),
								Method = HttpMethod.Delete,
								RequestUri = new Uri("art", UriKind.Relative)
							};
		var deleteResult = await client.SendAsync(request);
		deleteResult.EnsureSuccessStatusCode();

		var getAfterDeleteResult = await client.GetAsync($"art/GetByArtistId/{art.Artist.Id}");
		getAfterDeleteResult.StatusCode.ShouldBe(HttpStatusCode.OK);
		getAfterDeleteResult.Content.ReadAsStringAsync().Result.ShouldBe("[]");
	}
}
