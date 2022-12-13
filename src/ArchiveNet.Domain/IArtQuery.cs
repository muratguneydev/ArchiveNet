namespace ArchiveNet.Domain;

public interface IArtQuery : IDisposable
{
	Task<IEnumerable<Art>> GetAsync();
	Task<IEnumerable<Art>> GetAsync(Name artistName);
}
