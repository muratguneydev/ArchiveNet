using System.Diagnostics.CodeAnalysis;
using ArchiveNet.Web.Api.Dtos;

namespace ArchiveNet.Tests.Comparers;

public class NameCollectionDtoComparer : IEqualityComparer<NameCollectionDto>
{
	public bool Equals(NameCollectionDto? x, NameCollectionDto? y)
	{
		return x!.OrderBy(a => a.Value).SequenceEqual(y!.OrderBy(a => a.Value), new NameDtoComparer());
	}

	public int GetHashCode([DisallowNull] NameCollectionDto obj)
	{
		return obj.Sum(name => name.GetHashCode()*13);
	}
}