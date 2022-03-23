using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace web_proj.Modles
{
    public class User
    {
        [Key]
        [Required]
        public int Id { get; set;}
        [Required]
        public string Username { get; set;}=null!;
        [Required]
        public string Password { get; set;}=null!;
        [Required]
        public string Email { get; set;}=null!;

        public DateTime DateCreated { get; set;} = DateTime.UtcNow;
    }
}