using ArchiveNet.Repository;

namespace ArchiveNet.Tests.Unit;

public static class ArtRecordTestHelper
{
	public static ArtRecord Create(string alsoKnownAsCsv = "J Smith,JS", string artistName = "John Smith",
		DateTime? entryDateTime = null, int rating = 3, string title = "My Song", string uri = "http://mysong.com")
	{
		entryDateTime = entryDateTime == null ? new DateTime(2022, 5, 15, 13, 14, 17) : entryDateTime;
		return new ArtRecord()
		{
			AlsoKnownAsCsv = alsoKnownAsCsv,
			ArtistName = artistName,
			EntryDateTime = entryDateTime.Value,
			Rating = rating,
			Title = title,
			Uri = uri
		};
	}
}
