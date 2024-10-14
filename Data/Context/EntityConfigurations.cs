using DoctorAvailabiltity.DoctorManagment.Entities;
using DoctorAvailabiltity.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace DoctorAvailabiltity.Data.Context
{
    public class EntityConfigurations : IEntityConfigurations
    {
        public void ConfigureAllEntities(ModelBuilder modelBuilder)
        {
            ConfigureDoctor(modelBuilder);
            ConfigureDoctorAvailability(modelBuilder);
            ConfigureTimeRange(modelBuilder);
            ConfigureDay(modelBuilder);
        }

        private void ConfigureDoctor(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Doctor>()
                .HasKey(p => p.DoctorId);
            modelBuilder.Entity<Doctor>()
                .Property(p => p.DoctorId)
                .ValueGeneratedOnAdd();
        }

        private void ConfigureDoctorAvailability(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DoctorAvailability>()
                .HasKey(p => p.DoctorAvailabilityId);
            modelBuilder.Entity<DoctorAvailability>()
                .Property(p => p.DoctorAvailabilityId)
                .ValueGeneratedOnAdd();
        }

        private void ConfigureTimeRange(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TimeRange>()
                .HasKey(p => p.TimeRangeId);
            modelBuilder.Entity<TimeRange>()
                .Property(p => p.TimeRangeId)
                .ValueGeneratedOnAdd();
        }

        private void ConfigureDay(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Day>()
                .HasKey(p => p.DayId);
            modelBuilder.Entity<Day>()
                .Property(p => p.DayId)
                .ValueGeneratedOnAdd();
        }
    }
}