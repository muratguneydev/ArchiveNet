using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using ArchiveNet.Domain;

namespace ArchiveNet.Repository;
public class SingerQuery
{
	private DynamoDBContext context;

	public SingerQuery(IAmazonDynamoDB amazonDynamoDbClient)
	{
		this.context = new DynamoDBContext(amazonDynamoDbClient);
	}

	public IEnumerable<Singer> Get()
	{
		return new Singer[] {};
	}
}
