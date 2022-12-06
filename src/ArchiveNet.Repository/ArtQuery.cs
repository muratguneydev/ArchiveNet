using Amazon.DynamoDBv2.DataModel;
using ArchiveNet.Domain;

namespace ArchiveNet.Repository;
public class ArtQuery
{
	private IDynamoDBContext amazonDynamoDBContext;

	public ArtQuery(IDynamoDBContext amazonDynamoDBContext)
	{
		this.amazonDynamoDBContext = amazonDynamoDBContext;
	}

	public async Task<IEnumerable<Art>> Get()
	{
		var data = await this.amazonDynamoDBContext.ScanAsync<ArtRecord>(null).GetRemainingAsync();
		return data
			.Select(artRecord => new Art(
										artRecord.GetArtist(),
										artRecord.Title,
										artRecord.Rating,
										artRecord.EntryDateTime,
										artRecord.Uri));
	}
}
