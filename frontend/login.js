import { loginForm, loginUser } from "./modules/auth.js";

document.getElementById("loginButton").addEventListener("click", login);

function login(){
    let username = document.getElementById("username").value;
    let password = document.getElementById("password").value;

    let form = new loginForm(username, password);
    
    loginUser(form);
}