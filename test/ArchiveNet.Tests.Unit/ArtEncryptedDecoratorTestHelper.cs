using ArchiveNet.Repository;

namespace ArchiveNet.Tests.Unit;

public static class ArtEncryptedDecoratorTestHelper
{
	public static ArtEncryptedDecorator Create(string alsoKnownAsCsv = "KE-K!Epf", string artistName = "Kpio!Epf",
		DateTime? entryDateTime = null, int rating = 3, string title = "Nz!Tpoh", string uri = "iuuq;00nztpoh/dpn")
	{
		entryDateTime = entryDateTime == null ? new DateTime(2022, 5, 15, 13, 14, 17) : entryDateTime;
		return new ArtEncryptedDecorator(
			ArtTestHelper.Create(alsoKnownAsCsv, artistName, entryDateTime, rating, title, uri),
			new DummyCryptor()
		);
		
	}
}
