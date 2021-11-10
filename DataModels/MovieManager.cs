using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace MovieLibrary_DB1.DataModels
{
    public class MovieManager
    {
        public void Add()
        {
            //2. Add Movie to database -- ADD MOVIE
            //CRUD - Create, Read, Update, Delete
            System.Console.WriteLine("Enter your Movie name: ");
            var movieTitle = Console.ReadLine();

            var movie = new Movie();
            movie.Title = movieTitle;


            System.Console.WriteLine("Enter the Movie Release Date: ");
            int movieReleaseDate = int.Parse(Console.ReadLine());

            //Create an ID#

            using (var db = new Context.MovieContext())
            {
                db.Movies.Add(movie);
                db.SaveChanges();
            }
        } 
        
        public void Search()
        {
        }

        public void Update()
        {
        }

        public void Delete()
        {
        }
    }
}