using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using ArchiveNet.Domain;

namespace ArchiveNet.Repository;
public class ArtQuery
{
	private DynamoDBContext amazonDynamoDBContext;

	public ArtQuery(IDynamoDBContext amazonDynamoDBContext)
	{
		var config = new AmazonDynamoDBConfig()
		{
			ServiceURL = "http://192.168.5.166:8000",
			AuthenticationRegion = "eu-west-2"
		};
		var amazonDynamoDbClient = new AmazonDynamoDBClient(config);
		this.amazonDynamoDBContext = new DynamoDBContext(amazonDynamoDbClient);//amazonDynamoContext
	}

	public async Task<IEnumerable<Art>> Get()
	{
		var data = await this.amazonDynamoDBContext.ScanAsync<ArtRecord>(null).GetRemainingAsync();
		return data
			.Select(artRecord => new Art(
									artRecord.GetArtist(), artRecord.Title, artRecord.Stars));
	}
}
