aws dynamodb delete-table \
	--table-name ArtWork \
	--endpoint-url http://192.168.5.166:8000

aws dynamodb create-table \
	--table-name ArtWork \
	--attribute-definitions \
		AttributeName=ArtistId,AttributeType=N \
		AttributeName=SK,AttributeType=S \
		AttributeName=Title,AttributeType=S \
		AttributeName=Uri,AttributeType=S \
		AttributeName=Rating,AttributeType=N \
		AttributeName=EntryDateOffsetSince2000,AttributeType=N \
	--key-schema AttributeName=ArtistId,KeyType=HASH AttributeName=SK,KeyType=RANGE \
	--provisioned-throughput ReadCapacityUnits=1,WriteCapacityUnits=1 \
	--endpoint-url http://192.168.5.166:8000 \
	--global-secondary-indexes \
        "[
            {
                \"IndexName\": \"SKIndex\",
                \"KeySchema\": [
					{\"AttributeName\":\"SK\",\"KeyType\":\"HASH\"},
					{\"AttributeName\":\"ArtistId\",\"KeyType\":\"RANGE\"}
				],
                \"Projection\":{
                    \"ProjectionType\":\"ALL\"
                },
                \"ProvisionedThroughput\": {
                    \"ReadCapacityUnits\": 10,
                    \"WriteCapacityUnits\": 5
                }
            },
	    	{
                \"IndexName\": \"TitleIndex\",
                \"KeySchema\": [
					{\"AttributeName\":\"Title\",\"KeyType\":\"HASH\"}
				],
                \"Projection\":{
                    \"ProjectionType\":\"ALL\"
                },
                \"ProvisionedThroughput\": {
                    \"ReadCapacityUnits\": 10,
                    \"WriteCapacityUnits\": 5
                }
            },
	    	{
                \"IndexName\": \"RatingIndex\",
                \"KeySchema\": [
					{\"AttributeName\":\"Rating\",\"KeyType\":\"HASH\"}
				],
                \"Projection\":{
                    \"ProjectionType\":\"ALL\"
                },
                \"ProvisionedThroughput\": {
                    \"ReadCapacityUnits\": 10,
                    \"WriteCapacityUnits\": 5
                }
            },
	    	{
                \"IndexName\": \"EntryDateOffsetIndex\",
                \"KeySchema\": [
					{\"AttributeName\":\"EntryDateOffsetSince2000\",\"KeyType\":\"HASH\"}
				],
                \"Projection\":{
                    \"ProjectionType\":\"ALL\"
                },
                \"ProvisionedThroughput\": {
                    \"ReadCapacityUnits\": 10,
                    \"WriteCapacityUnits\": 5
                }
            }
        ]"
