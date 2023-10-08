using BusinessLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SharedLayer.DTOs;
using SplitBills.Authorization;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SplitBills.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UserController : ControllerBase
    {
        private IBusinessLogic _businessLogic;
        private IConfiguration _config;

        public UserController(IBusinessLogic businessLogic, IConfiguration config)
        {
            _businessLogic = businessLogic;
            _config = config;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public string GetClaim()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var userClaims = identity.Claims;
            return userClaims.FirstOrDefault((x) => x.Type == ClaimTypes.Email)?.Value;
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> UserRegisterActionTask([FromBody] SignUpDTO signUpDTO)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(400);
            }
            var response = await _businessLogic.GetUserBusinessLogic().RegisterTask(signUpDTO);
            if (response.Succeeded)
            {
                return StatusCode(201, response.Succeeded);
            }
            else
            {
                return StatusCode(200, response.Errors);
            }
        }

        [HttpGet("LoginUser")]
        [AllowAnonymous]
        public async Task<IActionResult> UserLoginActionTask(
            [FromHeader]
            [Required(ErrorMessage ="Email id is required")]
            [EmailAddress]
            [RegularExpression("\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$", ErrorMessage = "Email id format is not correct")]
            string emailId,
            [FromHeader]
            [Required(ErrorMessage = "Password is required")]
            [PasswordPropertyText(true)]
            [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[#$^+=!*()@%&]).{8,}$", ErrorMessage = "Password format is not correct")]
            string password)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(400);
            }
            bool response = await _businessLogic.GetUserBusinessLogic().LoginTask(emailId, password);

            if (response == true)
            {
                GenerateToken _generateToken = new GenerateToken(emailId, _config);
                return StatusCode(202, _generateToken.GetToken());
            }
            else
            {
                return StatusCode(401, "User login failed, email or password is incorrect");
            }
        }

        [HttpGet("UserName")]
        [Authorize]
        public async Task<IActionResult> GetUserName()
        {
            var userName = await _businessLogic.GetUserBusinessLogic().GetUserName(GetClaim());
            if (userName == null)
            {
                return StatusCode(400, "Email Id not exist!");
            }
            return StatusCode(200, userName);
        }
    }
}
