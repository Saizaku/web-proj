using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using web_proj.Modles;
using web_proj.Services;
using web_proj.Services.Response;

namespace web_proj.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService){
            this.authService = authService;
        }

        [Route("login")]
        [HttpPost]
        public ActionResult<User> login([FromBody] LoginForm loginForm){
            BaseResponse<User> response = authService.login(loginForm);
            if(response.Success)
                return Ok(response.Resource);

            return BadRequest(response.Message);
        }

        [Route("register")]
        [HttpPost]
        public async Task<ActionResult<User>> register([FromBody] User user){
            BaseResponse<User> response = await authService.registerAsync(user);
            if(response.Success)
                return Ok(response.Resource);

            return BadRequest(response.Message);
        }
    }
}