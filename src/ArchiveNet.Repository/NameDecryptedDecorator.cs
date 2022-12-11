using ArchiveNet.Domain;

namespace ArchiveNet.Repository;

public record NameDecryptedDecorator : Name
{
	public NameDecryptedDecorator(Name original, Cryptor cryptor)
		: base(cryptor.Decrypt(original.Value))
	{
	}
}
