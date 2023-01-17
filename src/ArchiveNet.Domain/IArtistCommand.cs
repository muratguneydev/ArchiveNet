namespace ArchiveNet.Domain;

public interface IArtistCommand : IDisposable
{
	Task Insert(Artist artist);
}