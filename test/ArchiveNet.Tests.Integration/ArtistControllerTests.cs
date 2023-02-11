using System.Net.Http.Json;
using System.Text.Json;
using ArchiveNet.Web.Api.Dtos;
using AutoFixture.NUnit3;
using Microsoft.AspNetCore.Mvc.Testing;

namespace ArchiveNet.Tests.Integration;

//https://learn.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-7.0
public class ArtistControllerTests
{
	[Test, AutoData]
	public async Task UpdateValidArtistReturnsOk(ArtistDto artistDto)
	{
		Environment.SetEnvironmentVariable("ARCHIVENET_API_DatabaseSettings__URL", "http://192.168.5.166:8003");
            
		//for more complex set up use
		// var factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
        //         {
        //             builder.ConfigureAppConfiguration((context, configBuilder) =>
        //             {
        //                 // configBuilder.AddInMemoryCollection(
        //                 //     new Dictionary<string, string?>
        //                 //     {
        //                 //         {"DatabaseSettings:Url",  "OverriddenValue"}
        //                 //     });
		// 				var projectDir = Directory.GetCurrentDirectory();
        // 				var configPath = Path.Combine(projectDir, "appsettings.json");
		// 				configBuilder.AddJsonFile(configPath);
        //             });
        //         });
		// var client = factory.CreateClient();

		var webApp = new WebApplicationFactory<Program>();
		var client = webApp.CreateClient();
		
		var addResult = await client.PutAsJsonAsync("artist", artistDto);
		addResult.EnsureSuccessStatusCode();

		var getResult = await client.GetAsync($"artist/{artistDto.Id}");
		getResult.EnsureSuccessStatusCode();
		
		var contentStream = await getResult.Content.ReadAsStreamAsync();
		var serializationOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
		var receivedArtistDto = await JsonSerializer.DeserializeAsync<ArtistDto>(contentStream, serializationOptions);
		Assert.AreEqual(artistDto.Name, receivedArtistDto!.Name);
	}
}
