public class ImageUrl
	{
		public ImageUrl(string url)
		{
			this.Url = url;
		}

		public string Url { get; }

		public bool IsEmpty => this is EmptyImageUrl;
	}

	public class EmptyImageUrl : ImageUrl
	{
		private static EmptyImageUrl emptyImageUrl = new EmptyImageUrl();
		private EmptyImageUrl()
			:base(string.Empty)
		{
			
		}

		public static EmptyImageUrl ImageUrl => emptyImageUrl;
	}