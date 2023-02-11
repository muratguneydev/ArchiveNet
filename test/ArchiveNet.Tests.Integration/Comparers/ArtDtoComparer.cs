using System.Diagnostics.CodeAnalysis;
using ArchiveNet.Web.Api.Dtos;

namespace ArchiveNet.Tests.Comparers;

public class ArtDtoComparer : IEqualityComparer<ArtDto>
{
	public bool Equals(ArtDto? x, ArtDto? y)
	{
		return x!.Title == y!.Title
			&& x.EntryDateTime.TruncateMilliseconds() == y.EntryDateTime.TruncateMilliseconds()
			&& x.Rating == y.Rating
			&& x.Uri == y.Uri
			&& new ArtistDtoComparer().Equals(x.Artist, y.Artist);
	}

	public int GetHashCode([DisallowNull] ArtDto obj)
	{
		throw new NotImplementedException();
	}
}
