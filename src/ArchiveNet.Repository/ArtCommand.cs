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
		//this.amazonDynamoDBContext.SaveAsync(new ArtRecord(art));

		var request = new UpdateItemRequest
		{
			TableName = TableName,
			Key = new ArtPrimaryKey(art).Key,
			ExpressionAttributeNames = new Dictionary<string,string>() {
				{"#T", "Title"},
				{"#R", "Rating"},
				{"#E", "EntryDateTime"},
				{"#U", "Uri"},
			},
			ExpressionAttributeValues = new Dictionary<string, AttributeValue>()
			{
				{":title", new AttributeValue { S = art.Title}},
				{":rating", new AttributeValue { N = art.Rating.ToString()} },
				{":entrydatetime", DateTimeUtcConverter.ToEntry(art.EntryDateTime) },
				{":uri", new AttributeValue { S = art.Uri}}
			},
			UpdateExpression = "SET #T = :title, #R = :rating, #E = :entrydatetime, #U = :uri"
		};

		return this.amazonDynamoDBClient.UpdateItemAsync(request);
	}

	// public virtual Task Insert(IEnumerable<Art> arts)
	// {
	// 	var batch = this.amazonDynamoDBContext.CreateBatchWrite<ArtRecord>();
	// 	batch.AddPutItems(arts.Select(art => new ArtRecord(art)));
	// 	return batch.ExecuteAsync();
	// }

	public virtual Task Update(Art art)
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
				{":title", new AttributeValue { S = art.Title}},
				{":rating", new AttributeValue { N = art.Rating.ToString()} }
			},
			UpdateExpression = "SET #T = :title, #R = :rating"
			//ReturnValues = "ALL_NEW" // Return all the attributes of the updated item.
		};

		return this.amazonDynamoDBClient.UpdateItemAsync(request);
		//var response = await this.amazonDynamoDBClient.UpdateItemAsync(request);
		//response.Attributes to return the updated item's attributes. ReturnValues = "ALL_NEW"

	}

	public virtual Task Delete(Art art)
	{
		var request = new DeleteItemRequest
		{
			TableName = TableName,
			Key = new ArtPrimaryKey(art).Key
		};

		return this.amazonDynamoDBClient.DeleteItemAsync(request);
	}
}
