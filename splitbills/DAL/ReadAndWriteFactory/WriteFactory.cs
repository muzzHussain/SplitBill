using DAL.DataBase;
using DAL.Models;
using DAL.Repos.Implementation;
using DAL.Repos.Interface;

namespace DAL.ReadAndWriteFactory
{
    public class WriteFactory : IWriteFactory
    {
        private readonly IWriteOperation<User> _userWriteOperation;
        private readonly ICommitOperation _commitOperation;
        private readonly IWriteOperation<Group> _groupWriteOperation;
        private readonly IWriteOperation<UsersGroup> _usersGroupWriteOperation;
        private readonly IWriteOperation<UserExpense> _userExpenseWriteOperation;
        private readonly IWriteOperation<GroupExpense> _groupExpenseWriteOperation;

        public WriteFactory(SplitBillDbContext context)
        {
            _userWriteOperation = new UserRepo(context);
            _commitOperation = new CommitRepo(context);
            _groupWriteOperation = new GroupRepo(context);
            _usersGroupWriteOperation = new UsersGroupRepo(context);
            _userExpenseWriteOperation = new UserExpenseRepo(context);
            _groupExpenseWriteOperation = new GroupExpenseRepo(context);
        }

        public IWriteOperation<User> UserWrite()
        {
            return _userWriteOperation;
        }

        public IWriteOperation<Group> GroupWrite()
        {
            return _groupWriteOperation;
        }

        public IWriteOperation<UsersGroup> UsersGroupWrite()
        {
            return _usersGroupWriteOperation;
        }

        public IWriteOperation<UserExpense> UserExpenseWrite()
        {
            return _userExpenseWriteOperation;
        }

        public IWriteOperation<GroupExpense> GroupExpenseWrite()
        {
            return _groupExpenseWriteOperation;
        }

        public ICommitOperation CommitWrite()
        {
            return _commitOperation;
        }
    }
}
