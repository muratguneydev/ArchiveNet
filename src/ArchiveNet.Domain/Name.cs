namespace ArchiveNet.Domain;

public record Name
{
	private readonly string name;

	public Name(string name)
	{
		this.name = name;
	}

	public override string ToString() => this.name;
}
