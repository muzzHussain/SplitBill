using DAL.DataBase;
using DAL.Repos.Interface;
using System.Threading.Tasks;

namespace DAL.Repos.Implementation
{
    public class CommitRepo : ICommitOperation
    {
        private readonly SplitBillDbContext _context;

        public CommitRepo(SplitBillDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() >= 0;
        }
    }
}
