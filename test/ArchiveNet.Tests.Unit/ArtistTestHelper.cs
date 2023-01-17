using ArchiveNet.Domain;

namespace ArchiveNet.Tests.Unit;

public static class ArtistTestHelper
{
	public static Artist Create(int id = default, string name = "John Doe", string alsoKnownAsCsv = "J Smith,JS")
	{
		return new Artist(id, NameTestHelper.Create(name),
			NameTestHelper.CreateCollection(alsoKnownAsCsv.Split(',').Select(name => new Name(name))));
	}
}
