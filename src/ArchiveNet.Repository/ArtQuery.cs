using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using ArchiveNet.Domain;

namespace ArchiveNet.Repository;

public class ArtQuery : IArtQuery
{
	private const string TableName = "ArtWork";
	private readonly IAmazonDynamoDB amazonDynamoDBClient;

	public ArtQuery(ArchiveDbConfig archiveDbConfig)//pass context?
	{
		var contextFactory = new DBContextFactory(archiveDbConfig);
		this.amazonDynamoDBClient = contextFactory.GetDBClient();
	}

	public void Dispose()
	{
		this.amazonDynamoDBClient.Dispose();
	}

	// public virtual async Task<IEnumerable<Art>> GetAsync()
	// {
	// 	var data = await this.amazonDynamoDBClient
	// 		.ScanAsync<ArtRecord>(null)
	// 		.GetRemainingAsync();
	// 	return data
	// 		.Select(GetArt);
	// }

	public virtual async Task<IEnumerable<Art>> GetAsync(int artistId)
	{
		QueryRequest queryRequest = new QueryRequest
            {
                TableName = TableName,
                ScanIndexForward = true
            };

		var keyConditionExpression = "ArtistId = :v_artistId";// and begins_with(Title, :v_title)";
		var expressionAttributeValues = new Dictionary<string, AttributeValue>();

		expressionAttributeValues.Add(":v_artistId", new AttributeValue
		{
			N = artistId.ToString()
		});

		queryRequest.KeyConditionExpression = keyConditionExpression;
        queryRequest.ExpressionAttributeValues = expressionAttributeValues;
		var result = await this.amazonDynamoDBClient.QueryAsync(queryRequest);
        var items = result.Items;

		var artistItem = GetArtistItems(items).Single();
		var artist = GetArtist(artistItem);

		var arts = GetArtItems(items)
					.Select(item => GetArt(item, artist));

		return arts;
	}

	public virtual async Task<IEnumerable<Art>> GetByDateOffsetAsync(int lastNumberOfDays)
	{
		var entryDateOffsetSince2000 = (DateTime.UtcNow.Date - ArtRecord.Date2000).Days - lastNumberOfDays;

		QueryRequest queryRequest = new QueryRequest
            {
                TableName = TableName,
                IndexName = "EntryDateOffsetIndex",
                ScanIndexForward = true
            };

		var keyConditionExpression = "EntryDateOffsetSince2000 = :v_dateOffset";// and begins_with(IssueId, :v_issue)";
		var expressionAttributeValues = new Dictionary<string, AttributeValue>();

		expressionAttributeValues.Add(":v_dateOffset", new AttributeValue
		{
			N = entryDateOffsetSince2000.ToString()
		});

		queryRequest.KeyConditionExpression = keyConditionExpression;
        queryRequest.ExpressionAttributeValues = expressionAttributeValues;
		var result = await this.amazonDynamoDBClient.QueryAsync(queryRequest);
        var items = result.Items;

		var artists = await this.FetchArtists(items);
		var artistsDictionary = artists.ToDictionary(artist => artist.Id, artist => artist);

		return items.Select(item => GetArt(item, artistsDictionary));
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

	private static Art GetArt(Dictionary<string, AttributeValue> item, Artist artist)
	{
		return new Art(
			artist,
			item["Title"].S,
			int.Parse(item["Rating"].N),
			DateTimeUtcConverter.FromEntry(item["EntryDateTime"]),
			item["Uri"].S
		);
	}

	private static Art GetArt(Dictionary<string, AttributeValue> item, Dictionary<int, Artist> artists)
	{
		return GetArt(item, artists[GetArtistId(item)]);
	}

	private static Art GetArt(Dictionary<string, AttributeValue> attributes)
	{
		return new Art(
			new Artist(GetArtistId(attributes)),
			attributes["Title"].S,
			int.Parse(attributes["Rating"].N),
			DateTimeUtcConverter.FromEntry(attributes["EntryDateTime"]),
			attributes["Uri"].S
		);
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

	private static IEnumerable<Dictionary<string, AttributeValue>> GetArtistItems(IEnumerable<Dictionary<string, AttributeValue>> items)
	{
		return items.Where(item => item.Values.Any(value => value.S != null && value.S.StartsWith("Artist#")));
	}

	private static IEnumerable<Dictionary<string, AttributeValue>> GetArtItems(IEnumerable<Dictionary<string, AttributeValue>> items)
	{
		return items.Where(item => item.Values.Any(value => value.S != null && value.S.StartsWith("Art#")));
	}
}
