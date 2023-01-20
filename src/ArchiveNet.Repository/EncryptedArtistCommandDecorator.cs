using ArchiveNet.Domain;

namespace ArchiveNet.Repository;

public class EncryptedArtistCommandDecorator : IArtistCommand
{
	private readonly ArtistCommand artistCommand;
	private readonly Cryptor cryptor;

	public EncryptedArtistCommandDecorator(ArtistCommand artistCommand, Cryptor cryptor)
	{
		this.artistCommand = artistCommand;
		this.cryptor = cryptor;
	}

	public void Dispose()
	{
		this.artistCommand.Dispose();
	}

	public Task Insert(Artist artist)
	{
		return this.artistCommand.Insert(new ArtistEncryptedDecorator(artist, this.cryptor));
	}

	public Task Update(Artist artist)
	{
		return this.artistCommand.Update(new ArtistEncryptedDecorator(artist, this.cryptor));
	}
}