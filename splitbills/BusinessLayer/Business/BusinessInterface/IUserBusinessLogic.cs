using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Business.BusinessInterface
{
    public interface IUserBusinessLogic<A>
    {
        public Task<IdentityResult> RegisterTask(A entity);

        public Task<bool> LoginTask(string emailId, string password);

        public Task<string> GetUserName(string emailId);
    }
}
