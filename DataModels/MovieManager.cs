using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Data;

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
                System.Console.WriteLine($"\nThe New Movie Added: {newMovie?.Id} {newMovie?.Title}");
            }
        } 
        //------------------------------------------------------------------------------------------------------------  
        public void Search()
        {
            System.Console.WriteLine("Search for Movie Name: ");
            var title = Console.ReadLine()?.ToUpper();
            System.Console.WriteLine("Release Year of Movie: ");
            var releaseYear = Console.ReadLine();
            string titleAndYear = title + " (" + releaseYear + ")";
           
            using (var db = new Context.MovieContext())
            {
                var movieTitle = db.Movies.FirstOrDefault(x => x.Title.ToUpper() == titleAndYear.ToUpper());
                if (movieTitle != null) 
                {
                    System.Console.WriteLine($"\nMovie Found: {movieTitle.Id} {movieTitle.Title}");
                }
                    else
                    System.Console.WriteLine("\n* There is no match for that movie and release date in the Movies database.");
                               
            }
        }

        //-----------------------------------------------------------------------------------------------------------
        public void SearchKeyword()
        {
            var searchWord = "";
            System.Console.Write("\nWhat movie Title or keyphrase would you like to search?: ");
            searchWord = Console.ReadLine()?.ToUpper();
            if (searchWord != null)
           {
               using (var db = new Context.MovieContext())
               {
                    var moviesContains = db.Movies.Where(m => m.Title.ToUpper().Contains(searchWord));
                    moviesContains.ToList().ForEach(p => Console.WriteLine(p.ToString()));
                    
                    System.Console.WriteLine($"\nMovie Library matches: ({moviesContains.Count()})");
                    string listMore = "";
                    int skip = 0;
                    int take = 10;
                    do
                    {
                        moviesContains.Skip(skip).Take(take).ToList()
                        .ForEach(s => Console.WriteLine($"     {ToString}"));
                        skip = skip+10;

                        string oops5 = "";
                        do {
                        Console.WriteLine("\nWould you like to have more movies listed? Y/N");
                        listMore = Console.ReadLine().ToUpper();
                        oops5 = (listMore == "Y" || listMore == "N") ? "Y" : "N";
                        } while (oops5 != "Y");  
                    } while (!(listMore == "N"));
               }                
           }
        }
        //-----------------------------------------------------------------------------------------------------------

         public void Display()
                    {
                        using (var db = new Context.MovieContext())
                        {
                            System.Console.WriteLine("\nMovies Database List:\n");
                            foreach (var b in db.Movies) 
                            {
                                if (b.Title == null || b?.Id == null || b.ReleaseDate == null)
                                    System.Console.WriteLine($"Movie ID#{b.Id} is empty.");
                                else
                                    System.Console.WriteLine($"{b.Id} {b.Title} {b.ReleaseDate}");
                            }
                        }
                    }


        //-----------------------------------------------------------------------------------------------------------

        public void Update()
        {
            System.Console.WriteLine("Enter ID# of movie to Update: ");
            var iDSearch = long.Parse(Console.ReadLine());

                System.Console.WriteLine("Enter Updated Movie Title: ");
                var title2 = Console.ReadLine();
                System.Console.WriteLine("Enter updated Release Year: ");
                var releaseYear2 = Console.ReadLine();
                string titleAndYear2 = title2 + " (" + releaseYear2 + ")";

                using (var db = new Context.MovieContext())
                {
                    var updateMovie = db.Movies.Where(x => x.Id == iDSearch).FirstOrDefault();
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
            System.Console.WriteLine("\nEnter ID# of movie to delete: ");
            long IdOrPrint = int.Parse(Console.ReadLine());
            using (var db = new Context.MovieContext())            
            {       
                var maxId  = db.Movies.Max(x => x.Id);
                var minId = db.Movies.Min(x => x.Id);    
                if (IdOrPrint <= maxId && IdOrPrint >= minId)
                {
                    var deleteMovieId = db.Movies.FirstOrDefault(x => x.Id == IdOrPrint);
                    if (deleteMovieId != null)
                    {
                        System.Console.WriteLine($"\nMovie Removed: {deleteMovieId.Id}");
                        db.Movies.Remove(deleteMovieId);
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
}