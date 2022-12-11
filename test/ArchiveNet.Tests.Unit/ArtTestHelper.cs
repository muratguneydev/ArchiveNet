using ArchiveNet.Domain;

namespace ArchiveNet.Tests.Unit;

public static class ArtTestHelper
{
	public static Art Create(string alsoKnownAsCsv = "J Smith,JS", string artistName = "John Smith",
		DateTime? entryDateTime = null, int rating = 3, string title = "My Song", string uri = "http://mysong.com")
	{
		entryDateTime = entryDateTime == null ? new DateTime(2022, 5, 15, 13, 14, 17) : entryDateTime;
		return new Art(
			ArtistTestHelper.Create(artistName, alsoKnownAsCsv),
			title,
			rating,
			entryDateTime.Value,
			uri
		);
	}
}
