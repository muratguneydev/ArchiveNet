using ArchiveNet.Domain;

namespace ArchiveNet.Web.Api.Dtos;

public record ArtistDto(int Id, NameDto Name, NameCollectionDto AlsoKnownAs)
{
	public ArtistDto()
		: this(default, new NameDto(""), new NameCollectionDto() )
	{
		
	}
	public ArtistDto(NameDto name)
		: this(default, name, new NameCollectionDto())
	{
		
	}

	public ArtistDto(int id, NameDto name)
		: this(id, name, new NameCollectionDto())
	{
		
	}

	public ArtistDto(int id)
		: this(id, new NameDto(string.Empty), new NameCollectionDto())
	{
		
	}

	public ArtistDto(Artist artist)
		: this(artist.Id, new NameDto(artist.Name))
	{
		
	}

	public Artist ToArtist() => new Artist(this.Id, this.Name.ToName(), this.AlsoKnownAs.ToNameCollection());
}
