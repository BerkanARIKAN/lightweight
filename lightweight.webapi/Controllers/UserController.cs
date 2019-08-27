using System.Collections.Generic;
using lightweight.business.Abstract;
using lightweight.data.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace lightweight.webapi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public ServiceResponse<Users> Authenticate([FromBody]Users userParam)
        {
            return _userService.Authenticate(userParam.email,userParam.password);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpGet]
        public ServiceResponse<Users> GetAll()
        {
            var response = _userService.GetList();
            return response;
        }
    }
}
