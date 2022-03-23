using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web_proj.Services.Response
{
    public class LoginForm
    {
        public string Username{get; set;}
        public string Password{get; set;}

        public LoginForm(string username, string password){
            Username = username;
            Password = password;
        }
    }
}