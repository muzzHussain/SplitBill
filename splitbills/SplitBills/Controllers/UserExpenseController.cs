using BusinessLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SplitBills.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserExpenseController : ControllerBase
    {
        private IBusinessLogic _businessLogic;
        private IConfiguration _config;

        public UserExpenseController(IBusinessLogic businessLogic, IConfiguration config)
        {
            _config = config;
            _businessLogic = businessLogic;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> FetchExpenseById(Guid expenseId)
        {
            var result = await _businessLogic.GetUserExpenseBusinessLogic().fetchUserExpenses(expenseId);
            return StatusCode(200, result);
        }
    }
}
