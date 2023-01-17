using Amazon.DynamoDBv2.DataModel;
using ArchiveNet.Domain;

namespace ArchiveNet.Repository;

[DynamoDBTable("ArtWork")]
public record ArtRecord : IArtWorkRecord
{
	public static readonly DateTime Date2000 = new DateTime(2000, 1, 1);
	public ArtRecord()
	{
		this.ArtistId = default;
		this.SK = string.Empty;
		this.Title = string.Empty;
		this.Rating = default;
		this.Uri = string.Empty;
	}

	public ArtRecord(Art art)
	{
		var primaryKey = new ArtPrimaryKey(art);

		var artist = art.Artist;
		this.ArtistId = primaryKey.ArtistId;
		this.SK = primaryKey.SK;
		this.Title = art.Title;
		this.EntryDateTime = art.EntryDateTime;
		this.Rating = art.Rating;
		this.Uri = art.Uri;

		this.EntryDateOffsetSince2000 = (this.EntryDateTime.Date - Date2000).Days;
	}

	public int ArtistId { get; set; }
	public string SK { get; set; }
	public string Title { get; set; }
	public DateTime EntryDateTime { get; set; }
	public int Rating { get; set; }
	public string Uri { get; set; }
	public long EntryDateOffsetSince2000 { get; set; }
}
