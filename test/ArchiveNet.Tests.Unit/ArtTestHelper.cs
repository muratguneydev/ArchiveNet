using ArchiveNet.Domain;

namespace ArchiveNet.Tests.Unit;

public static class ArtTestHelper
{
	public static Art Create(Artist? artist = null,
		DateTime? entryDateTime = null, int rating = 3, string title = "My Song", string uri = "http://mysong.com")
	{
		artist = artist ?? ArtistTestHelper.Create();
		entryDateTime = entryDateTime == null ? new DateTime(2022, 5, 15, 13, 14, 17) : entryDateTime;
		return new Art(
			artist,
			title,
			rating,
			entryDateTime.Value,
			uri
		);
	}
}
