namespace ArchiveNet.Domain;

public record Artist(int Id, Name Name, NameCollection AlsoKnownAs)
{
	public Artist()
		: this(default, new Name(""), new NameCollection() )
	{
		
	}
	public Artist(Name name)
		: this(default, name, new NameCollection())
	{
		
	}

	public Artist(int id, Name name)
		: this(id, name, new NameCollection())
	{
		
	}

	public Artist(int id)
		: this(id, new Name(string.Empty), new NameCollection())
	{
		
	}
}
