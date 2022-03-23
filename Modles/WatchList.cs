using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace web_proj.Modles
{
    public class WatchList
    {
        [Key]
        [Required]
        public int Id { get; set;}

        [Required]
        public string Name { get; set;}=null!;

        public DateTime DateCreated { get; set;} = DateTime.UtcNow;

        [Required]
        [ForeignKey ("UserId")]
        public int UserId{get; set;}

        public User? User{get; set;}

        public ICollection<Movie>? Movies { get; set;}
    }
}