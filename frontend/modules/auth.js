export class loginForm{
    constructor(username, password) {
        this.username=username;
        this.password=password;
    }
}

export class registerForm{
    constructor(username, password, email){
        this.username = username;
        this.password = password;
        this.email = email;
    }

    editUser(){
        
    }
}

export function loginUser(form){
    
    if(form.username.length < 6){
        alert("Invalid username");
        return;
    }
    if(form.password.length<8){
        alert("Invalid password");
        return;
    }
    
    fetch("http://localhost:5291/api/auth/login",{
        method:"POST",
        headers:{
            "Content-Type":"application/json"
        },
        body:JSON.stringify(form)
    }).then(s=>{
        if(s.ok){
            s.json().then(data=>{
                localStorage.setItem("userID", data.id);
                window.location.replace("user.html");
            })
        }
    }).catch(e=>{
        console.log(e);
    });
}

export function registerUser(form){
    
    if(form.username.length < 6){
        alert("Invalid username");
        return;
    }
    if(form.password.length<8){
        alert("Invalid password");
        return;
    }
    if(!form.email.includes("@")){
        alert("Invalid email");
        return;
    }
    
    fetch("http://localhost:5291/api/auth/register",{
        method:"POST",
        headers:{
            "Content-Type":"application/json"
        },
        body:JSON.stringify(form)
    }).then(s=>{
        if(s.ok){
            s.json().then(data=>{
                localStorage.setItem("userID", data.id);
                window.location.replace("user.html");
            })
        }
    }).catch(e=>{
        console.log(e);
    });
}