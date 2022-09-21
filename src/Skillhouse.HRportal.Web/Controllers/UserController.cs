using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Skillhouse.HRportal.Business;
using Skillhouse.HRportal.Model;

namespace Skillhouse.HRportal.Controllers
{
    [Route("api/user")]
    public class UserController : BaseController
    {
        private readonly UserBusiness _userBusiness;
        public UserController(UserBusiness userBusiness)
        {
            _userBusiness = userBusiness;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            var authModel = await _userBusiness.GetUserDetails(loginModel);
            if (authModel.Error == null)
            {
                await _userBusiness.PopulateJwtTokenAsync(authModel);
            }
            return Ok(authModel);
        }
    }
}