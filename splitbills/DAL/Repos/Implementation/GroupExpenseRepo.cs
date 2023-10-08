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
    public class GroupExpenseRepo : IWriteOperation<GroupExpense>, IReadOperation<GroupExpense>
    {
        private readonly SplitBillDbContext _context;

        public GroupExpenseRepo(SplitBillDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddTask(GroupExpense entity)
        {
            await _context.GroupExpenses.AddAsync(entity);
            return true;
        }

        public async Task<bool> RemoveTask(GroupExpense entity)
        {
            GroupExpense groupExpense = await _context.GroupExpenses.FirstOrDefaultAsync((x) => x.Id == entity.Id);
            _context.GroupExpenses.Remove(groupExpense);
            return true;
        }

        public async Task<IEnumerable<GroupExpense>> GetAllRecordsTask()
        {
            return await _context.GroupExpenses.ToListAsync();
        }

        public async Task<GroupExpense> GetByConditionTask(Expression<Func<GroupExpense, bool>> expression)
        {
            return await _context.GroupExpenses.FirstOrDefaultAsync(expression);
        }

        public async Task<IEnumerable<GroupExpense>> GetAllRecordsByConditionTask(Expression<Func<GroupExpense, bool>> expression)
        {
            return await _context.GroupExpenses.Where(expression).ToListAsync();
        }
        public async Task<bool> EditTask(GroupExpense entity)
        {
            GroupExpense groupExpense = await _context.GroupExpenses.FirstOrDefaultAsync((x) => x.Id == entity.Id);
            groupExpense.Title = entity.Title;
            groupExpense.ExpenseAmount = entity.ExpenseAmount;
            groupExpense.Timestamp = new DateTime();
            return true;
        }
    }
}
