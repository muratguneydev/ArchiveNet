using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;

namespace ArchiveNet.Repository;
//https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/DynamoDBContext.ArbitraryDataMapping.html
public class ArtCommand
{
	private readonly DynamoDBContext amazonDynamoDBContext;

	public ArtCommand(IDynamoDBContext amazonDynamoDBContext)
	{
		var config = new AmazonDynamoDBConfig()
		{
			ServiceURL = "http://192.168.5.166:8000",
			AuthenticationRegion = "eu-west-2"
		};
		var amazonDynamoDbClient = new AmazonDynamoDBClient(config);
		this.amazonDynamoDBContext = new DynamoDBContext(amazonDynamoDbClient);
	}

	public Task Insert(ArtRecord art)
	{
		return this.amazonDynamoDBContext.SaveAsync(art);
	}
}

