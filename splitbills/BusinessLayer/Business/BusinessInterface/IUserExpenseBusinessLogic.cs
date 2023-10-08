using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Business.BusinessInterface
{
    public interface IUserExpenseBusinessLogic<A>
    {
        public Task<IEnumerable<A>> fetchUserExpenses(Guid expenseId);
    }
}
