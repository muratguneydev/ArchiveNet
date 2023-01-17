using Amazon.DynamoDBv2.DataModel;
using ArchiveNet.Domain;

namespace ArchiveNet.Repository;

public class ArtistCommand : IArtistCommand
{
	private readonly IDynamoDBContext amazonDynamoDBContext;

	public ArtistCommand(ArchiveDbConfig archiveDbConfig)
	{
		this.amazonDynamoDBContext = new DBContextFactory(archiveDbConfig).GetDBContext();
	}

	public void Dispose()
	{
		this.amazonDynamoDBContext.Dispose();
	}

	public virtual Task Insert(Artist artist)
	{
		return this.amazonDynamoDBContext.SaveAsync(new ArtistRecord(artist));
	}
}