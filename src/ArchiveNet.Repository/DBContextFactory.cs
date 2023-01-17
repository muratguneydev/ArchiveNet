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

	public virtual IDynamoDBContext GetDBContext()
	{
		return new DynamoDBContext(this.GetDBClient());
	}

	public virtual IAmazonDynamoDB GetDBClient()
	{
		var config = new AmazonDynamoDBConfig()
		{
			ServiceURL = this.archiveDbConfig.ServiceUrl,
			AuthenticationRegion = this.archiveDbConfig.Region
		};
		return new AmazonDynamoDBClient(config);
	}
}
