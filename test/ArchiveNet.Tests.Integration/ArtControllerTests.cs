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
	public async Task PostValidArtReturnsOk(ArtistDto artist)
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

}
