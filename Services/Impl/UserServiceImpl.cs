using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_proj.Modles;
using web_proj.Services;
using web_proj.Services.Response;
using web_proj.Presistance.Contexts;
using Microsoft.EntityFrameworkCore;

namespace web_proj.Services.Impl
{
    public class UserServiceImpl : BaseServiceImpl<User>, IUserService
    {
        public UserServiceImpl(AppDbContext context) : base(context){
        }
        public override async Task<BaseResponse<User>> UpdateAsync(User user){
            try{
                var existingentity = await dbSet.FirstOrDefaultAsync(x=> x.Id == user.Id);
                if(existingentity == null){
                    return new BaseResponse<User>(false, "No user with provided Id.");
                }

                existingentity.Username = user.Username;
                existingentity.Email = user.Email;
                existingentity.Password = user.Password;
                
                context.SaveChanges();
                return new BaseResponse<User>(true, "Successfully updated user", user);
            } catch (Exception ex){
                return new BaseResponse<User>(false, "Internal server error:" + ex.Message);
            }
        }

        public override async Task<BaseResponse<User>> RemoveAsync(int Id){
            try{
                var existingUser = await dbSet.FirstOrDefaultAsync(x=> x.Id == Id);
                if(existingUser == null){
                    return new BaseResponse<User>(false, "No user with provided Id.");
                }

                dbSet.Remove(existingUser);
                context.SaveChanges();
                return new BaseResponse<User>(true, "Successfully deleated user with id: " + Id);
            } catch (Exception ex){
                return new BaseResponse<User>(false, "Internal server error:" + ex.Message);
            }
        }
        public async Task<ICollection<User>> FindByUsernameAsync(string username){
            return await dbSet.Where(x=> x.Username.Contains(username)).Take<User>(10).ToListAsync();
        }
    }
}