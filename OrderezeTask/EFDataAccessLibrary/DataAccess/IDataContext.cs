using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EFDataAccessLibrary
{
    public interface IDataContext
    {
        DbSet<Image> Images { get; set; }

        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }
}
