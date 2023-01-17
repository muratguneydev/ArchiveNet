using ArchiveNet.Domain;
using Microsoft.AspNetCore.Mvc;

namespace ArchiveNet.Web.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ArtController : ControllerBase
{
	private readonly IArtQuery artQuery;
	private readonly IArtCommand artCommand;
	private readonly ILogger<ArtController> logger;

	public ArtController(IArtQuery artQuery, IArtCommand artCommand, ILogger<ArtController> logger)
	{
		this.artQuery = artQuery;
		this.artCommand = artCommand;
		this.logger = logger;
	}

	// [HttpGet]
    // public Task<IEnumerable<Art>> GetAsync()
	// {
	// 	return this.artQuery.GetAsync();
	// }

	// //https://127.0.0.1:6124/swagger/index.html
	// //https://127.0.0.1:6124/Art/{artistName}
	// [HttpGet("{artistName}")]
    // public Task<IEnumerable<Art>> GetAsync(string artistName)
	// {
	// 	return this.artQuery.GetAsync(new Name(artistName));
	// }

	//https://127.0.0.1:6124/swagger/index.html
	//https://127.0.0.1:6124/Art/{artistId}
	[HttpGet("~/Art/GetByArtistId/{artistId}")]
    public Task<IEnumerable<Art>> GetAsync(int artistId)
	{
		return this.artQuery.GetAsync(artistId);
	}

//https://localhost:6124/Art/GetByDateOffset/0
	[Route("~/Art/GetByDateOffset/{dateOffset}")]
	[HttpGet]
    public Task<IEnumerable<Art>> GetByDateOffset(int dateOffset)
	{
		return this.artQuery.GetByDateOffsetAsync(dateOffset);
	}

	[HttpPut]
    public async Task<IActionResult> Put(Art art)
	{
		try
			{
				await this.artCommand.Update(art);
				
				return Ok("Art item updated successfully.");
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