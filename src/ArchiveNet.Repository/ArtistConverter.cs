using ArchiveNet.Domain;

namespace ArchiveNet.Repository;

public static class ArtistConverter
{
	public static Artist Convert(ArtistRecord artistRecord)
	{
		return new Artist(
			artistRecord.ArtistId,
			new Name(artistRecord.Name),
			new NameCollection(artistRecord.AlsoKnownAsCsv
											.Split(',')
											.Select(nameString => new Name(nameString))));
	}
}
