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

	// public async Task<IEnumerable<Art>> GetAsync()
	// {
	// 	return (await this.artQuery.GetAsync())
	// 		.Select(art => new ArtDecryptedDecorator(art, this.cryptor));
	// }

	public async Task<IEnumerable<Art>> GetAsync(int artistId)
	{
		return (await this.artQuery.GetAsync(artistId))
			.Select(art => new ArtDecryptedDecorator(art, this.cryptor));
	}

	public async Task<IEnumerable<Art>> GetByDateOffsetAsync(int lastNumberOfDays)
	{
		return (await this.artQuery.GetByDateOffsetAsync(lastNumberOfDays))
			.Select(art => new ArtDecryptedDecorator(art, this.cryptor));
	}
}