using Microsoft.EntityFrameworkCore;

namespace Repositories.Context
{
    public interface IEntityConfiguration
    {
        void ConfigureAllEntities(ModelBuilder modelBuilder);

    }
}
