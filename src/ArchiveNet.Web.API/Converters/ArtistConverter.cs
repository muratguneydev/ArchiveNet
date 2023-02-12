using ArchiveNet.Domain;
using ArchiveNet.Web.Api.Dtos;

namespace ArchiveNet.Web.Api.Converters;

public static class ArtistConverter
{
	public static Artist ToArtist(this ArtistDto artistDto) => new Artist(artistDto.Id, artistDto.Name.ToName(), artistDto.AlsoKnownAs.ToNameCollection());
	
	public static ArtistDto ToArtistDto(this Artist artist) => new ArtistDto(artist.Id, artist.Name.ToNameDto());
}
