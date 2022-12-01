namespace ArchiveNet.Domain;

public record Artist(Name Name, IEnumerable<Name> AlsoKnownAs);