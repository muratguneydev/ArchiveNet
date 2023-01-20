using ArchiveNet.Domain;
using Microsoft.AspNetCore.Mvc;

namespace ArchiveNet.Web.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ArtistController : ControllerBase
{
	private readonly IArtistQuery artistQuery;
	private readonly IArtistCommand artistCommand;
	private readonly ILogger<ArtistController> logger;

	public ArtistController(IArtistQuery artistQuery, IArtistCommand artistCommand, ILogger<ArtistController> logger)
	{
		this.artistQuery = artistQuery;
		this.artistCommand = artistCommand;
		this.logger = logger;
	}

	//https://127.0.0.1:6124/Artist
	[HttpGet("~/Artist")]
    public Task<IEnumerable<Artist>> GetAsync()
	{
		return this.artistQuery.GetAsync();
	}

	//https://127.0.0.1:6124/Artist/2
	[HttpGet("~/Artist/{artistId}")]
    public Task<Artist> GetAsync(int artistId)
	{
		return this.artistQuery.GetAsync(artistId);
	}

	[HttpPut]
    public async Task<IActionResult> Put(Artist artist)
	{
		try
			{
				await this.artistCommand.Update(artist);
				
				return Ok("Artist updated successfully.");
			}
			catch (ArgumentException ae)//catches ArgumentNullException too
			{
				return BadRequest(ae.Message);
			}
			catch (KeyNotFoundException knfe)
			{
				return NotFound(knfe.Message);
			}

		
	}
}