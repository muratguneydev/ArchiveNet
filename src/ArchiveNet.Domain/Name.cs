using System.Collections;

namespace ArchiveNet.Domain;

public record Name
{
	public Name(string value)
	{
		this.Value = value;
	}

	public string Value { get; }

	public override string ToString() => this.Value;
}

public class NameCollection : List<Name> //: IEnumerable<Name>
{
	//private readonly IEnumerable<Name> names;
	public NameCollection()
		: this(new Name[] {})
	{
		
	}

	//TODO: replace with private member when DTO is created. Don't use domain object as DTO.
	public IEnumerable<Name> Names { get; }

	public NameCollection(IEnumerable<Name> names)
	{
		this.Names = names;
	}
	// public IEnumerator<Name> GetEnumerator()
	// {
	// 	return this.Names.GetEnumerator();
	// }

	// IEnumerator IEnumerable.GetEnumerator()
	// {
	// 	return this.Names.GetEnumerator();
	// }

	public override bool Equals(object? obj)
	{
		var other = obj as NameCollection;
		if (other == null)
			return false;

		return this.Names.OrderBy(name => name.Value).SequenceEqual(other.OrderBy(name => name.Value));
	}

	public override int GetHashCode()
	{
		return this.Names.Sum(name => name.GetHashCode());
	}
}