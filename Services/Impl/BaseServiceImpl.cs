using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using web_proj.Presistance.Contexts;
using web_proj.Services.Response;
using Microsoft.EntityFrameworkCore;

namespace web_proj.Services.Impl
{
    public class BaseServiceImpl<T> : IBaseService<T> where T:class
    {
        protected readonly AppDbContext context;
        internal DbSet<T> dbSet;

        public BaseServiceImpl(AppDbContext context){
            this.context = context;
            this.dbSet = context.Set<T>();
        }
        public virtual async Task<ICollection<T>> FindAllAsync(){
            return await dbSet.ToListAsync();
        }
        public virtual async Task<T> FindByIdAsync(int Id){
            return await dbSet.FindAsync(Id);
        }
        public virtual async Task<BaseResponse<T>> SaveAsync(T entity){
            await dbSet.AddAsync(entity);
            context.SaveChanges();
            return new BaseResponse<T>(true, "Success", entity);
        }
        public virtual async Task<BaseResponse<T>> UpdateAsync(T entity){
            throw new NotImplementedException();
        }
        public virtual async Task<BaseResponse<T>> RemoveAsync(int Id){
            throw new NotImplementedException();
        }

        
    }
}