using DAL.Models;
using DAL.Repos.Interface;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ReadAndWriteFactory
{
    public interface IReadFactory
    {
        public IReadOperation<User> UserRead();

        public IReadOperation<Group> GroupRead();

        public IReadOperation<UsersGroup> UsersGroupRead();

        public IReadOperation<UserExpense> UserExpenseRead();

        public IReadOperation<GroupExpense> GroupExpenseRead();
    }
}
