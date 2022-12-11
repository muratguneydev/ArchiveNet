namespace ArchiveNet.Domain;

public record Art
{
	public Art(Artist artist, string title, int rating, DateTime entryDateTime, string uri)
	{
		Artist = artist;
		Title = title;
		Rating = rating;
		EntryDateTime = entryDateTime;
		Uri = uri;
	}

	public Artist Artist { get; }
	public string Title { get; }
	public int Rating { get; }
	public DateTime EntryDateTime { get; }
	public string Uri { get; }
}
