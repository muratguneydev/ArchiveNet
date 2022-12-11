namespace ArchiveNet.Repository;

public class Cryptor
{
	private const int CryptSeed = 1;

	public virtual string Encrypt(string rawText)
	{
		return Crypt(rawText, CryptSeed);
	}

	public virtual string Decrypt(string encryptedText)
	{
		return Crypt(encryptedText, -CryptSeed);
	}

	private static string Crypt(string text, int cryptSeed)
	{
		if (text == null)
			return string.Empty;
		var result = new char[text.Length];
		for (var i = 0; i < text.Length; i++)
		{
			result[i] = (char)((byte)text[i] + cryptSeed);
		}
		return new string(result);
	}
}