using ArchiveNet.Domain;
using ArchiveNet.Repository;

namespace ArchiveNet.Tests.Unit;

public static class ArtRecordTestHelper
{
	public static ArtRecord Create(Artist? artist,// string alsoKnownAsCsv = "J Smith,JS", string artistName = "John Smith",
		DateTime? entryDateTime = null, int rating = 3, string title = "My Song", string uri = "http://mysong.com")
	{
		entryDateTime = entryDateTime == null ? new DateTime(2022, 5, 15, 13, 14, 17) : entryDateTime;
		artist = artist ?? ArtistTestHelper.Create();
		var art = ArtTestHelper.Create(artist: artist, entryDateTime: entryDateTime, rating: rating, title: title, uri: uri);
		return new ArtRecord(art);
	}
}
