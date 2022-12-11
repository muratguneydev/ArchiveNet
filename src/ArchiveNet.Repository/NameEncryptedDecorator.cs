using ArchiveNet.Domain;

namespace ArchiveNet.Repository;

public record NameEncryptedDecorator : Name
{
	public NameEncryptedDecorator(Name original, Cryptor cryptor)
		: base(cryptor.Encrypt(original.Value))
	{
	}
}
