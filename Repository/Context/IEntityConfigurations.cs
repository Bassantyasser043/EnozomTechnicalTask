using Microsoft.EntityFrameworkCore;

namespace DoctorAvailabiltity.Repository.Context
{
    public interface IEntityConfigurations
    {
        void ConfigureAllEntities(ModelBuilder modelBuilder);
    }
}