namespace ArchiveNet.Tests;

public static class TestExtensions
{
	public static DateTime TruncateMilliseconds(this DateTime dateTime)
	{
		return dateTime.Truncate(TimeSpan.FromMilliseconds(1));
	}

	public static DateTime Truncate(this DateTime dateTime, TimeSpan timeSpan)
	{
		if (timeSpan == TimeSpan.Zero) return dateTime; // Or could throw an ArgumentException
		if (dateTime == DateTime.MinValue || dateTime == DateTime.MaxValue) return dateTime; // do not modify "guard" values
		return dateTime.AddTicks(-(dateTime.Ticks % timeSpan.Ticks));
	}
}
