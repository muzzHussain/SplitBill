using DAL.DataBase;
using DAL.Models;
using DAL.Repos.Implementation;
using DAL.Repos.Interface;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DAL.ReadAndWriteFactory
{
    public class ReadFactory : IReadFactory
    {
        private readonly IReadOperation<User> _userReadOperation;
        private readonly IReadOperation<UsersGroup> _usersGroupReadOperation;
        private readonly IReadOperation<Group> _groupReadOperation;
        private readonly IReadOperation<UserExpense> _userExpenseReadOperation;
        private readonly IReadOperation<GroupExpense> _groupExpenseReadOperation;

        public ReadFactory(SplitBillDbContext context)
        {
            _userReadOperation = new UserRepo(context);
            _groupReadOperation = new GroupRepo(context);
            _usersGroupReadOperation = new UsersGroupRepo(context);
            _userExpenseReadOperation = new UserExpenseRepo(context);
            _groupExpenseReadOperation = new GroupExpenseRepo(context);
        }

        public IReadOperation<User> UserRead()
        {
            return _userReadOperation;
        }

        public IReadOperation<Group> GroupRead()
        {
            return _groupReadOperation;
        }

        public IReadOperation<UsersGroup> UsersGroupRead()
        {
            return _usersGroupReadOperation;
        }

        public IReadOperation<UserExpense> UserExpenseRead()
        {
            return _userExpenseReadOperation;
        }

        public IReadOperation<GroupExpense> GroupExpenseRead()
        {
            return _groupExpenseReadOperation;
        }

    }
}
