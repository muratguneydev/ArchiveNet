using ArchiveNet.Domain;
using ArchiveNet.Web.Api.Dtos;
using ArchiveNet.Web.Api.Converters;

namespace ArchiveNet.Web.Api.Converters;

public static class ArtConverter
{
	public static Art ToArt(this ArtDto artDto) => new Art(artDto.Artist.ToArtist(), artDto.Title, artDto.Rating, artDto.EntryDateTime, artDto.Uri);

	public static ArtDto ToArtDto(this Art art) =>new ArtDto(art.Artist.ToArtistDto(), art.Title, art.Rating, art.EntryDateTime, art.Uri);
}
