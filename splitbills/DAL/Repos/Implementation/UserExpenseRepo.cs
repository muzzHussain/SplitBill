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
    public class UserExpenseRepo : IWriteOperation<UserExpense>, IReadOperation<UserExpense>
    {
        private readonly SplitBillDbContext _context;

        public UserExpenseRepo(SplitBillDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddTask(UserExpense entity)
        {
            await _context.UserExpenses.AddAsync(entity);
            return true;
        }

        public async Task<bool> RemoveTask(UserExpense entity)
        {
            UserExpense userExpense = await _context.UserExpenses.FirstOrDefaultAsync((x) => x.UserId == entity.UserId);
            _context.UserExpenses.Remove(userExpense);
            return true;
        }

        public async Task<IEnumerable<UserExpense>> GetAllRecordsTask()
        {
            return await _context.UserExpenses.ToListAsync();
        }

        public async Task<UserExpense> GetByConditionTask(Expression<Func<UserExpense, bool>> expression)
        {
            return await _context.UserExpenses.FirstOrDefaultAsync(expression);
        }

        public async Task<IEnumerable<UserExpense>> GetAllRecordsByConditionTask(Expression<Func<UserExpense, bool>> expression)
        {
            return await _context.UserExpenses.Where(expression).ToListAsync();
        }

    }
}
