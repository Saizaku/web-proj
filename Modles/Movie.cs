using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace web_proj.Modles
{
    public class Movie
    {
        [Key]
        [Required]
        public int Id { get; set;}
        [Required]
        public string Name { get; set;}=null!;

        [Required]
        public string imgUrl{get; set;}=null!;

        [Required]
        [DataType( DataType.DateTime)]
        public DateTime ReleaseDate { get; set;}
        [JsonIgnore]
        public ICollection<WatchList>? WatchLists { get; set;}
    }
}