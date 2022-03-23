using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using web_proj.Modles;
using web_proj.Services;
using web_proj.Services.Response;

namespace web_proj.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WatchListController : ControllerBase
    {
        private readonly IWatchListService watchlistService;

        public WatchListController(IWatchListService watchlistService){
            this.watchlistService = watchlistService;
        }

        [Route("")]
        [HttpGet]
        public async Task<ActionResult> findAll(){
            return Ok(await watchlistService.FindAllAsync());
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<ActionResult> findById(int id){
            WatchList watchlist = await watchlistService.FindByIdAsync(id);
            if(watchlist != null)
                return Ok(watchlist);
            return BadRequest("No watchlist with provided id");
        }

        [Route("name/{watchlistname}")]
        [HttpGet]
        public async Task<ActionResult> findByName(string watchlistname){
            return Ok(await watchlistService.FindByNameAsync(watchlistname));
        }

        [Route("")]
        [HttpPost]
        public async Task<ActionResult> saveWatchList([FromBody] WatchList watchlist){
            BaseResponse<WatchList> response = await watchlistService.SaveAsync(watchlist);
            if(response.Success)
                return Ok(response.Resource);
            return BadRequest(response.Message);
        }

        [Route("{id}")]
        [HttpPut]
        public async Task<ActionResult> updateWatchList([FromBody] WatchList watchlist){
            BaseResponse<WatchList> response = await watchlistService.UpdateAsync(watchlist);
            if(response.Success)
                return Ok(response.Resource);
            return BadRequest(response.Message);
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<ActionResult> removeWatchList(int Id){
            BaseResponse<WatchList> response = await watchlistService.RemoveAsync(Id);
            if(response.Success)
                return Ok(response.Message);
            return BadRequest(response.Message);
        }

        [Route("addMovie/{id}/{movieId}")]
        [HttpPut]
        public ActionResult addMovieToWatchList(int id, int movieId){
            BaseResponse<WatchList> response = watchlistService.AddMovieToWatchList(id, movieId);
            if(response.Success)
                return Ok(response.Resource);
            return BadRequest(response.Message);
        }

        [Route("removeMovie/{id}/{movieId}")]
        [HttpDelete]
        public ActionResult removeMovieFromWatchList(int id, int movieId){
            BaseResponse<WatchList> response = watchlistService.RemoveMovieFromWatchList(id, movieId);
            if(response.Success)
                return Ok(response.Message);
            return Ok(response.Message);
        }

        [Route("findByOwnerId/{id}")]
        [HttpGet]
        public async Task<ActionResult> findByOwnerId(int id){
            return Ok(await watchlistService.FindByOwnerIdAsync(id));
        }
    }
}