using System.Diagnostics.CodeAnalysis;
using ArchiveNet.Domain;

namespace ArchiveNet.Tests.Unit;

public class ArtComparer : IEqualityComparer<Art>
{
	public bool Equals(Art? x, Art? y)
	{
		if (x == null && y == null)
			return true;
		if (x == null || y == null)
			return false;

		return	new ArtistComparer().Equals(x.Artist, y.Artist)
			&& x.EntryDateTime == y.EntryDateTime
			&& x.Rating == y.Rating
			&& x.Title == y.Title
			&& x.Uri == y.Uri;
	}

	public int GetHashCode([DisallowNull] Art obj)
	{
		throw new NotImplementedException();
	}
}

public class ArtistComparer : IEqualityComparer<Artist>
{
	public bool Equals(Artist? x, Artist? y)
	{
		if (x == null && y == null)
			return true;
		if (x == null || y == null)
			return false;

		return new NameCollectionComparer().Equals(x.AlsoKnownAs, y.AlsoKnownAs)
			&& new NameComparer().Equals(x.Name, y.Name);// x.Name == y.Name;
	}

	public int GetHashCode([DisallowNull] Artist obj)
	{
		throw new NotImplementedException();
	}
}

public class NameCollectionComparer : IEqualityComparer<NameCollection>
{
	public bool Equals(NameCollection? x, NameCollection? y)
	{
		return x!
				.OrderBy(name => name.Value)
				.SequenceEqual(y!.OrderBy(name => name.Value), new NameComparer());
	}

	public int GetHashCode([DisallowNull] NameCollection obj)
	{
		return obj.Sum(name => name.GetHashCode() * 13 + 7);
	}
}

public class NameComparer : IEqualityComparer<Name>
{
	public bool Equals(Name? x, Name? y)
	{
		return x!.Value == y!.Value;
	}

	public int GetHashCode([DisallowNull] Name obj)
	{
		return obj.GetHashCode();
	}
}