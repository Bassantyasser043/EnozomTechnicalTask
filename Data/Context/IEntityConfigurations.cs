using Microsoft.EntityFrameworkCore;

namespace DoctorAvailabiltity.Data.Context
{
    public interface IEntityConfigurations
    {
        void ConfigureAllEntities(ModelBuilder modelBuilder);
    }
}