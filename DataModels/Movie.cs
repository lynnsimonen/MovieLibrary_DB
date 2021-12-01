using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MovieLibrary_DB1.DataModels
{
    public class Movie
    {
        public long Id { get; set; }
        [Required]
        [MaxLength(85)]
        public string Title { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy}")]
        public DateTime ReleaseDate { get; set; }

        
        public virtual ICollection<MovieGenre> MovieGenres {get;set;}
        public virtual ICollection<UserMovie> UserMovies {get;set;}

        public override string ToString() 
        {
            return String.Format("{0,8}  {1,-65}  {2,-45}",Id, Title, ReleaseDate);
        }
    }
}
