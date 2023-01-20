using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using ArchiveNet.Domain;

namespace ArchiveNet.Repository;

public class ArtistQuery : IArtistQuery
{
	private const string TableName = "ArtWork";
	private readonly IAmazonDynamoDB amazonDynamoDBClient;

	public ArtistQuery(ArchiveDbConfig archiveDbConfig)
	{
		var contextFactory = new DBContextFactory(archiveDbConfig);
		this.amazonDynamoDBClient = contextFactory.GetDBClient();
	}

	public void Dispose()
	{
		this.amazonDynamoDBClient.Dispose();
	}

	public virtual async Task<Artist> GetAsync(int artistId)
	{
		var request = new GetItemRequest
		{
			TableName = TableName,
			Key = new ArtistPrimaryKey(artistId).Key
		};
		var response = await this.amazonDynamoDBClient.GetItemAsync(request);

		var result = response.Item;
		return GetArtist(result);
	}

	public virtual async Task<IEnumerable<Artist>> GetAsync()
	{
		//Note: This doesn't work. HASH key of an index can only be queried using equals.
		var queryRequest = new QueryRequest
		{
			TableName = TableName,
			IndexName = "SKIndex",
			ScanIndexForward = true
		};

		var keyConditionExpression = "begins_with(SK, :v_artistprefix)";
		var expressionAttributeValues = new Dictionary<string, AttributeValue> {
			{":v_artistprefix", new AttributeValue { S = "Artist#" }
		}};

		queryRequest.KeyConditionExpression = keyConditionExpression;
		queryRequest.ExpressionAttributeValues = expressionAttributeValues;
		var result = await this.amazonDynamoDBClient.QueryAsync(queryRequest);
		var items = result.Items;

		var artists = await this.FetchArtists(items);
		return artists;
	}

	private async Task<IEnumerable<Artist>> FetchArtists(IEnumerable<Dictionary<string, AttributeValue>> items)
	{
		var artistIds = items.Select(GetArtistId).Distinct();
		var request = new BatchGetItemRequest
		{
			RequestItems = new Dictionary<string, KeysAndAttributes>()
								{
									{
										TableName,
										new KeysAndAttributes {
											Keys = artistIds
													.Select(artistId => new ArtistPrimaryKey(artistId).Key)
													.ToList()
										}
									}
								}
		};
		var response = await this.amazonDynamoDBClient.BatchGetItemAsync(request);
		var artistItems = response.Responses.Single().Value;
		return artistItems.Select(GetArtist);

		// Any unprocessed keys? could happen if you exceed ProvisionedThroughput or some other error.
		// Dictionary<string, KeysAndAttributes> unprocessedKeys = response.UnprocessedKeys;
		// foreach (var unprocessedTableKeys in unprocessedKeys)
		// {
		//     // Print table name.
		//     Console.WriteLine(unprocessedTableKeys.Key);
		//     // Print unprocessed primary keys.
		//     foreach (var key in unprocessedTableKeys.Value.Keys)
		//     {
		//         PrintItem(key);
		//     }
		// }

		// request.RequestItems = unprocessedKeys;
	}

	private static Artist GetArtist(Dictionary<string, AttributeValue> attributes)
	{
		return new Artist(
			GetArtistId(attributes),
			new Name(attributes["Name"].S)
		);
	}

	private static int GetArtistId(Dictionary<string, AttributeValue> attributes)
	{
		return int.Parse(attributes["ArtistId"].N);
	}
}
