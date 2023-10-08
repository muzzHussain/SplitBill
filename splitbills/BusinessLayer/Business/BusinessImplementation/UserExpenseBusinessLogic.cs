using AutoMapper;
using BusinessLayer.Business.BusinessInterface;
using DAL;
using DAL.DataBase;
using DAL.Models;
using SharedLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer.Business.BusinessImplementation
{
    public class UserExpenseBusinessLogic : IUserExpenseBusinessLogic<ParticularUserExpenseDTO>
    {
        private IDataAccessLayer _dataAccessLayer;
        private readonly IMapper _mapper;
        private readonly SplitBillDbContext _context;

        public UserExpenseBusinessLogic(IMapper mapper, IDataAccessLayer dataAccessLayer, SplitBillDbContext context)
        {
            _context = context;
            _dataAccessLayer = dataAccessLayer;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ParticularUserExpenseDTO>> fetchUserExpenses(Guid expenseId)
        {
            IList<ParticularUserExpenseDTO> userExpenseDetails = new List<ParticularUserExpenseDTO>();
            IEnumerable<UserExpense> userExpenses = await _dataAccessLayer.Read().UserExpenseRead().GetAllRecordsByConditionTask((x) => x.ExpenseId == expenseId);
            GroupExpense groupExpenseDetails = await _dataAccessLayer.Read().GroupExpenseRead().GetByConditionTask((x) => x.Id == expenseId);

            foreach(var item in userExpenses)
            {
                User user = await _dataAccessLayer.Read().UserRead().GetByConditionTask((x) => x.Id == item.UserId);
                ParticularUserExpenseDTO userExpenseDTO = new ParticularUserExpenseDTO
                {
                    expenseTitle = groupExpenseDetails.Title,
                    ExpenseId = item.ExpenseId,
                    UserId = item.UserId,
                    userName = user.UserName,
                    spendAmount = item.Spend_Amount,
                    GroupId = item.GroupId,
                    EmailId = user.Email,
                    perPerson = item.Per_Person
                };
                userExpenseDetails.Add(userExpenseDTO);
            }
            return userExpenseDetails;
        }
    }
}
