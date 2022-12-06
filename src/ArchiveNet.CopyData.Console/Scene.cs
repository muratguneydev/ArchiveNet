public class Scene
{
	public long Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public string Url { get; set; } = string.Empty;

	public DateTime EntryDateTime { get; set; }

	public int Rating { get; set; }

	public ICollection<SceneStar> SceneStars { get; set; } = new List<SceneStar>();
}