using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using ArchiveNet.Domain;

namespace ArchiveNet.Repository;

public class ArtistCommand : IArtistCommand
{
	private const string TableName = "ArtWork";

	private readonly IDynamoDBContext amazonDynamoDBContext;
	private readonly IAmazonDynamoDB amazonDynamoDBClient;

	public ArtistCommand(ArchiveDbConfig archiveDbConfig)
	{
		var contextFactory = new DBContextFactory(archiveDbConfig);
		this.amazonDynamoDBContext = contextFactory.GetDBContext();
		this.amazonDynamoDBClient = contextFactory.GetDBClient();

	}

	public void Dispose()
	{
		this.amazonDynamoDBContext.Dispose();
	}

	public virtual Task Insert(Artist artist)
	{
		//return this.amazonDynamoDBContext.SaveAsync(new ArtistRecord(artist));
		return Update(artist);
	}

	public virtual async Task Update(Artist artist)
	{
		var request = new UpdateItemRequest
		{
			TableName = TableName,
			Key = new ArtistPrimaryKey(artist).Key,
			ExpressionAttributeNames = new Dictionary<string,string>() {
				{"#N", "Name"}
			},
			ExpressionAttributeValues = new Dictionary<string, AttributeValue>()
			{
				{":name",new AttributeValue { S = artist.Name.Value}}
			},
			UpdateExpression = "SET #N = :name"
			//ReturnValues = "ALL_NEW" // Return all the attributes of the updated item.
		};
		var response = await this.amazonDynamoDBClient.UpdateItemAsync(request);
		//response.Attributes to return the updated item's attributes. ReturnValues = "ALL_NEW"
	}
}