using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_proj.Modles;
using web_proj.Services.Response;

namespace web_proj.Services
{
    public interface IWatchListService : IBaseService<WatchList>
    {
        Task<ICollection<WatchList>> FindByNameAsync(string name);

        Task<ICollection<WatchList>> FindByOwnerIdAsync(int OwnerId);

        BaseResponse<WatchList> AddMovieToWatchList(int watchListId, int movieId);
        BaseResponse<WatchList> RemoveMovieFromWatchList(int watchListId, int movieId);
    }
}