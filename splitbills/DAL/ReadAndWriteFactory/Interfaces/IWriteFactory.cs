using DAL.Models;
using DAL.Repos.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ReadAndWriteFactory
{
    public interface IWriteFactory
    {
        public IWriteOperation<User> UserWrite();
        public IWriteOperation<Group> GroupWrite();
        public IWriteOperation<UsersGroup> UsersGroupWrite();
        public IWriteOperation<UserExpense> UserExpenseWrite();
        public IWriteOperation<GroupExpense> GroupExpenseWrite();
        public ICommitOperation CommitWrite();
    }
}
