using System.Diagnostics.CodeAnalysis;
using ArchiveNet.Web.Api.Dtos;

namespace ArchiveNet.Tests.Comparers;

public class ArtistDtoComparer : IEqualityComparer<ArtistDto>
{
	public bool Equals(ArtistDto? x, ArtistDto? y)
	{
		return x!.Name == y!.Name
			&& new NameCollectionDtoComparer().Equals(x.AlsoKnownAs, y.AlsoKnownAs);
	}

	public int GetHashCode([DisallowNull] ArtistDto obj)
	{
		return obj.GetHashCode();
	}
}
