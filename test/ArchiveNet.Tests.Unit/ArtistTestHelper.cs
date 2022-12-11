using ArchiveNet.Domain;

namespace ArchiveNet.Tests.Unit;

public static class ArtistTestHelper
{
	public static Artist Create(string artistName = "John Doe", string alsoKnownAsCsv = "J Smith,JS")
	{
		return new Artist(NameTestHelper.Create(artistName),
			NameTestHelper.CreateCollection(alsoKnownAsCsv.Split(',').Select(name => new Name(name))));
	}
}
