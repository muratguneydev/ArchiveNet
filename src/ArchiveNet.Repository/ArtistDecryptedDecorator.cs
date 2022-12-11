using ArchiveNet.Domain;

namespace ArchiveNet.Repository;

public record ArtistDecryptedDecorator : Artist
{
	public ArtistDecryptedDecorator(Artist artist, Cryptor cryptor)
		: base(new NameDecryptedDecorator(artist.Name, cryptor),
				new NameCollection(artist.AlsoKnownAs.Select(aka => new NameDecryptedDecorator(aka, cryptor))))
	{
	}
}
