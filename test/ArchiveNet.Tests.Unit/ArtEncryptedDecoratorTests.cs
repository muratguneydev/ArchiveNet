using ArchiveNet.Repository;

namespace ArchiveNet.Tests.Unit;

public class ArtEncryptedDecoratorTests
{
	[Test]
	public void EncryptedCorrectlyTest()
	{
		var art = ArtTestHelper.Create(
			artist: ArtistTestHelper.Create(name: "John Doe", alsoKnownAsCsv: "JD,J Doe"),
			entryDateTime: new DateTime(2022, 5, 15),
			rating: 5,
			title: "My Song",
			uri: "http://mysong.com"
		);

		var expectedEncryptedArt = ArtEncryptedDecoratorTestHelper.Create(
			artist: ArtistTestHelper.Create(name: "Kpio!Epf", alsoKnownAsCsv: "KE,K!Epf"),
			entryDateTime: new DateTime(2022, 5, 15),
			rating: 5,
			title: "Nz!Tpoh",
			uri: "iuuq;00nztpoh/dpn"
		);
		//Act
		var encryptedArt = new ArtEncryptedDecorator(art, new Cryptor());
		//Assert
		Assert.IsTrue(new ArtComparer().Equals(expectedEncryptedArt, encryptedArt));
	}
}

public class ArtDecryptedDecoratorTests
{
	[Test]
	public void DecryptedCorrectlyTest()
	{
		var encryptedArt = ArtEncryptedDecoratorTestHelper.Create(
			artist: ArtistTestHelper.Create(name: "Kpio!Epf", alsoKnownAsCsv: "KE,K!Epf"),
			entryDateTime: new DateTime(2022, 5, 15),
			rating: 5,
			title: "Nz!Tpoh",
			uri: "iuuq;00nztpoh/dpn"
		);

		var expectedDecryptedArt = ArtTestHelper.Create(
			artist: ArtistTestHelper.Create(name: "John Doe", alsoKnownAsCsv: "JD,J Doe"),
			entryDateTime: new DateTime(2022, 5, 15),
			rating: 5,
			title: "My Song",
			uri: "http://mysong.com"
		);
		//Act
		var decryptedArt = new ArtDecryptedDecorator(encryptedArt, new Cryptor());
		//Assert
		Assert.IsTrue(new ArtComparer().Equals(expectedDecryptedArt, decryptedArt));
	}
}
