using Amazon.DynamoDBv2.DataModel;
using ArchiveNet.Domain;

namespace ArchiveNet.Repository;

[DynamoDBTable("ArtWork")]
public record ArtistRecord : IArtWorkRecord
{
	public ArtistRecord()
	{
		this.ArtistId = default;
		this.SK = string.Empty;
		this.Name = string.Empty;
		this.AlsoKnownAsCsv = string.Empty;
	}

	public ArtistRecord(Artist artist)
	{
		var primaryKey = new ArtistPrimaryKey(artist);
		this.ArtistId = primaryKey.ArtistId;
		this.SK = primaryKey.SK;
		this.Name = artist.Name.ToString();
		this.AlsoKnownAsCsv = string.Join(',', artist.AlsoKnownAs.Select(aka => aka.ToString()));
	}

	public int ArtistId { get; set; }
	public string SK { get; set; }
	public string Name { get; set; }
	public string AlsoKnownAsCsv { get; set; }
}
