import { user } from "./modules/user.js";
import { watchlist, watchlistForm } from "./modules/watchlist.js";

window.onload = userDocLoaded;

var usr;


var btn = document.getElementById("newWatchlist");

var span = document.getElementsByClassName("close")[0];

document.getElementById("deleteUser").onclick = function(){
    usr.deleteUser();
}

document.getElementById("submitUser").onclick = function(){
    usr.username = document.getElementById("editUsername").value;
    usr.email = document.getElementById("editEmail").value;
    usr.password = document.getElementById("editPassword").value;

    usr.updateUser();
}

btn.onclick = function(){
    document.getElementById("newWatchlistModal").style.display = "block";
}

document.getElementById("editUser").onclick = function(){
    document.getElementById("editUsername").value = usr.username;
    document.getElementById("editEmail").value = usr.email;
    document.getElementById("editUserModal").style.display = "block";
}

document.getElementsByClassName("close")[0].onclick = function(){closeModal("newWatchlistModal");}
document.getElementsByClassName("close")[1].onclick = function(){closeModal("editUserModal");}

document.getElementById("submitWatchlist").addEventListener("click", addNewWatchlist);

function userDocLoaded(){
    usr = new user();
    usr.id = localStorage.getItem("userID");

    fetch("http://localhost:5291/api/user/"+ usr.id,{
        method:"GET"
    }).then(s=>{
        if(s.ok){
            s.json().then(data=>{
                usr.username = data.username;
                usr.email = data.email;
                document.getElementById("username").innerHTML= data.username;
                document.getElementById("email").innerHTML= data.email;
            });
        }
    }).catch(e=>{
        console.log(e);
    });

    fetch("http://localhost:5291/api/watchlist/findByOwnerId/"+usr.id,{
        method:"GET"
    }).then(s=>{
        if(s.ok){
            s.json().then(data=>{
                data.forEach(watchList=>{
                    let temp = new watchlist(watchList.id, watchList.name, watchList.dateCreated, watchList.userId, null);
                    var watchlists = document.getElementById("watchlists");
                    temp.drawWatchList(watchlists);
                })
            })
        }
    }).catch(e=>{
        console.log(e);
    });
}

function addNewWatchlist(){
    let name = document.getElementById("name").value;

    let watchlist = new watchlistForm(name, usr.id);

    watchlist.saveWatchlist();

    closeModal("newWatchlistModal");
}

function closeModal(id){
    document.getElementById(id).style.display = "none";
}

