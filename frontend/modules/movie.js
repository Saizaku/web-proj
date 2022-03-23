export class movie{
    constructor(id, name, releaseDate, imgUrl){
        this.id = id;
        this.name = name;
        this.releaseDate = new Date(releaseDate).getFullYear();
        this.imgUrl = imgUrl;
    }

    drawMovie(host, bIsModal){
        let view = document.createElement("div");
        view.classList.add("movie_hero");
        view.id = this.id;

        let img = document.createElement("img");
        img.src = this.imgUrl;
        img.classList.add("movie_img");

        let content = document.createElement("div");
        content.innerHTML = this.name + " (" + this.releaseDate+")";
        content.classList.add("movie_content");

        let addBtn = document.createElement("div");

        if(bIsModal){     
            addBtn.innerHTML = "Add movie";
            addBtn.classList.add("movie_add");
            addBtn.onclick = this.addMovieToWatchlist;
        }else{
            addBtn.innerHTML = "Remove movie";
            addBtn.classList.add("movie_add");
            addBtn.onclick = removeMovieFromWatchlist;
        }


        view.appendChild(img);
        view.appendChild(content);
        view.appendChild(addBtn);
        host.appendChild(view);
    }

    addMovieToWatchlist(){
        let watchlist_id = localStorage.getItem("watchlist_id");
        let movie_id = this.parentElement.id;

        fetch("http://localhost:5291/api/watchlist/addMovie/"+watchlist_id+"/"+movie_id,{
            method:"PUT"
        }).then(s=>{
            if(s.ok){
                const mov = document.getElementById(movie_id);
                let clone = mov.cloneNode(true);

                clone.lastChild.innerHTML = "Remove movie";
                clone.lastChild.onclick = removeMovieFromWatchlist;

                document.getElementById("movies").appendChild(clone);
            }
        })
    }

    
}

export function removeMovieFromWatchlist(){
    let watchlist_id = localStorage.getItem("watchlist_id");
    let movie_id = this.parentElement.id;

    fetch("http://localhost:5291/api/watchlist/removeMovie/"+watchlist_id+"/"+movie_id,{
        method:"DELETE"
    }).then(s=>{
        if(s.ok){
            this.parentElement.remove(); 
        }
    })
}