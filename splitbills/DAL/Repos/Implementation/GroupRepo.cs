using DAL.DataBase;
using DAL.Models;
using DAL.Repos.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DAL.Repos.Implementation
{
    public class GroupRepo : IWriteOperation<Group>, IReadOperation<Group>
    {
        private readonly SplitBillDbContext _context;

        public GroupRepo(SplitBillDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Group>> GetAllRecordsTask()
        {
            return await _context.Groups.ToListAsync();
        }

        public async Task<Group> GetByConditionTask(Expression<Func<Group, bool>> expression)
        {
            return await _context.Groups.FirstOrDefaultAsync(expression);
        }

        public async Task<IEnumerable<Group>> GetAllRecordsByConditionTask(Expression<Func<Group, bool>> expression)
        {
            return await _context.Groups.Where(expression).ToListAsync();
        }

        public async Task<bool> AddTask(Group entity)
        {
            await _context.Groups.AddAsync(entity);
            return true;
        }

        public async Task<bool> RemoveTask(Group entity)
        {
            Group groupEntity = await _context.Groups.FirstOrDefaultAsync((x) => x.Id == entity.Id);
            _context.Groups.Remove(groupEntity);
            return true;
        }
    }
}
