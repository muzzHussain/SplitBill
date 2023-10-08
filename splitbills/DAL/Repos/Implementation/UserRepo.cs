using DAL.DataBase;
using DAL.Models;
using DAL.Repos.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DAL.Repos.Implementation
{
    public class UserRepo : IWriteOperation<User>, IReadOperation<User>
    {
        private readonly SplitBillDbContext _context;

        public UserRepo(SplitBillDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllRecordsTask()
        {
            return (IEnumerable<User>)await _context.Users.ToListAsync();
        }


        public async Task<IEnumerable<User>> GetAllRecordsByConditionTask(Expression<Func<User, bool>> expression)
        {
            var identityTask = await _context.Users.ToListAsync();
            var filteredTask = identityTask.Select(u => MapToUserModel(u)).Where(expression.Compile());

            return filteredTask;
        }

        public async Task<User> GetByConditionTask(Expression<Func<User, bool>> expression)
        {
            return _context.Users.Select(u => MapToUserModel(u)).Where(expression.Compile()).ToList().FirstOrDefault();
        }

        public async Task<bool> AddTask(User entity)
        {
            await _context.Users.AddAsync(entity);
            return true;
        }

        public async Task<bool> RemoveTask(User entity)
        {
            User userEntity = (User)await _context.Users.FirstOrDefaultAsync((ent) => ent.Id == entity.Id);
            _context.Users.Remove(userEntity);
            return true;
        }

        private static User MapToUserModel(IdentityUser identityUser)
        {
            var user = new User
            {
                Id = identityUser.Id,
                UserName = identityUser.UserName,
                NormalizedUserName = identityUser.NormalizedUserName,
                Email = identityUser.Email,
                NormalizedEmail = identityUser.NormalizedEmail,
                EmailConfirmed = identityUser.EmailConfirmed,
                PasswordHash = identityUser.PasswordHash,
                SecurityStamp = identityUser.SecurityStamp,
                ConcurrencyStamp = identityUser.ConcurrencyStamp,
                PhoneNumber = identityUser.PhoneNumber,
                TwoFactorEnabled = identityUser.TwoFactorEnabled,
                LockoutEnd = identityUser.LockoutEnd,
                LockoutEnabled = identityUser.LockoutEnabled,
                AccessFailedCount = identityUser.AccessFailedCount
            };
            return user;
        }
    }
}
