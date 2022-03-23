export class watchlist{
    constructor(id, name, dateCreated, userId, movies){
        this.id = id;
        this.name = name;
        this.dateCreated = new Date(dateCreated).toLocaleDateString();
        this.userId = userId;
        this.movies = movies;
    }

    drawWatchList(host){
        let view = document.createElement("div");
        view.id = this.id;

        let line = document.createElement("p");
        line.innerHTML = "Name: " + this.name + " | Date created: " + this.dateCreated;
        view.classList.add("watchlist_line");
        line.onclick = this.clickedWatchlist;
        view.appendChild(line);

        let deleteBtn = document.createElement("span");
        deleteBtn.innerHTML = "&times";
        deleteBtn.onclick = this.deleteWatchlist;

        /*let editBtn = document.createElement("span");
        editBtn.innerHTML = "&#9999;";
        editBtn.onclick = this.editWatchlist;*/

        /*let btnDiv = document.createElement("div");
        btnDiv.appendChild(deleteBtn);
        btnDiv.appendChild(editBtn);*/

        view.appendChild(deleteBtn);
        host.appendChild(view);
    }

    clickedWatchlist(){
        let id = this.parentElement.id;
        localStorage.setItem("watchlist_id", id);
        window.location.replace("watchlist.html");
    }

    deleteWatchlist(){
        fetch("http://localhost:5291/api/watchlist/"+this.parentElement.id,{
            method: "DELETE"
        }).then(s=>{
            if(s.ok){
                this.parentElement.remove();
            }
        }).catch(e=>{
            console.log(e);
        });
    }
}

export class watchlistForm{
    constructor(name, userId){
        this.name = name;
        this.userId = userId;
    }

    saveWatchlist(){
        fetch("http://localhost:5291/api/watchlist",{
        method: "POST",
        headers:{
            "Content-Type":"application/json"
        },
        body:JSON.stringify(this)
        }).then(s=>{
            if(s.ok){
                s.json().then(data=>{
                    let temp = new watchlist(data.id, data.name, data.dateCreated);
                    var watchlists = document.getElementById("watchlists");
                    temp.drawWatchList(watchlists);
                })
            }
        }).catch(e=>{
            console.log(e);
        });
    }
}