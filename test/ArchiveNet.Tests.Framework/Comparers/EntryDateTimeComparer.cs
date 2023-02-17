using System.Diagnostics.CodeAnalysis;

namespace ArchiveNet.Tests.Comparers;

public class EntryDateTimeComparer : IEqualityComparer<DateTime>
{
	public bool Equals(DateTime x, DateTime y)
	{
		return x.Year == y.Year
			&& x.Month == y.Month
			&& x.Day == y.Day
			&& x.Hour == y.Hour
			&& x.Minute == y.Minute
			&& x.Second == y.Second;
	}

	public int GetHashCode([DisallowNull] DateTime obj)
	{
		throw new NotImplementedException();
	}
}