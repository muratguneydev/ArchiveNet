using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;

namespace ArchiveNet.Repository;

public class DBContextFactory
{
	private readonly ArchiveDbConfig archiveDbConfig;

	public DBContextFactory(ArchiveDbConfig archiveDbConfig)
	{
		this.archiveDbConfig = archiveDbConfig;
	}

	public IDynamoDBContext Get()
	{
		var config = new AmazonDynamoDBConfig()
		{
			ServiceURL = this.archiveDbConfig.ServiceUrl,
			AuthenticationRegion = this.archiveDbConfig.Region
		};
		var amazonDynamoDbClient = new AmazonDynamoDBClient(config);
		return new DynamoDBContext(amazonDynamoDbClient);
	}
}
