import { watchlist } from "./modules/watchlist.js";
import { movie } from "./modules/movie.js";

document.body.onload = WatchlistDocLoaded;
var watchList = new watchlist();

document.getElementById("addMovie").onclick = function(){
    document.getElementById("addMovieModal").style.display = "block";
    onMovieModalOpen();
}

document.getElementsByClassName("close")[0].onclick = function(){
    closeModal("addMovieModal");
    document.getElementById("allMovies").innerHTML = '';
}


function WatchlistDocLoaded(){
    let id = localStorage.getItem("watchlist_id");
    fetch("http://localhost:5291/api/watchlist/"+id,{
        method: "GET"
    }).then(s=>{
        if(s.ok){
            s.json().then(data=>{
                watchList.name = data.name;
                watchList.userId = data.userId;
                watchList.dateCreated = new Date(data.dateCreated).toLocaleDateString();
                document.getElementById("name").innerHTML = watchList.name;
                document.getElementById("date_created").innerHTML = watchList.dateCreated;
                data.movies.forEach(mov=>{
                    let temp = new movie(mov.id, mov.name, mov.releaseDate, mov.imgUrl);
                    let moviesDiv = document.getElementById("movies");
                    temp.drawMovie(moviesDiv, false);
                })
            })
        }
    }).catch(e=>{
        console.log(e);
    })
}

function onMovieModalOpen(){
    fetch("http://localhost:5291/api/movie",{
        method:"GET"
    }).then(s=>{
        if(s.ok){
            s.json().then(data=>{
                    data.forEach(mov =>{
                        let temp = new movie(mov.id, mov.name, mov.releaseDate, mov.imgUrl);
                        let moviesDiv = document.getElementById("allMovies");
                        temp.drawMovie(moviesDiv, true);
                    })
            })
        }
    })
}

function closeModal(id){
    document.getElementById(id).style.display = "none";
}