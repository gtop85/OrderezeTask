using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace EFDataAccessLibrary
{
    public class ImageContext : DbContext, IDataContext
    {
        public ImageContext(DbContextOptions options) : base(options) { }

        public DbSet<Image> Images { get; set; }

        private IDbContextTransaction _transaction;

        public void BeginTransaction()
        {
            _transaction = Database.BeginTransaction();
        }

        public void Commit()
        {
            try
            {
                SaveChanges();
                _transaction.Commit();
            }
            finally
            {
                _transaction.Dispose();
            }
        }

        public void Rollback()
        {
            _transaction.Rollback();
            _transaction.Dispose();
        }
    }
}
