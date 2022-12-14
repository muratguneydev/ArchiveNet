using Amazon.DynamoDBv2.DataModel;
using ArchiveNet.Domain;

namespace ArchiveNet.Repository;

[DynamoDBTable("ArtWork")]
public record ArtRecord
{
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
	}

	public string ArtistName { get; set; }
	public string AlsoKnownAsCsv { get; set; }
	public string Title { get; set; }
	public DateTime EntryDateTime { get; set; }
	public int Rating { get; set; }
	public string Uri { get; set; }

	public Artist GetArtist() => ArtistConverter.Convert(this);
}

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