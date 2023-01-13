namespace ArchiveNet.Domain;

public interface IArtQuery : IDisposable
{
	Task<IEnumerable<Art>> GetAsync();
	Task<IEnumerable<Art>> GetAsync(Name artistName);
	Task<IEnumerable<Art>> GetAsync(long lastNumberOfDays);
}
