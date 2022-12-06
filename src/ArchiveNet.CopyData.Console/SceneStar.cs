public class SceneStar
	{
		public SceneStar()
		{
			
		}
		public SceneStar(Scene scene, Star star)
		{
			if (scene is null)
			{
				throw new ArgumentNullException(nameof(scene));
			}

			if (star is null)
			{
				throw new ArgumentNullException(nameof(star));
			}
			
			this.SceneId = scene.Id;
			this.StarId = star.Id;
			this.Scene = scene;
			this.Star = star;
		}

		public long SceneId { get; set; }

		public long StarId { get; set; }

		public Scene Scene { get; set; } = new Scene();
		public Star Star { get; set; } = new Star();
	}