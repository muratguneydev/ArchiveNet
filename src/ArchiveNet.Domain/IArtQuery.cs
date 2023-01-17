namespace ArchiveNet.Domain;

public interface IArtQuery : IDisposable
{
	//Task<IEnumerable<Art>> GetAsync();
	Task<IEnumerable<Art>> GetAsync(int artistId);
	Task<IEnumerable<Art>> GetByDateOffsetAsync(int lastNumberOfDays);
}
