using EmployeeManager.Infrastructure.DataAccess;

namespace EmployeeManager.Infrastructure.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly IDataContext _context;
        protected BaseRepository(IDataContext context)
        {
            _context = context;
        }
    }
}