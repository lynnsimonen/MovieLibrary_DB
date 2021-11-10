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
            System.Console.WriteLine("Title of Movie to Add: ");
            var title = Console.ReadLine();
            System.Console.WriteLine("Release Year of Movie: ");
            var releaseYear = Console.ReadLine();
            string titleAndYear = title + " (" + releaseYear + ")";
            using (var db = new Context.MovieContext())
            {
                var movie = new Movie() {Title = titleAndYear};
                db.Movies.Add(movie);
                db.SaveChanges();

                var newMovie = db.Movies.FirstOrDefault(x => x.Title == titleAndYear);
                System.Console.WriteLine($"\nThe New Movie Added: {newMovie.Id} {newMovie.Title}");
            }
        } 
        //------------------------------------------------------------------------------------------------------------  
        public void Search()
        {
            System.Console.WriteLine("Search for Movie Name: ");
            var title = Console.ReadLine();
            System.Console.WriteLine("Release Year of Movie: ");
            var releaseYear = Console.ReadLine();
            string titleAndYear = title + " (" + releaseYear + ")";
           
            using (var db = new Context.MovieContext())
            {
                var movieTitle = db.Movies.FirstOrDefault(x => x.Title.ToUpper() == titleAndYear.ToUpper());
                if (movieTitle != null)
                    {System.Console.WriteLine($"\nMovie Found: {movieTitle.Id} {movieTitle.Title}");}
                    else
                    {
                        System.Console.WriteLine("\n* There is no match for that movie and release date in the Movies database.");
                    }            
            }
        }
        //-----------------------------------------------------------------------------------------------------------

        public void Update()
        {
            System.Console.WriteLine("Enter Title to Update: ");
            var title1 = Console.ReadLine();
            System.Console.WriteLine("Release Year of Movie: ");
            var releaseYear1 = Console.ReadLine();
            string titleAndYear1 = title1 + " (" + releaseYear1 + ")";

            System.Console.WriteLine("Enter Updated Movie Title: ");
            var title2 = Console.ReadLine();
            System.Console.WriteLine("Enter updated Release Year: ");
            var releaseYear2 = Console.ReadLine();
            string titleAndYear2 = title2 + " (" + releaseYear2 + ")";

            using (var db = new Context.MovieContext())
            {
                var updateMovie = db.Movies.FirstOrDefault(x => x.Title.ToUpper() == titleAndYear1.ToUpper());
                System.Console.WriteLine($"\nORIGINAL MOVIE: ({updateMovie.Id}) {updateMovie.Title}");

                updateMovie.Title = titleAndYear2;

                db.Movies.Update(updateMovie);
                System.Console.WriteLine($"\nUPDATED MOVIE: ({updateMovie.Id}) {updateMovie.Title}");
                db.SaveChanges();  
            }
        }
        //-----------------------------------------------------------------------------------------------------------
        public void Delete()
        {
            System.Console.WriteLine("Enter Title to Delete: ");
            var title1 = Console.ReadLine();
            System.Console.WriteLine("Release Year of Movie: ");
            var releaseYear1 = Console.ReadLine();
            string TitleAndYear1 = title1 + " (" + releaseYear1 + ")";

            using (var db = new Context.MovieContext())
            {
                var deleteTitleAndYear = db.Movies.FirstOrDefault(x => x.Title.ToUpper() == TitleAndYear1.ToUpper());
                if (deleteTitleAndYear != null)
                {
                    System.Console.WriteLine($"\nMovie Removed: {deleteTitleAndYear.Id} - {deleteTitleAndYear.Title}");
                     // verify exists first
                    db.Movies.Remove(deleteTitleAndYear);
                    db.SaveChanges();
                }                
                else
                {
                    System.Console.WriteLine("\n* There is no match for that movie and release date in the Movies database.");
                }
            }
        }
    }
}