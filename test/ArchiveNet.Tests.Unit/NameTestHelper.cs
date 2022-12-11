using ArchiveNet.Domain;

namespace ArchiveNet.Tests.Unit;

public static class NameTestHelper
{
	public static Name Create(string name = "John Doe")
	{
		return new Name(name);
	}

	public static NameCollection CreateCollection(IEnumerable<Name>? names = null)
	{
		names = names ?? new NameCollection(new[] { Create() });
		return new NameCollection(names);
	}
}