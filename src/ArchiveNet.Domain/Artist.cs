namespace ArchiveNet.Domain;

public record Artist(Name Name, NameCollection AlsoKnownAs)
{
	public Artist()
		: this(new Name(""), new NameCollection() )
	{
		
	}
	public Artist(Name name)
		: this(name, new NameCollection())
	{
		
	}
}