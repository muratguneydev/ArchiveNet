using Microsoft.EntityFrameworkCore;

public class ArchiveDbContext : DbContext
{
	private readonly string connectionString;

	// public ArchiveDbContext(DbContextOptions<ArchiveDbContext> options)
	// 	: base(options)
	// {
	// 	//Database.EnsureCreated();
	// }

	public ArchiveDbContext(string connectionString)
		: base()
	{
		this.connectionString = connectionString;
		Database.EnsureCreated();
		// this.Stars = Set<Star>();
		// this.Scenes = Set<Scene>();
		// this.SceneStars = Set<SceneStar>();
	}

	public DbSet<Star>? Stars { get; set; }// = Set<Star>();
	public DbSet<Scene>? Scenes { get; set; }// = Set<Scene>();
	public DbSet<SceneStar>? SceneStars { get; set; }// = Set<SceneStar>();

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlite(this.connectionString);
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Star>()
		    .HasIndex(s => s.Name)
			.IsUnique();

		modelBuilder.Entity<Scene>()
		    .HasIndex(s => s.Url)
			.IsUnique()
			.HasDatabaseName("UK_Scene_Url");
			//.HasName("UK_Scene_Url");

		// modelBuilder.Entity<SceneStar>()
		// 	.Property(ss => ss.Id)
		// 	.ValueGeneratedOnAdd();

		modelBuilder.Entity<SceneStar>()
		    .HasIndex(ss => new { ss.SceneId, ss.StarId })
			.IsUnique();

		modelBuilder.Entity<SceneStar>()
			.HasKey(ss => new { ss.SceneId, ss.StarId });

		modelBuilder.Entity<SceneStar>()
			.HasOne(ss => ss.Scene)
			.WithMany(s => s.SceneStars)
			.HasForeignKey(ss => ss.SceneId);

		modelBuilder.Entity<SceneStar>()
			.HasOne(ss => ss.Star)
			.WithMany(s => s.SceneStars)
			.HasForeignKey(ss => ss.StarId);

		// modelBuilder.Entity<SaveImageTask>()
		//     .HasIndex(s => s.ImageUrl)
		// 	.IsUnique()
		// 	.HasName("PK_SaveImageTasks_ImageUrl");
	}
}
