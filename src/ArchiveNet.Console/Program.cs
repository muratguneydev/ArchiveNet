// See https://aka.ms/new-console-template for more information
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using ArchiveNet.Repository;

Console.WriteLine("Hello, World!");

var config = new AmazonDynamoDBConfig()
{
	ServiceURL = "http://192.168.5.166:8000",
	AuthenticationRegion = "eu-west-2"
};
var amazonDynamoDbClient = new AmazonDynamoDBClient(config);
var amazonDynamoDBContext = new DynamoDBContext(amazonDynamoDbClient);

// var artCommand = new ArtCommand(amazonDynamoDBContext);
// await artCommand.Insert(new ArtRecord(new Art(new Artist(new Name("Murat Guney"), new Name[] {}), "My best friend", 2)));

var artQuery = new ArtQuery(amazonDynamoDBContext);
var data = await artQuery.Get();
Console.Read();