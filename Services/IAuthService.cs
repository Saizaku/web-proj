using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_proj.Modles;
using web_proj.Services.Response;

namespace web_proj.Services
{
    public interface IAuthService
    {
        BaseResponse<User> login(LoginForm loginForm);
        Task<BaseResponse<User>> registerAsync(User user);
    }
}