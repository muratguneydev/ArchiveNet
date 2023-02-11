using ArchiveNet.Domain;
using ArchiveNet.Web.Api.Dtos;
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

	[HttpGet("~/Art/GetByArtistId/{artistId}")]
    public async Task<IEnumerable<ArtDto>> GetAsync(int artistId)
	{
		return (await this.artQuery.GetAsync(artistId))
			.Select(art => new ArtDto(art));
	}

	[Route("~/Art/GetByDateOffset/{dateOffset}")]
	[HttpGet]
    public async Task<IEnumerable<ArtDto>> GetByDateOffset(int dateOffset)
	{
		return (await this.artQuery.GetByDateOffsetAsync(dateOffset))
			.Select(art => new ArtDto(art));
	}

	//https://localhost:6124/Art
	[HttpPut]
    public async Task<IActionResult> Put(ArtDto art)
	{
		try
		{
			await this.artCommand.Update(art.ToArt());
			
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

	//https://localhost:6124/Art
	[HttpPost]
    public async Task<IActionResult> Post(ArtDto art)
	{
		try
		{
			await this.artCommand.Insert(art.ToArt());
			
			return Ok("Art item inserted successfully.");
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