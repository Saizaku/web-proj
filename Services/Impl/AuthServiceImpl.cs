using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_proj.Modles;
using web_proj.Presistance.Contexts;
using web_proj.Services.Response;
using Microsoft.EntityFrameworkCore;

namespace web_proj.Services.Impl
{
    public class AuthServiceImpl : IAuthService
    {
        internal DbSet<User> dbSet;
        private readonly IUserService userService;

        public AuthServiceImpl(AppDbContext context, IUserService userService){
            dbSet = context.Users;
            this.userService = userService;
        }
        public BaseResponse<User> login(LoginForm loginForm){
            User user = dbSet.Where(x=> x.Username == loginForm.Username).FirstOrDefault();
            if(user == null)
                return new BaseResponse<User>(false, "Invalid username");

            if(user.Password== loginForm.Password)
                return new BaseResponse<User>(true, "", user);

            return new BaseResponse<User>(false, "Wrong passowrd");
        }
        public async Task<BaseResponse<User>> registerAsync(User user){
            User existingUser = dbSet.Where(x=> x.Username == user.Username).FirstOrDefault();
            if(existingUser != null)
                return new BaseResponse<User>(false, "Invalid username");

            BaseResponse<User> response = await userService.SaveAsync(user);
            if(response.Success)
                return new BaseResponse<User>(true, "", user);
            return new BaseResponse<User>(false, response.Message);
        }

    }
}