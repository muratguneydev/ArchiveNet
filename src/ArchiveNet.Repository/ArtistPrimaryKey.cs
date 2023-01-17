using Amazon.DynamoDBv2.Model;
using ArchiveNet.Domain;

namespace ArchiveNet.Repository;

public record struct ArtistPrimaryKey : IArtWorkRecord
{
	public ArtistPrimaryKey(Artist artist)
		: this(artist.Id)
	{
	}

	public ArtistPrimaryKey(int artistId)
	{
		this.ArtistId = artistId;
		this.SK = $"Artist#{artistId}";
	}

	public int ArtistId { get; }
	public string SK { get; }

	public Dictionary<string, AttributeValue> Key => new Dictionary<string, AttributeValue> {
														{ nameof(this.ArtistId), new AttributeValue { N = this.ArtistId.ToString() } },
														{ nameof(this.SK), new AttributeValue { S = this.SK } }
													};
}
