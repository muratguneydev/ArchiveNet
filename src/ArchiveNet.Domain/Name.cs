using System.Collections;

namespace ArchiveNet.Domain;

public record Name
{
	public Name(string name)
	{
		this.Value = name;
	}

	public string Value { get; }

	public override string ToString() => this.Value;
}

public class NameCollection : IEnumerable<Name>
{
	private readonly IEnumerable<Name> names;
	public NameCollection()
		: this(new Name[] {})
	{
		
	}

	public NameCollection(IEnumerable<Name> names)
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