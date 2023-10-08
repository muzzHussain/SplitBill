using AutoMapper;
using BusinessLayer.Business.BusinessImplementation;
using BusinessLayer.Business.BusinessInterface;
using DAL;
using DAL.DataBase;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SharedLayer.DTOs;

namespace BusinessLayer
{
    public class BusinessLogic : IBusinessLogic
    {
        private readonly IMapper _mapper;
        private readonly IDataAccessLayer _dataAccessLayer;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SplitBillDbContext _context;

        public IUserBusinessLogic<SignUpDTO> _userBusinessLogic;
        public IGroupBusinessLogic<GroupRequestDTO, CreateGroupDTO,GroupDetailsDTO> _groupBusinessLogic;
        public IGroupExpenseBusinessLogic<ExpenseDTO> _groupExpenseBusinessLogic;
        public IUserExpenseBusinessLogic<ParticularUserExpenseDTO> _userExpenseBusinessLogic;
       
        

        public BusinessLogic(IMapper mapper, DbContextOptions<SplitBillDbContext> options, UserManager<IdentityUser> userManager)
        {
            _mapper = mapper;
            _dataAccessLayer = new DAL.DataAccessLayer(options);
            _userManager = userManager;
            _userBusinessLogic = new UserBusinessLogic(_mapper, _dataAccessLayer, _userManager);
            _groupBusinessLogic = new GroupBusinessLogic(_mapper, _dataAccessLayer, _userManager);
            _groupExpenseBusinessLogic = new GroupExpenseBusinessLogic(_mapper, _dataAccessLayer, _context);
            _userExpenseBusinessLogic = new UserExpenseBusinessLogic(mapper, _dataAccessLayer, _context);
        }

        public IUserBusinessLogic<SignUpDTO> GetUserBusinessLogic()
        {
            return _userBusinessLogic;
        }
              
        public IGroupBusinessLogic<GroupRequestDTO, CreateGroupDTO,GroupDetailsDTO> GetGroupBusinessLogic()
        {
            return _groupBusinessLogic;
        }

        public IGroupExpenseBusinessLogic<ExpenseDTO> GetGroupExpenseBusinessLogic()
        {
            return _groupExpenseBusinessLogic;
        }

        public IUserExpenseBusinessLogic<ParticularUserExpenseDTO> GetUserExpenseBusinessLogic()
        {
            return _userExpenseBusinessLogic;
        }
    }
}
