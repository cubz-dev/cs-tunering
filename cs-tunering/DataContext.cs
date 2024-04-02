global using Microsoft.EntityFrameworkCore;

namespace cs_tunering
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.Development.json")
                .Build();

            string connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tournament>()
                .Property(x => x.IsParentTournament)
                .HasDefaultValue(1);
        }

        public DbSet<Tournament> Tournaments => Set<Tournament>();
        public DbSet<Player> Players => Set<Player>();
        public DbSet<ChildTournament> ChildTournaments => Set<ChildTournament>();

    }
}
