using DAL.Models;
using SharedLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer.Business.BusinessInterface
{
    public interface IGroupExpenseBusinessLogic<A>
    {
        public Task<bool> AddExpenseInGroup(A groupExpenseDTO);

        public Task<IEnumerable<GroupExpenseDTO>> GetAllGroupExpense(Guid groupId);

        public Task<IEnumerable<ParticularUserExpenseDTO>> GetUsersInExpenses(Guid expenseId);

        public Task<Double> DisplayExpense(Guid ID);

        public Task<bool> DeleteExpense(Guid ID);

        public Task<bool> UpdateExpenseInGroup(UpdateExpenseDTO ExpenseDTO);

    }
}
