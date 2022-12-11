using ArchiveNet.Domain;

namespace ArchiveNet.Repository;

public static class ArtistConverter
{
	public static Artist Convert(ArtRecord artRecord)
	{
		return new Artist(
			new Name(artRecord.ArtistName),
			new NameCollection(artRecord.AlsoKnownAsCsv
											.Split(',')
											.Select(nameString => new Name(nameString))));
	}
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