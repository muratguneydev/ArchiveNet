using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;

namespace ArchiveNet.Repository;
//https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/DynamoDBContext.ArbitraryDataMapping.html
public class ArtCommand
{
	private readonly IDynamoDBContext amazonDynamoDBContext;

	public ArtCommand(IDynamoDBContext amazonDynamoDBContext)
	{
		this.amazonDynamoDBContext = amazonDynamoDBContext;
	}

	public Task Insert(ArtRecord art)
	{
		return this.amazonDynamoDBContext.SaveAsync(art);
	}

	public Task Insert(IEnumerable<ArtRecord> arts)
	{
		var bookBatch = this.amazonDynamoDBContext.CreateBatchWrite<ArtRecord>();
		bookBatch.AddPutItems(arts);
		return bookBatch.ExecuteAsync();
	}
}

