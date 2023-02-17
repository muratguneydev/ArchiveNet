using ArchiveNet.Domain;

namespace ArchiveNet.Repository;

public class EncryptedArtCommandDecorator : IArtCommand
{
	private readonly ArtCommand artCommand;
	private readonly Cryptor cryptor;

	public EncryptedArtCommandDecorator(ArtCommand artCommand, Cryptor cryptor)
	{
		this.artCommand = artCommand;
		this.cryptor = cryptor;
	}

	public Task Delete(Art art)
	{
		return this.artCommand.Delete(new ArtEncryptedDecorator(art, this.cryptor));
	}

	public void Dispose()
	{
		this.artCommand.Dispose();
	}

	public Task Insert(Art art)
	{
		return this.artCommand.Insert(new ArtEncryptedDecorator(art, this.cryptor));
	}

	// public Task Insert(IEnumerable<Art> arts)
	// {
	// 	return this.artCommand.Insert(
	// 		arts.Select(art => new ArtEncryptedDecorator(art, this.cryptor))
	// 	);
	// }

	public Task Update(Art art)
	{
		return this.artCommand.Update(new ArtEncryptedDecorator(art, this.cryptor));
	}
}
