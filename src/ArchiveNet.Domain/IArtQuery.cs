namespace ArchiveNet.Domain;

public interface IArtQuery : IDisposable
{
	Task<IEnumerable<Art>> Get();
}
