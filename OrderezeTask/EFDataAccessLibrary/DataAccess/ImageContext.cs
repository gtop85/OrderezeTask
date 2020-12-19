using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;

namespace EFDataAccessLibrary
{
    public class ImageContext : DbContext, IDataContext
    {
        public ImageContext(DbContextOptions options) : base(options) { }

        public DbSet<Image> Images { get; set; }

        private IDbContextTransaction _transaction;

        public async Task BeginTransactionAsync()
        {
            _transaction = await Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            try
            {
                await SaveChangesAsync();
                await _transaction.CommitAsync();
            }
            finally
            {
                await _transaction.DisposeAsync();
            }
        }

        public async Task RollbackAsync()
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
        }
    }
}
