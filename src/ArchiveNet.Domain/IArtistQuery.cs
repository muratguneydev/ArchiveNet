namespace ArchiveNet.Domain;

public interface IArtistQuery : IDisposable
{
	Task<Artist> GetAsync(int artistId);
	Task<IEnumerable<Artist>> GetAsync();
}
