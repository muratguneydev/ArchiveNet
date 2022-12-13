using Amazon.DynamoDBv2.DataModel;
using ArchiveNet.Domain;

namespace ArchiveNet.Repository;

public class ArtQuery : IArtQuery
{
	private IDynamoDBContext amazonDynamoDBContext;

	public ArtQuery(ArchiveDbConfig archiveDbConfig)//pass context?
	{
		this.amazonDynamoDBContext = new DBContextFactory(archiveDbConfig).Get();
	}

	public void Dispose()
	{
		this.amazonDynamoDBContext.Dispose();
	}

	public virtual async Task<IEnumerable<Art>> GetAsync()
	{
		var data = await this.amazonDynamoDBContext.ScanAsync<ArtRecord>(null).GetRemainingAsync();
		return data
			.Select(GetArt);
	}

	public virtual async Task<IEnumerable<Art>> GetAsync(Name artistName)
	{
		var data = await this.amazonDynamoDBContext
			.QueryAsync<ArtRecord>(artistName.Value)
			.GetRemainingAsync();
		return data
			.Select(GetArt);
	}

	private static Art GetArt(ArtRecord artRecord)
	{
		return new Art(
						artRecord.GetArtist(),
						artRecord.Title,
						artRecord.Rating,
						artRecord.EntryDateTime,
						artRecord.Uri);
	}
}
