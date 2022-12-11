using ArchiveNet.Domain;

namespace ArchiveNet.Repository;

public record ArtEncryptedDecorator : Art
{
	public ArtEncryptedDecorator(Art original, Cryptor cryptor)
		: base(
			new ArtistEncryptedDecorator(original.Artist, cryptor),
			cryptor.Encrypt(original.Title),
			original.Rating,
			original.EntryDateTime,
			cryptor.Encrypt(original.Uri))
	{
		
	}
}
