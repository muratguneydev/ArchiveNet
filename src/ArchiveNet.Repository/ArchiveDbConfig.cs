namespace ArchiveNet.Repository;

public record ArchiveDbConfig
{
	public ArchiveDbConfig(string serviceUrl, string region)
	{
		if (string.IsNullOrWhiteSpace(serviceUrl))
		{
			throw new ArgumentException($"'{nameof(serviceUrl)}' cannot be null or whitespace.", nameof(serviceUrl));
		}

		if (string.IsNullOrWhiteSpace(region))
		{
			throw new ArgumentException($"'{nameof(region)}' cannot be null or whitespace.", nameof(region));
		}
		ServiceUrl = serviceUrl;
		Region = region;
	}

	public string ServiceUrl { get; }
	public string Region { get; }
}

