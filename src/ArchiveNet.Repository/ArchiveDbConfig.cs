namespace ArchiveNet.Repository;

public record ArchiveDbConfig(string ServiceUrl, string Region);

// public record EmptyArchiveDbConfig : ArchiveDbConfig
// {
// 	private static EmptyArchiveDbConfig archiveDbConfig = new EmptyArchiveDbConfig();
// 	private EmptyArchiveDbConfig() : base(string.Empty, string.Empty)
// 	{
// 	}

// 	public static EmptyArchiveDbConfig ArchiveDbConfig => archiveDbConfig;
// }