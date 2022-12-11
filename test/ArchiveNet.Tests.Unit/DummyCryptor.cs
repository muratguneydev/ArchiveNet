using ArchiveNet.Repository;

namespace ArchiveNet.Tests.Unit;

public class DummyCryptor : Cryptor
{
	public override string Encrypt(string rawText)
	{
		return rawText;
	}

	public override string Decrypt(string encryptedText)
	{
		return encryptedText;
	}
}