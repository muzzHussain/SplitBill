using AutoMapper;
using BusinessLayer.Business.BusinessInterface;
using DAL;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using SharedLayer.DTOs;
using System;
using System.Threading.Tasks;

namespace BusinessLayer.Business.BusinessImplementation
{
    internal class UserBusinessLogic : IUserBusinessLogic<SignUpDTO>
    {
        private readonly IMapper _mapper;
        private readonly IDataAccessLayer _dataAccessLayer;

        private readonly UserManager<IdentityUser> _userManager;

        public UserBusinessLogic(IMapper mapper, IDataAccessLayer dataAccessLayer, UserManager<IdentityUser> userManager)
        {
            _mapper = mapper;
            _dataAccessLayer = dataAccessLayer;
            _userManager = userManager;
        }

        private static bool VerifyPassword(string password, string passwordHash)
        {
            var passwordHasher = new PasswordHasher<object>();
            var verifyResult = passwordHasher.VerifyHashedPassword(null, passwordHash, password);
            return verifyResult == PasswordVerificationResult.Success;
        }

        public async Task<IdentityResult> RegisterTask(SignUpDTO entity)
        {
            User userFromDAL = await _dataAccessLayer.Read().UserRead().GetByConditionTask((x)=> x.Email==entity.Email);
            if (userFromDAL == null)
            {
                var identityUser = new IdentityUser()
                {
                    UserName = entity.UserName,
                    Email = entity.Email,
                };
                var res = await _userManager.CreateAsync(identityUser, entity.Password);
                return res;
            }
            else
            {
                throw new ArgumentException("Email Already Exists !");
            }
        }

        public async Task<bool> LoginTask(string emailId, string password)
        {
            User userFromDAL = await _dataAccessLayer.Read().UserRead().GetByConditionTask((x) => x.Email == emailId);
            if (userFromDAL == null)
            {
                return false;
            }
            if (VerifyPassword(password, userFromDAL.PasswordHash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<string> GetUserName(string emailId)
        {
            User user = await _dataAccessLayer.Read().UserRead().GetByConditionTask((x) => x.Email == emailId);
            if (user == null)
            {
                throw new ArgumentException("Email Id doesn't exist");
            }
            return user.UserName;
        }

    }
}
