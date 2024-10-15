using DoctorManagment.Repositories.Data.DataSeeders;
using Microsoft.EntityFrameworkCore;
using Repositories.Entities;

namespace Repositories.Context
{
    public class MyContext : DbContext
    {
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<DoctorAvailability> DoctorAvailabilities { get; set; }
        public DbSet<TimeRange> TimeRanges { get; set; }
        public DbSet<Day> Days { get; set; }

        private readonly IConfiguration _configuration;
        private readonly IEntityConfiguration _entityConfiguration;
        public MyContext(DbContextOptions<MyContext> options, IConfiguration configuration, IEntityConfiguration entityConfiguration)
            : base(options)
        {
            _configuration = configuration;
            _entityConfiguration = entityConfiguration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            var connectionString = _configuration.GetConnectionString("DoctorAvailabilityDb");
            optionsBuilder.UseMySQL(connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            _entityConfiguration.ConfigureAllEntities(modelBuilder);
            DataSeeder.DataSeedingToDatabase(modelBuilder);
        }
    }
}
