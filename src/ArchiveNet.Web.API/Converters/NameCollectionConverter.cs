using ArchiveNet.Domain;
using ArchiveNet.Web.Api.Dtos;

namespace ArchiveNet.Web.Api.Converters;

public static class NameCollectionConverter
{
	public static NameCollection ToNameCollection(this NameCollectionDto nameCollectionDto) => new NameCollection(nameCollectionDto.Select(aName => aName.ToName()));
}
