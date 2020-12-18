using Microsoft.EntityFrameworkCore;

namespace EFDataAccessLibrary
{
    public interface IDataContext
    {
        DbSet<Image> Images { get; set; }

        void BeginTransaction();
        void Commit();
        void Rollback();
    }
}
