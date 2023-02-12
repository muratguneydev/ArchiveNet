using System.Diagnostics.CodeAnalysis;
using ArchiveNet.Web.Api.Dtos;

namespace ArchiveNet.Tests.Comparers;

public class NameDtoComparer : IEqualityComparer<NameDto>
{
	public bool Equals(NameDto? x, NameDto? y)
	{
		return x!.Value == y!.Value;
	}

	public int GetHashCode([DisallowNull] NameDto obj)
	{
		return obj.GetHashCode();
	}
}
