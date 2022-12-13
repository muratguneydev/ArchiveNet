using ArchiveNet.Domain;
using ArchiveNet.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ArchiveNet.Web.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ArtController : ControllerBase
{
	private readonly IArtQuery artQuery;
	private readonly ILogger<ArtController> logger;

	public ArtController(IArtQuery artQuery, ILogger<ArtController> logger)
	{
		this.artQuery = artQuery;
		this.logger = logger;
	}

	[HttpGet]
    public Task<IEnumerable<Art>> GetAsync()
	{
		return this.artQuery.GetAsync();
	}

	[HttpGet("artistName")]
    public Task<IEnumerable<Art>> GetAsync(string artistName)
	{
		return this.artQuery.GetAsync(new Name(artistName));
	}
}