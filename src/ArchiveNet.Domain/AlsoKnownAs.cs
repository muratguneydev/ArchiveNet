using System.Collections;

namespace ArchiveNet.Domain;

public class AlsoKnownAs : IEnumerable<Name>
{
	private readonly IEnumerable<Name> names;

	public AlsoKnownAs(IEnumerable<Name> names)
	{
		this.names = names;
	}
	public IEnumerator<Name> GetEnumerator()
	{
		return this.names.GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return this.names.GetEnumerator();
	}
}

public class EmptyAlsoKnownAs : AlsoKnownAs
{
	private static EmptyAlsoKnownAs emptyAlsoKnownAs = new EmptyAlsoKnownAs();
	private EmptyAlsoKnownAs() : base(new Name[] {})
	{
	}

	public static EmptyAlsoKnownAs AlsoKnownAs => emptyAlsoKnownAs;
}