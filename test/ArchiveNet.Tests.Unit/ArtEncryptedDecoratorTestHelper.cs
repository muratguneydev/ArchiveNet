using ArchiveNet.Domain;
using ArchiveNet.Repository;

namespace ArchiveNet.Tests.Unit;

public static class ArtEncryptedDecoratorTestHelper
{
	public static ArtEncryptedDecorator Create(Artist artist,
		DateTime? entryDateTime = null, int rating = 3, string title = "Nz!Tpoh", string uri = "iuuq;00nztpoh/dpn")
	{
		artist = artist ?? ArtistTestHelper.Create(name: "Kpio!Epf", alsoKnownAsCsv: "KE-K!Epf");
		entryDateTime = entryDateTime == null ? new DateTime(2022, 5, 15, 13, 14, 17) : entryDateTime;
		return new ArtEncryptedDecorator(
			ArtTestHelper.Create(artist, entryDateTime, rating, title, uri),
			new DummyCryptor()
		);
		
	}
}
