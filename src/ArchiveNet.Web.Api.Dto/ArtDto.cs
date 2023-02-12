namespace ArchiveNet.Web.Api.Dtos;

public record ArtDto
{
	public ArtDto()
	{
		
	}

	public ArtDto(ArtistDto artist, string title, int rating, DateTime entryDateTime, string uri)
	{
		Artist = artist;
		Title = title;
		Rating = rating;
		EntryDateTime = entryDateTime;
		Uri = uri;
	}

	public ArtistDto Artist { get; set; } = new ArtistDto();
	public string Title { get; set; } = string.Empty;
	public int Rating { get; set; } = default;
	public DateTime EntryDateTime { get; set; } = new DateTime();
	public string Uri { get; set; } = string.Empty;
}
