using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_proj.Modles;
using web_proj.Presistance.Contexts;
using Microsoft.EntityFrameworkCore;
using web_proj.Services.Response;


namespace web_proj.Services.Impl
{
    public class WatchListServiceImpl : BaseServiceImpl<WatchList>, IWatchListService
    {
        public WatchListServiceImpl(AppDbContext context) : base(context){
        }

        public override async Task<WatchList> FindByIdAsync(int id){
            return await dbSet.Include("Movies").FirstOrDefaultAsync(x=> x.Id == id);
        }
        public override async Task<BaseResponse<WatchList>> UpdateAsync(WatchList watchlist){
            try{
                var existingWatchList = await dbSet.FirstOrDefaultAsync(x=> x.Id == watchlist.Id);
                if(existingWatchList == null){
                    return new BaseResponse<WatchList>(false, "No watchlist with provided Id.");
                }

                existingWatchList.Name = watchlist.Name;
                
                context.SaveChanges();
                return new BaseResponse<WatchList>(true, "Successfully updated WatchList", watchlist);
            } catch (Exception ex){
                return new BaseResponse<WatchList>(false, "Internal server error:" + ex.Message);
            }
        }

        public override async Task<BaseResponse<WatchList>> RemoveAsync(int Id){
            try{
                var existingWatchList = await dbSet.FirstOrDefaultAsync(x=> x.Id == Id);
                if(existingWatchList == null){
                    return new BaseResponse<WatchList>(false, "No watchlist with provided Id.");
                }

                dbSet.Remove(existingWatchList);
                context.SaveChanges();
                return new BaseResponse<WatchList>(true, "Successfully deleated watchlist with id: " + Id);
            } catch (Exception ex){
                return new BaseResponse<WatchList>(false, "Internal server error:" + ex.Message);
            }
        }
        public async Task<ICollection<WatchList>> FindByNameAsync(string name){
            return await dbSet.Where(x=> x.Name.Contains(name)).Take<WatchList>(10).ToListAsync();
        }

        public BaseResponse<WatchList> AddMovieToWatchList(int watchListId, int movieId){
            try{
                var existingWatchList = dbSet.Include("Movies").FirstOrDefault(x=> x.Id == watchListId);
                if(existingWatchList == null){
                    return new BaseResponse<WatchList>(false, "No watchlist with provided Id.");
                }

                var movie = context.Movies.Find(movieId);

                 if(movie == null){
                    return new BaseResponse<WatchList>(false, "No movie with provided Id.");
                }


                if(existingWatchList.Movies == null){
                    existingWatchList.Movies = new List<Movie>();
                    existingWatchList.Movies.Add(movie);
                }   
                else if(!existingWatchList.Movies.Contains(movie)){
                    existingWatchList.Movies.Add(movie);
                    context.SaveChanges();
                    return new BaseResponse<WatchList>(true, "Successfully updated WatchList", existingWatchList);
                }
                
                return new BaseResponse<WatchList>(false, "Movie is already in watchList");
            } catch (Exception ex){
                return new BaseResponse<WatchList>(false, "Internal server error:" + ex.Message);
            }
        }
        public BaseResponse<WatchList> RemoveMovieFromWatchList(int watchListId, int movieId){
            try{
                var existingWatchList = dbSet.Include("Movies").FirstOrDefault(x=> x.Id == watchListId);
                if(existingWatchList == null){
                    return new BaseResponse<WatchList>(false, "No watchlist with provided Id.");
                }

                if(existingWatchList.Movies != null){
                    var movie = existingWatchList.Movies.Single(x => x.Id == movieId);
                    existingWatchList.Movies.Remove(movie);
                    context.SaveChanges();
                    return new BaseResponse<WatchList>(true, "Successfully removed movie from WatchList", existingWatchList);
                }
                return new BaseResponse<WatchList>(false, "No moive in watch list with prvided id");
                
            } catch (Exception ex){
                return new BaseResponse<WatchList>(false, "Internal server error:" + ex.Message);
            }
        }

        public async Task<ICollection<WatchList>> FindByOwnerIdAsync(int OwnerId){
            return await dbSet.Where(x=> x.UserId == OwnerId).ToListAsync();
        }
    }
}