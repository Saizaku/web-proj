using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_proj.Services.Response;

namespace web_proj.Services
{
    public interface IBaseService<T> where T : class
    {
        Task<ICollection<T>> FindAllAsync();
        Task<T> FindByIdAsync(int Id);
        Task<BaseResponse<T>> SaveAsync(T user);
        Task<BaseResponse<T>> UpdateAsync(T user);
        Task<BaseResponse<T>>  RemoveAsync(int Id);
    }
}