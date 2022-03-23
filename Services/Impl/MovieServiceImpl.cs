using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_proj.Modles;
using web_proj.Services.Response;
using web_proj.Presistance.Contexts;
using Microsoft.EntityFrameworkCore;

namespace web_proj.Services.Impl
{
    public class MovieServiceImpl : BaseServiceImpl<Movie>, IMovieService
    {
        public MovieServiceImpl(AppDbContext context) : base(context){
        }
        public override async Task<BaseResponse<Movie>> UpdateAsync(Movie movie){
            try{
                var existingentity = await dbSet.FirstOrDefaultAsync(x=> x.Id == movie.Id);
                if(existingentity == null){
                    return new BaseResponse<Movie>(false, "No movie with provided Id.");
                }

                existingentity.Name = movie.Name;
                existingentity.ReleaseDate = movie.ReleaseDate;
                existingentity.imgUrl = movie.imgUrl;
                
                context.SaveChanges();
                return new BaseResponse<Movie>(true, "Successfully updated Movie", movie);
            } catch (Exception ex){
                return new BaseResponse<Movie>(false, "Internal server error:" + ex.Message);
            }
        }

        public override async Task<BaseResponse<Movie>> RemoveAsync(int Id){
            try{
                var existingMovie = await dbSet.FirstOrDefaultAsync(x=> x.Id == Id);
                if(existingMovie == null){
                    return new BaseResponse<Movie>(false, "No movie with provided Id.");
                }

                dbSet.Remove(existingMovie);
                context.SaveChanges();
                return new BaseResponse<Movie>(true, "Successfully deleated movie with id: " + Id);
            } catch (Exception ex){
                return new BaseResponse<Movie>(false, "Internal server error:" + ex.Message);
            }
        }
        public async Task<ICollection<Movie>> FindByNameAsync(string name){
            return await dbSet.Where(x=> x.Name.Contains(name)).Take<Movie>(10).ToListAsync();
        }
    }
}