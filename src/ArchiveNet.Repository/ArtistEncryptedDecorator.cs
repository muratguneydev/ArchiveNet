using ArchiveNet.Domain;

namespace ArchiveNet.Repository;

public record ArtistEncryptedDecorator : Artist
{
	public ArtistEncryptedDecorator(Artist artist, Cryptor cryptor)
		: base(new NameEncryptedDecorator(artist.Name, cryptor),
			new NameCollection(artist.AlsoKnownAs.Select(aka => new NameEncryptedDecorator(aka, cryptor))))
	{
	}
}
