using AutoMapper;
using BusinessLayer.Business.BusinessInterface;
using DAL;
using DAL.DataBase;
using DAL.Models;
using SharedLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLayer.Business.BusinessImplementation
{
    public class GroupExpenseBusinessLogic : IGroupExpenseBusinessLogic<ExpenseDTO>
    {
        private IDataAccessLayer _dataAccessLayer;
        private readonly IMapper _mapper;
        private readonly SplitBillDbContext _context;
        public GroupExpenseBusinessLogic(IMapper mapper, IDataAccessLayer dataAccessLayer, SplitBillDbContext context)
        {
            _dataAccessLayer = dataAccessLayer;
            _mapper = mapper;
            _context = context;
        }

        public async Task<bool> AddExpenseInGroup(ExpenseDTO expenseDTO)
        {
            var totalAmountSpend = expenseDTO.UsersList.Sum((x) => x.Value);
            var countUsers = expenseDTO.UsersList.Count;
            var perPerson = totalAmountSpend / countUsers;

            GroupExpense groupExpense = new GroupExpense()
            {
                Id = new Guid(),
                Title = expenseDTO.Title,
                GroupId = expenseDTO.GroupId,
                ExpenseAmount = totalAmountSpend,
                Timestamp = DateTime.Now
            };
            await _dataAccessLayer.Write().GroupExpenseWrite().AddTask(groupExpense);


            foreach (var user in expenseDTO.UsersList)
            {
                var userDetails = await _dataAccessLayer.Read().UserRead().GetByConditionTask((x) => x.Email == user.Key);
                UserExpense userExpense = new UserExpense()
                {

                    ExpenseId = groupExpense.Id,
                    GroupId = expenseDTO.GroupId,
                    UserId = userDetails.Id,
                    Spend_Amount = user.Value,
                    Per_Person = perPerson
                };
                await _dataAccessLayer.Write().UserExpenseWrite().AddTask(userExpense);
            }
            await _dataAccessLayer.Write().CommitWrite().SaveChanges();
            return true;
        }

        public async Task<IEnumerable<GroupExpenseDTO>> GetAllGroupExpense(Guid groupId)
        {
            IList<GroupExpenseDTO> expenseDetail = new List<GroupExpenseDTO>();
            IEnumerable<GroupExpense> groupExpenseDetails = await _dataAccessLayer.Read().GroupExpenseRead().GetAllRecordsByConditionTask((x) => x.GroupId == groupId);

            foreach(var item in groupExpenseDetails)
            {
                GroupExpenseDTO groupExpenseDTO = new GroupExpenseDTO
                {
                    ExpenseId = item.Id,
                    ExpenseTitle = item.Title,
                    TotalExpenseAmount = item.ExpenseAmount
                };
                expenseDetail.Add(groupExpenseDTO);
            }
            return expenseDetail;
        }

        public async Task<Double> DisplayExpense(Guid groupId)
        {
            IEnumerable<GroupExpense> expenses = await _dataAccessLayer.Read().GroupExpenseRead().GetAllRecordsByConditionTask((x) => x.GroupId == groupId);

            if (expenses == null)
            {
                Console.WriteLine("No expenses found for the provided group ID.");
                return 1000; 
            }

            double totalExpense = expenses.Sum(expense => expense.ExpenseAmount);

            return totalExpense;


        }

        public async Task<bool> DeleteExpense(Guid ID)
        {
            GroupExpense exp = await _dataAccessLayer.Read().GroupExpenseRead().GetByConditionTask((x) => x.Id == ID);
            if (exp != null)
            {
                GroupExpense data = new GroupExpense
                {
                    Id = exp.Id,
                    Title = exp.Title,
                    GroupId = exp.GroupId,
                    ExpenseAmount = exp.ExpenseAmount,
                    Timestamp = exp.Timestamp,
                };


                var result = await _dataAccessLayer.Write().GroupExpenseWrite().RemoveTask(data);
                await _dataAccessLayer.Write().CommitWrite().SaveChanges();
                return result;
            }
            return true;
        }

        public async Task<IEnumerable<ParticularUserExpenseDTO>> GetUsersInExpenses(Guid expenseId)
        {
            IEnumerable<UserExpense> userInExpense = await _dataAccessLayer.Read().UserExpenseRead().GetAllRecordsByConditionTask((x) => x.ExpenseId == expenseId);
            IList<ParticularUserExpenseDTO> userExpenseList = new List<ParticularUserExpenseDTO>();

            foreach(var item in userInExpense)
            {
                User user = await _dataAccessLayer.Read().UserRead().GetByConditionTask((x) => x.Id == item.UserId);
                ParticularUserExpenseDTO userList = new ParticularUserExpenseDTO
                {
                    ExpenseId = item.ExpenseId,
                    userName = user.UserName,
                    UserId = item.UserId,
                    spendAmount = item.Spend_Amount,
                    perPerson = item.Per_Person,
                    GroupId = item.GroupId
                };
                userExpenseList.Add(userList);
            }
            
            return userExpenseList;
        }

        public async Task<bool> UpdateExpenseInGroup(UpdateExpenseDTO data)
        {
            var result = await _dataAccessLayer.Read().GroupExpenseRead().GetByConditionTask((x) => x.Id == data.ExpenseId);
            if (result != null)
            {
                var totalAmountSpend = data.UsersList.Sum((x) => x.Value);
                result.ExpenseAmount = totalAmountSpend;
                result.Title = data.Title;
                result.Timestamp = new DateTime().Date;
                await _dataAccessLayer.Write().CommitWrite().SaveChanges();

                var countUsers = data.UsersList.Count();
                var perPerson = totalAmountSpend / countUsers;
                var expenseDetails = await _dataAccessLayer.Read().GroupExpenseRead().GetByConditionTask((x) => x.Id == data.ExpenseId);

                IEnumerable<UserExpense> userExpense = await _dataAccessLayer.Read().UserExpenseRead().GetAllRecordsByConditionTask((x) => x.ExpenseId == data.ExpenseId);

                var newList = new Dictionary<string, double>();
               
                foreach (var item in data.UsersList)
                {
                    var userData = await _dataAccessLayer.Read().UserRead().GetByConditionTask((x) => x.Email == item.Key);
                    var userId = userData.Id;
                    newList.Add(userId, item.Value);
                }

                foreach(var item in userExpense)
                {
                    var amount = newList.GetValueOrDefault(item.UserId);
                    var res = await _dataAccessLayer.Read().UserExpenseRead().GetByConditionTask((x) => x.UserId == item.UserId && x.ExpenseId == item.ExpenseId);
                    res.Per_Person = perPerson;
                    res.Spend_Amount = amount;
                    await _dataAccessLayer.Write().CommitWrite().SaveChanges();
                }
                await _dataAccessLayer.Write().CommitWrite().SaveChanges();
                return true;
            }
            return false;
        }

        private async Task<int> CountUserInUserExpense(Guid expenseId)
        {
            IEnumerable<UserExpense> userExpenses = await _dataAccessLayer.Read().UserExpenseRead().GetAllRecordsByConditionTask((x) => x.ExpenseId == expenseId);

            return userExpenses.Count();
        }
    }
}

    

