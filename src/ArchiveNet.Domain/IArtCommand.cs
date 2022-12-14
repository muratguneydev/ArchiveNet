namespace ArchiveNet.Domain;

public interface IArtCommand : IDisposable
{
	Task Insert(Art art);
	Task Insert(IEnumerable<Art> arts);
}