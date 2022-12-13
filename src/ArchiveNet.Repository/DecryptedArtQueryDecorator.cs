using ArchiveNet.Domain;

namespace ArchiveNet.Repository;

public class DecryptedArtQueryDecorator : IArtQuery
{
	private readonly ArtQuery artQuery;
	private readonly Cryptor cryptor;

	public DecryptedArtQueryDecorator(ArtQuery artQuery, Cryptor cryptor)
	{
		this.artQuery = artQuery;
		this.cryptor = cryptor;
	}

	public void Dispose()
	{
		this.artQuery.Dispose();
	}

	public async Task<IEnumerable<Art>> GetAsync()
	{
		return (await this.artQuery.GetAsync())
			.Select(art => new ArtDecryptedDecorator(art, this.cryptor));
	}

	public async Task<IEnumerable<Art>> GetAsync(Name artistName)
	{
		return (await this.artQuery.GetAsync(new NameEncryptedDecorator(artistName, this.cryptor)))
			.Select(art => new ArtDecryptedDecorator(art, this.cryptor));
	}
}