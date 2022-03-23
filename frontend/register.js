import { registerForm, registerUser } from "./modules/auth.js";

document.getElementById("registerButton").addEventListener("click", register);

function register(){
    let username = document.getElementById("username").value;
    let password = document.getElementById("password").value;
    let email = document.getElementById("email").value;
    
    let form = new registerForm(username, password, email);
        
    registerUser(form);
}