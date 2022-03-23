using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_proj.Modles;

namespace web_proj.Services
{
    public interface IUserService : IBaseService<User>
    {
        Task<ICollection<User>> FindByUsernameAsync(string username);
    }
}