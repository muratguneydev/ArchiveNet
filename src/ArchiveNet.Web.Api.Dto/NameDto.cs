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

	public string Value { get; set; } = string.Empty;

	public override string ToString() => this.Value;
}
