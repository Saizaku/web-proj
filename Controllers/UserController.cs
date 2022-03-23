using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using web_proj.Services;
using web_proj.Modles;
using web_proj.Services.Response;

namespace web_proj.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService){
            this.userService = userService;
        }

        [Route("")]
        [HttpGet]
        public async Task<ActionResult> findAll(){
            return Ok(await userService.FindAllAsync());
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<ActionResult> findById(int id){
            User user = await userService.FindByIdAsync(id);
            if(user != null)
                return Ok(user);
            return BadRequest("No user with provided id");
        }

        [Route("username/{username}")]
        [HttpGet]
        public async Task<ActionResult> findByUsername(string username){
            return Ok(await userService.FindByUsernameAsync(username));
        }

        [Route("")]
        [HttpPost]
        public async Task<ActionResult> saveUser([FromBody] User user){
            BaseResponse<User> response = await userService.SaveAsync(user);
            if(response.Success)
                return Ok(response.Resource);
            return BadRequest(response.Message);
        }

        [Route("")]
        [HttpPut]
        public async Task<ActionResult> updateUser([FromBody] User user){
            BaseResponse<User> response = await userService.UpdateAsync(user);
            if(response.Success)
                return Ok(response.Resource);
            return BadRequest(response.Message);
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<ActionResult> removeUser(int id){
            BaseResponse<User> response = await userService.RemoveAsync(id);
            if(response.Success)
                return Ok(response.Message);
            return BadRequest(response.Message);
        }
    }
}