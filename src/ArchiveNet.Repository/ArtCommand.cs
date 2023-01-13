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
		var batch = this.amazonDynamoDBContext.CreateBatchWrite<ArtRecord>();
		batch.AddPutItems(arts.Select(art => new ArtRecord(art)));
		return batch.ExecuteAsync();
	}

	public virtual async Task Update(Art art)
	{
		// var data = (await this.amazonDynamoDBContext
		// 	.LoadAsync<IEnumerable<ArtRecord>>(art.Artist.Name.Value)
		// ).ToList();

		// data.Remove(data.Single(artItem => artItem.Uri == art.Uri));
		// data.Add(new ArtRecord(art));
		
		// await this.amazonDynamoDBContext.SaveAsync(data);
		await this.amazonDynamoDBContext.DeleteAsync(new ArtRecord(art));
		await this.Insert(art);

	}
}
