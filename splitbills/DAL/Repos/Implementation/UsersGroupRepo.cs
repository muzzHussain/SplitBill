using DAL.DataBase;
using DAL.Models;
using DAL.Repos.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos.Implementation
{
    public class UsersGroupRepo : IWriteOperation<UsersGroup>, IReadOperation<UsersGroup>
    {
        private readonly SplitBillDbContext _context;

        public UsersGroupRepo(SplitBillDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UsersGroup>> GetAllRecordsTask()
        {
            return await _context.UsersGroups.ToListAsync();
        }

        public async Task<UsersGroup> GetByConditionTask(Expression<Func<UsersGroup, bool>> expression)
        {
            return await _context.UsersGroups.FirstOrDefaultAsync(expression);
        }

        public async Task<IEnumerable<UsersGroup>> GetAllRecordsByConditionTask(Expression<Func<UsersGroup, bool>> expression)
        {
            return await _context.UsersGroups.Where(expression).ToListAsync();
        }


        public async Task<bool> AddTask(UsersGroup entity)
        {
            await _context.UsersGroups.AddAsync(entity);
            return true;
        }

        public async Task<bool> RemoveTask(UsersGroup entity)
        {
            //UsersGroup groupEntity = await _context.UsersGroups.SingleOrDefaultAsync((x) => x.UserId == entity.UserId);
            _context.UsersGroups.Remove(entity);
            return true;
        }
    }
}
