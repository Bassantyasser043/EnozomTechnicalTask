using DoctorAvailabiltity.Repository.DataSeeders;
using DoctorAvailabiltity.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace DoctorAvailabiltity.Repository.Context
{
    public class MyContext: DbContext
    {
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<DoctorAvailability> DoctorAvailabilities { get; set; }
        public DbSet<TimeRange> TimeRanges { get; set; }
        public DbSet<Day> Days{ get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            string ConnectionStringTask = "server=localhost;database=DoctorAvailability;user=TechnicalTask;password=testTechnical1234";
            var connectionString = optionsBuilder.UseMySQL(ConnectionStringTask);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Doctor>()
                .HasKey(p => p.DoctorId); // Set ProductId as primary key

            modelBuilder.Entity<Doctor>()
                .Property(p => p.DoctorId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<DoctorAvailability>()
                .HasKey(p => p.DoctorAvailabilityId); // Set ProductId as primary key

            modelBuilder.Entity<DoctorAvailability>()
                .Property(p => p.DoctorAvailabilityId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<TimeRange>()
               .HasKey(p => p.TimeRangeId); // Set ProductId as primary key

            modelBuilder.Entity<TimeRange>()
                .Property(p => p.TimeRangeId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Day>()
               .HasKey(p => p.DayId); // Set ProductId as primary key

            modelBuilder.Entity<Day>()
                .Property(p => p.DayId)
                .ValueGeneratedOnAdd();

            DaySeeder.Seed(modelBuilder);
            TimeRangeSeeder.Seed(modelBuilder);
            DoctorSeeder.Seed(modelBuilder);
            TimeAvailabilitySeeder.Seed(modelBuilder);



        }
    }
}
