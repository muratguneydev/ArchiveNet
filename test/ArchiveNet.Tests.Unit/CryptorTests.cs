using ArchiveNet.Repository;

namespace ArchiveNet.Tests.Unit;

public class CryptorTests
{
    [TestCase("abcd123e", "bcde234f")]
    [TestCase("ABcd123e", "BCde234f")]
    [TestCase("Red Hot Chilli Peppers - Under The Bridge", "Sfe!Ipu!Dijmmj!Qfqqfst!.!Voefs!Uif!Csjehf")]
    public void EncryptCorrectly(string rawText, string encryptedText)
    {
		var cryptor = new Cryptor();
        Assert.AreEqual(encryptedText, cryptor.Encrypt(rawText));
    }

	[TestCase("bcde234f", "abcd123e")]
    [TestCase("BCde234f", "ABcd123e")]
    [TestCase("Sfe!Ipu!Dijmmj!Qfqqfst!.!Voefs!Uif!Csjehf", "Red Hot Chilli Peppers - Under The Bridge")]
    public void DecryptCorrectly(string encryptedText, string decryptedText)
    {
		var cryptor = new Cryptor();
        Assert.AreEqual(decryptedText, cryptor.Decrypt(encryptedText));
    }
}
