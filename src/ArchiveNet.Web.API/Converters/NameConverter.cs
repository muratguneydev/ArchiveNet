using ArchiveNet.Domain;
using ArchiveNet.Web.Api.Dtos;

namespace ArchiveNet.Web.Api.Converters;

public static class NameConverter
{
	public static Name ToName(this NameDto nameDto) => new Name(nameDto.Value);
	public static NameDto ToNameDto(this Name name) => new NameDto(name.Value);
}
