using ArchiveNet.Domain;

namespace ArchiveNet.Repository;

public record ArtDecryptedDecorator : Art
{
	public ArtDecryptedDecorator(Art original, Cryptor cryptor)
		: base(
			new ArtistDecryptedDecorator(original.Artist, cryptor),
			cryptor.Decrypt(original.Title),
			original.Rating,
			original.EntryDateTime,
			cryptor.Decrypt(original.Uri))
	{
		
	}
}
