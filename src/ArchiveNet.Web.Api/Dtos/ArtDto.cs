using ArchiveNet.Domain;

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

	public ArtDto(Art art)
		: this(new ArtistDto(art.Artist), art.Title, art.Rating, art.EntryDateTime, art.Uri)
	{
		
	}

	public ArtistDto Artist { get; set; } = new ArtistDto();
	public string Title { get; set; } = string.Empty;
	public int Rating { get; set; } = default;
	public DateTime EntryDateTime { get; set; } = new DateTime();
	public string Uri { get; set; } = string.Empty;

	public Art ToArt() => new Art(this.Artist.ToArtist(), this.Title, this.Rating, this.EntryDateTime, this.Uri);
}
