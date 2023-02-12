namespace ArchiveNet.Tests;

public static class TestSetUp
{
	public static void SetDatabaseUrl()
	{
		Environment.SetEnvironmentVariable("ARCHIVENET_API_DatabaseSettings__URL", TestEnvironment.DatabaseUrl);
	}
}
