using DoctorAvailabiltity.Data.DataSeeders;
using DoctorAvailabiltity.DoctorManagment.Entities;
using DoctorAvailabiltity.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace DoctorAvailabiltity.Data.Context
{
    public class MyContext : DbContext
    {
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<DoctorAvailability> DoctorAvailabilities { get; set; }
        public DbSet<TimeRange> TimeRanges { get; set; }
        public DbSet<Day> Days { get; set; }

        private readonly IConfiguration _configuration;
        public MyContext(DbContextOptions<MyContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            var connectionString = _configuration.GetConnectionString("DoctorAvailabilityDb");
            optionsBuilder.UseMySQL(connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var entityConfigurations = new EntityConfigurations();
            entityConfigurations.ConfigureAllEntities(modelBuilder);

            DataSeeder.DataSeedingToDatabase(modelBuilder);
        }
    }
}
