export class user{
    constructor(username, password, id, email, watchLists) {
        this.id = id;
        this.email = email;
        this.username=username;
        this.password=password;
        this.watchLists=watchLists;
    }

    deleteUser(){
        fetch("http://localhost:5291/api/user/"+this.id,{
            method:"DELETE"
        }).then(s=>{
            if(s.ok){
                localStorage.clear();
                window.location.replace("index.html");
            }
        }).catch(e=>{
            console.log(e);
        });
    }

    updateUser(){
        fetch("http://localhost:5291/api/user/",{
            method:"PUT",
            headers:{
                "Content-Type":"application/json"
            },
            body:JSON.stringify(this)
        }).then(s=>{
            if(s.ok){
                s.json().then(s=>{
                    document.getElementById("username").innerHTML = s.username;
                    document.getElementById("email").innerHTML = s.email;
                });
            }
        }).catch(e=>{
            console.log(e);
        });
    }
}



