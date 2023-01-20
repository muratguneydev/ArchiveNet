using ArchiveNet.Domain;

namespace ArchiveNet.Repository;

public class DecryptedArtistQueryDecorator : IArtistQuery
{
	private readonly IArtistQuery artistQuery;
	private readonly Cryptor cryptor;

	public DecryptedArtistQueryDecorator(IArtistQuery artistQuery, Cryptor cryptor)
	{
		this.artistQuery = artistQuery;
		this.cryptor = cryptor;
	}

	public void Dispose()
	{
		this.artistQuery.Dispose();
	}

	public async Task<IEnumerable<Artist>> GetAsync()
	{
		return (await this.artistQuery.GetAsync())
			.Select(artist => new ArtistDecryptedDecorator(artist, this.cryptor));
	}

	public async Task<Artist> GetAsync(int artistId)
	{
		var artist = await this.artistQuery.GetAsync(artistId);
		return new ArtistDecryptedDecorator(artist, this.cryptor);
	}
}