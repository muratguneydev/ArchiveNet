using Amazon.DynamoDBv2.DataModel;
using ArchiveNet.Domain;

namespace ArchiveNet.Repository;

[DynamoDBTable("ArtWork")]
public record ArtRecord
{
	public static readonly DateTime Date2000 = new DateTime(2000, 1, 1);
	public ArtRecord()
	{
		this.ArtistName = string.Empty;
		this.AlsoKnownAsCsv = string.Empty;
		this.Title = string.Empty;
		this.Rating = default;
		this.Uri = string.Empty;
	}

	public ArtRecord(Art art)
	{
		this.ArtistName = art.Artist.Name.ToString();
		this.AlsoKnownAsCsv = string.Join(',', art.Artist.AlsoKnownAs.Select(aka => aka.ToString()));
		this.Title = art.Title;
		this.EntryDateTime = art.EntryDateTime;
		this.Rating = art.Rating;
		this.Uri = art.Uri;

		this.EntryDateOffsetSince2000 = (this.EntryDateTime.Date - Date2000).Days;
	}

	public string ArtistName { get; set; }
	public string AlsoKnownAsCsv { get; set; }
	public string Title { get; set; }
	public DateTime EntryDateTime { get; set; }
	public int Rating { get; set; }
	public string Uri { get; set; }
	//public DateOnly EntryDate => DateOnly.FromDateTime(this.EntryDateTime);
	public long EntryDateOffsetSince2000 { get; set; }

	public Artist GetArtist() => ArtistConverter.Convert(this);
}

// [DynamoDBTable("ArtWork2")]
// public record ArtRecord2
// {
// 	public static readonly DateTime Date2000 = new DateTime(2000, 1, 1);
// 	public ArtRecord2()
// 	{
// 		this.ArtistName = string.Empty;
// 		this.AlsoKnownAsCsv = string.Empty;
// 		this.Title = string.Empty;
// 		this.Rating = default;
// 		this.Uri = string.Empty;
// 	}

// 	public ArtRecord2(Art art)
// 	{
// 		this.ArtistName = art.Artist.Name.ToString();
// 		this.AlsoKnownAsCsv = string.Join(',', art.Artist.AlsoKnownAs.Select(aka => aka.ToString()));
// 		this.Title = art.Title;
// 		this.EntryDateTime = art.EntryDateTime;
// 		this.Rating = art.Rating;
// 		this.Uri = art.Uri;

// 		this.EntryDateOffsetSince2000 = (this.EntryDateTime.Date - Date2000).Days;
// 	}

// 	public string ArtistName { get; set; }
// 	public string AlsoKnownAsCsv { get; set; }
// 	public string Title { get; set; }
// 	public DateTime EntryDateTime { get; set; }
// 	public int Rating { get; set; }
// 	public string Uri { get; set; }
// 	//public DateOnly EntryDate => DateOnly.FromDateTime(this.EntryDateTime);
// 	public long EntryDateOffsetSince2000 { get; set; }

// 	public Artist GetArtist() => ArtistConverter.Convert(this);
// }

// public record ArtistRecord : IArtWorkRecord
// {
// 	public static readonly DateTime Date2000 = new DateTime(2000, 1, 1);
// 	public ArtistRecord()
// 	{
// 		this.ArtistId = default;
// 		this.SK = string.Empty;
// 		this.ArtistName = string.Empty;
// 		this.AlsoKnownAsCsv = string.Empty;
// 	}

// 	public ArtistRecord(Artist artist)
// 	{
// 		this.ArtistId = default;
// 		this.SK = string.Empty;
// 		this.ArtistName = artist.Name.ToString();
// 		this.AlsoKnownAsCsv = string.Join(',', artist.AlsoKnownAs.Select(aka => aka.ToString()));
// 	}

// 	public int ArtistId { get; set; }
// 	public string SK { get; set; }
// 	public string ArtistName { get; set; }
// 	public string AlsoKnownAsCsv { get; set; }
// }

// public interface IArtWorkRecord
// {
// 	int ArtistId { get; set; }
// 	string SK { get; set; }
// }

/*
DynamoDB is schemaless (except the key schema)
Don't include any non-key attribute definitions in AttributeDefinitions.
AttributeName=AlsoKnownAsCsv,AttributeType=S \

aws dynamodb create-table \
	--table-name ArtWork \
	--attribute-definitions \
		AttributeName=FirstName,AttributeType=S \
		AttributeName=LastName,AttributeType=S \
		AttributeName=Title,AttributeType=S \
		AttributeName=Stars,AttributeType=N \
	--key-schema AttributeName=FirstName,KeyType=HASH AttributeName=LastName,KeyType=RANGE \
	--provisioned-throughput ReadCapacityUnits=1,WriteCapacityUnits=1 \
	--endpoint-url http://192.168.5.166:8000 \
	--global-secondary-indexes \
        "[
            {
                \"IndexName\": \"TitleIndex\",
                \"KeySchema\": [{\"AttributeName\":\"Title\",\"KeyType\":\"HASH\"}],
                \"Projection\":{
                    \"ProjectionType\":\"ALL\"
                },
                \"ProvisionedThroughput\": {
                    \"ReadCapacityUnits\": 10,
                    \"WriteCapacityUnits\": 5
                }
            },
	    {
                \"IndexName\": \"StarsIndex\",
                \"KeySchema\": [{\"AttributeName\":\"Stars\",\"KeyType\":\"HASH\"}],
                \"Projection\":{
                    \"ProjectionType\":\"ALL\"
                },
                \"ProvisionedThroughput\": {
                    \"ReadCapacityUnits\": 10,
                    \"WriteCapacityUnits\": 5
                }
            }
        ]"

*/