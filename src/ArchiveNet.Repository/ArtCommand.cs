using Amazon.DynamoDBv2.DataModel;
using ArchiveNet.Domain;

namespace ArchiveNet.Repository;

//https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/DynamoDBContext.ArbitraryDataMapping.html
public class ArtCommand : IArtCommand
{
	private readonly IDynamoDBContext amazonDynamoDBContext;

	public ArtCommand(ArchiveDbConfig archiveDbConfig)
	{
		this.amazonDynamoDBContext = new DBContextFactory(archiveDbConfig).Get();
	}

	public void Dispose()
	{
		this.amazonDynamoDBContext.Dispose();
	}

	public virtual Task Insert(Art art)
	{
		return this.amazonDynamoDBContext.SaveAsync(new ArtRecord(art));
	}

	public virtual Task Insert(IEnumerable<Art> arts)
	{
		var bookBatch = this.amazonDynamoDBContext.CreateBatchWrite<ArtRecord>();
		bookBatch.AddPutItems(arts.Select(art => new ArtRecord(art)));
		return bookBatch.ExecuteAsync();
	}
}
