using Amazon.DynamoDBv2.Model;
using ArchiveNet.Domain;

namespace ArchiveNet.Repository;

public record struct ArtPrimaryKey : IArtWorkRecord
{
	public ArtPrimaryKey(Art art)
	{
		this.ArtistId = art.Artist.Id;
		this.SK = $"Art#{art.Uri}";
	}

	public int ArtistId { get; }
	public string SK { get; }

	public Dictionary<string, AttributeValue> Key => new Dictionary<string, AttributeValue> {
														{ nameof(this.ArtistId), new AttributeValue { N = this.ArtistId.ToString() } },
														{ nameof(this.SK), new AttributeValue { S = this.SK } }
													};
}