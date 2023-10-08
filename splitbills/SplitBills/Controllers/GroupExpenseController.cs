using BusinessLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SharedLayer.DTOs;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SplitBills.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GroupExpenseController : ControllerBase
    {
        private IBusinessLogic _businessLogic;
        private IConfiguration _config;

        public GroupExpenseController(IBusinessLogic businessLogic, IConfiguration config)
        {
            _businessLogic = businessLogic;
            _config = config;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public string GetClaim()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var userClaim = identity.Claims;
            return userClaim.FirstOrDefault((x) => x.Type == ClaimTypes.Email)?.Value;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddExpenseInGroup([FromBody] ExpenseDTO expenseDTO)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(400);
            }
            bool resp = await _businessLogic.GetGroupExpenseBusinessLogic().AddExpenseInGroup(expenseDTO);
            if (resp)
            {
                return StatusCode(200, "Expense added successfully!");
            }
            return StatusCode(200, "Something went wrong");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> DisplayExpense(Guid groupId)
        {
            double expense = await _businessLogic.GetGroupExpenseBusinessLogic().DisplayExpense(groupId);
            return StatusCode(200, expense);
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteExpense(Guid groupId)
        {
            var result = await _businessLogic.GetGroupExpenseBusinessLogic().DeleteExpense(groupId);
            return StatusCode(200, result);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetGroupExpense(Guid groupId)
        {
            var result = await _businessLogic.GetGroupExpenseBusinessLogic().GetAllGroupExpense(groupId);
            return StatusCode(200, result);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserExpenses(Guid expenseId)
        {
            var result = await _businessLogic.GetGroupExpenseBusinessLogic().GetUsersInExpenses(expenseId);
            return StatusCode(200, result);
        }


        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateExpense([FromBody] UpdateExpenseDTO data)

        {
            if (!ModelState.IsValid)
            {
                return StatusCode(400);
            }

             var resp = await _businessLogic.GetGroupExpenseBusinessLogic().UpdateExpenseInGroup(data);
            if (resp)
            {
                 return StatusCode(200, "Expense Updated successfully!");

             }
                return StatusCode(200, "Something went wrong");
        }
  
    }
}
