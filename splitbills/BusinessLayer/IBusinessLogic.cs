using BusinessLayer.Business.BusinessInterface;
using SharedLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public interface IBusinessLogic
    {
        public IUserBusinessLogic<SignUpDTO> GetUserBusinessLogic();

        public IGroupBusinessLogic<GroupRequestDTO, CreateGroupDTO,GroupDetailsDTO> GetGroupBusinessLogic();

        public IGroupExpenseBusinessLogic<ExpenseDTO> GetGroupExpenseBusinessLogic();

        public IUserExpenseBusinessLogic<ParticularUserExpenseDTO> GetUserExpenseBusinessLogic();


    }
}
