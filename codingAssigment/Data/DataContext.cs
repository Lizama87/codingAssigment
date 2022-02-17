using Microsoft.EntityFrameworkCore;


namespace codingAssigment.Data
{
	public class DataContext : DbContext
	{
		protected readonly IConfiguration Configuration;

		public DataContext(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		protected override void OnConfiguring(DbContextOptionsBuilder options)
		{
			options.UseSqlite(Configuration.GetConnectionString("DefaultConnection"));
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Crew>()
				.HasMany(c => c.Employees)
				.WithOne(e => e.Crew)
				.HasForeignKey(a => a.CrewId);
		}

		public DbSet<Employee> Employees { get; set; }
		public DbSet<Crew> Crews { get; set; }
	}
}
