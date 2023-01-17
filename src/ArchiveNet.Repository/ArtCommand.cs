using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using ArchiveNet.Domain;

namespace ArchiveNet.Repository;

//https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/DynamoDBContext.ArbitraryDataMapping.html
public class ArtCommand : IArtCommand
{
	private const string TableName = "ArtWork";

	private readonly IDynamoDBContext amazonDynamoDBContext;
	private readonly IAmazonDynamoDB amazonDynamoDBClient;

	public ArtCommand(ArchiveDbConfig archiveDbConfig)
	{
		var contextFactory = new DBContextFactory(archiveDbConfig);
		this.amazonDynamoDBContext = contextFactory.GetDBContext();
		this.amazonDynamoDBClient = contextFactory.GetDBClient();
	}

	public void Dispose()
	{
		this.amazonDynamoDBContext.Dispose();
		this.amazonDynamoDBClient.Dispose();
	}

	public virtual Task Insert(Art art)
	{
		return this.amazonDynamoDBContext.SaveAsync(new ArtRecord(art));
	}

	public virtual Task Insert(IEnumerable<Art> arts)
	{
		var batch = this.amazonDynamoDBContext.CreateBatchWrite<ArtRecord>();
		batch.AddPutItems(arts.Select(art => new ArtRecord(art)));
		return batch.ExecuteAsync();
	}

	public virtual async Task Update(Art art)
	{
		//https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/LowLevelDotNetItemCRUD.html#UpdateItemLowLevelDotNet
		var request = new UpdateItemRequest
		{
			TableName = TableName,
			Key = new ArtPrimaryKey(art).Key,
			ExpressionAttributeNames = new Dictionary<string,string>() {
				{"#T", "Title"},
				{"#R", "Rating"}
			},
			ExpressionAttributeValues = new Dictionary<string, AttributeValue>()
			{
				{":title",new AttributeValue { S = art.Title}},
				{":rating",new AttributeValue {N = art.Rating.ToString()}}
			},
			UpdateExpression = "SET #T = :title, #R = :rating"
			//ReturnValues = "ALL_NEW" // Return all the attributes of the updated item.
		};
		var response = await this.amazonDynamoDBClient.UpdateItemAsync(request);
		//response.Attributes to return the updated item's attributes. ReturnValues = "ALL_NEW"

	}
}
