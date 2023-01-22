using System.Collections;
using ArchiveNet.Domain;

namespace ArchiveNet.Web.Api.Dtos;

public record NameDto
{
	public NameDto()
	{
		
	}

	public NameDto(string value)
	{
		this.Value = value;
	}

	public NameDto(Name name)
		: this(name.Value)
	{
		
	}

	public string Value { get; set; } = string.Empty;

	public override string ToString() => this.Value;

	public Name ToName() => new Name(this.Value);
}

public class NameCollectionDto : List<NameDto>
{
	// private readonly IEnumerable<NameDto> names;
	
	// public NameCollectionDto()
	// 	: this(new NameDto[] {})
	// {
		
	// }

	// public NameCollectionDto(IEnumerable<NameDto> names)
	// {
	// 	this.names = names;
	// }

	// public IEnumerator<NameDto> GetEnumerator()
	// {
	// 	return this.names.GetEnumerator();
	// }

	// IEnumerator IEnumerable.GetEnumerator()
	// {
	// 	return this.names.GetEnumerator();
	// }

	public NameCollection ToNameCollection() => new NameCollection(this.Select(aName => aName.ToName()));
}