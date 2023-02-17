using System.Diagnostics.CodeAnalysis;
using ArchiveNet.Web.Api.Dtos;

namespace ArchiveNet.Tests.Comparers;

public class ArtDtoComparer : IEqualityComparer<ArtDto>
{
	public bool Equals(ArtDto? x, ArtDto? y)
	{
		return x!.Title == y!.Title
			&& new EntryDateTimeComparer().Equals(x.EntryDateTime, y.EntryDateTime)
			&& x.Rating == y.Rating
			&& x.Uri == y.Uri
			&& new ArtistDtoComparer().Equals(x.Artist, y.Artist);
	}

	public int GetHashCode([DisallowNull] ArtDto obj)
	{
		return obj.Title.GetHashCode() + obj.EntryDateTime.GetHashCode() * 13 + obj.Rating.GetHashCode() * 17
			+ obj.Uri.GetHashCode() * 19 + obj.Artist.GetHashCode() + new ArtistDtoComparer().GetHashCode(obj.Artist) * 23;
	}
}
