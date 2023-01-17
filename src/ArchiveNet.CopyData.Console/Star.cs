public class Star
	{
		public long Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string PhotoUrl { get; set; } = string.Empty;
		public DateTime EntryDateTime { get; set; }

		public ICollection<SceneStar> SceneStars { get; set; } = new List<SceneStar>();

		public ImageUrl ImageUrl =>
			(this.PhotoUrl == null || !this.PhotoUrl.StartsWith("http"))
				? EmptyImageUrl.ImageUrl
				: new ImageUrl(this.PhotoUrl);
		
		public decimal Rating =>
			(this.SceneStars == null || this.SceneStars.Count == 0)
				? 0
				: (decimal) (
						(this.SceneStars.Sum(ss => ss.Scene.Rating) / this.SceneStars.Count)
						+ (this.SceneStars.Count * 0.1)
					);

		public override int GetHashCode() => (int)this.Id;
		
		
	}
