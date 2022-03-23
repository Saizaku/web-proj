using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using web_proj.Services;
using web_proj.Modles;
using web_proj.Services.Response;

namespace web_proj.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService movieService;

        public MovieController(IMovieService movieService){
            this.movieService = movieService;
        }

        [Route("")]
        [HttpGet]
        public async Task<ActionResult> findAll(){
            return Ok(await movieService.FindAllAsync());
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<ActionResult> findById(int id){
            Movie movie = await movieService.FindByIdAsync(id);
            if(movie != null)
                return Ok(movie);
            return BadRequest("No movie with provided id");
        }

        [Route("name/{moviename}")]
        [HttpGet]
        public async Task<ActionResult> findByName(string moviename){
            return Ok(await movieService.FindByNameAsync(moviename));
        }

        [Route("")]
        [HttpPost]
        public async Task<ActionResult> saveMovie([FromBody] Movie movie){
            BaseResponse<Movie> response = await movieService.SaveAsync(movie);
            if(response.Success)
                return Ok(response.Resource);
            return BadRequest(response.Message);
        }

        [Route("{id}")]
        [HttpPut]
        public async Task<ActionResult> updateMovie([FromBody] Movie movie){
            BaseResponse<Movie> response = await movieService.UpdateAsync(movie);
            if(response.Success)
                return Ok(response.Resource);
            return BadRequest(response.Message);
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<ActionResult> removeMovie(int Id){
            BaseResponse<Movie> response = await movieService.RemoveAsync(Id);
            if(response.Success)
                return Ok(response.Message);
            return BadRequest(response.Message);
        }
    }
}