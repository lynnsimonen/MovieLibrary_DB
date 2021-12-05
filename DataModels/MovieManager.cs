using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Data;
using NLog;
using NLog.Web;

namespace MovieLibrary_DB1.DataModels
{
    public class MovieManager
    {
        public void Add()
        {
            bool bError;
            do
            {
                
                try 
                {
                    var title = "";
                    var releaseYear = DateTime.Now;
                    int dateMin;
                    int dateMax;
                    do {
                        bError = false;
                        System.Console.WriteLine("\nADD A MOVIE TO THE LIBRARY\nTitle of Movie to Add: ");
                        title = Console.ReadLine();
                        System.Console.WriteLine("Release Year of Movie (YYYY): ");
                        releaseYear = DateTime.Parse("01/01/" + (Console.ReadLine())); //.ToString("yyyy");
                        dateMin = DateTime.Compare(releaseYear, new DateTime(1900, 1, 1));
                        dateMax = DateTime.Compare(releaseYear, new DateTime(2023, 1, 1));
                        if ((dateMax > 0) || (dateMin < 0))
                            {System.Console.WriteLine("\nHint: try a year between 1900 and 2023.\n");}
                    }
                    while ((dateMax > 0) || (dateMin < 0));
                    
                    string titleAndYear = title + " (" + releaseYear.ToString("yyyy") + ")";

                    using (var db = new Context.MovieContext())
                    {
                        var movie = new Movie() {Title = titleAndYear, ReleaseDate = DateTime.Now};
                        db.Movies.Add(movie);
                        db.SaveChanges();

                        var newMovie = db.Movies.FirstOrDefault(x => x.Title == titleAndYear);
                        System.Console.WriteLine($"\nThe New Movie Added: {newMovie?.Id} {newMovie?.Title}");
                    }
                }
                catch (Exception e)
                {
                    System.Console.WriteLine("\n** Error Message: " + e.Message + "**");
                    bError = true;
                }
            } while (bError);     
        } 
        //------------------------------------------------------------------------------------------------------------  
        // SPECIFIC TITLE SEARCH ONLY
        public void Search()
        {
            System.Console.WriteLine("\nSEARCH FOR A SPECIFIC MOVIE\nMovie Title to Search: ");
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
        // SPECIFIC TITLE OR KEYWORD SEARCH
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
                    
                    System.Console.WriteLine($"\nMovie Database matches: ({moviesContains.Count()})");
                    string listMore = "N";
                    int skip = 0;
                    int take = 10;
                    System.Console.WriteLine("{0,8}  {1,-65}  {2,-45}", "ID", "Title", "Release Date");
                    do {
                        var listNow = moviesContains.Skip(skip).Take(take).ToList();
                        listNow.ForEach(b => Console.WriteLine("{0,8}  {1,-65}  {2,-45}", b.Id, b.Title, b.ReleaseDate));
                        skip = skip+10;
                        string oops5 = "";
                        if (listNow.Count() == 10)
                        {
                            do {
                                Console.WriteLine("\nWould you like to have more movies listed? Y/N");
                                listMore = Console.ReadLine().ToUpper();
                                oops5 = (listMore == "Y" || listMore == "N") ? "Y" : "N";
                            } while (oops5 != "Y");
                        }   
                        else 
                        {
                            listMore = "N";
                        }                     
                       
                    } while (listMore !="N");
               }                
           }
        }

        //------------------------------------------------------------------------------------------------
        
        public void Update()
        {
            string titleAndYear2 = "";
            long iDSearch = 100;            
            bool bError;
            do
            {                
                try {
                    bError = false;
                    System.Console.WriteLine("\nUPDATE MOVIE DETAILS\nEnter ID# of movie to Update: ");
                    iDSearch = long.Parse(Console.ReadLine());
                    System.Console.WriteLine("Enter Updated Movie Title: ");
                    var title2 = Console.ReadLine();
                    System.Console.WriteLine("Enter updated Release Year: ");
                    var releaseYear2 = Console.ReadLine();
                    titleAndYear2 = title2 + " (" + releaseYear2 + ")";

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
                catch (Exception e)
                {
                    System.Console.WriteLine("\n** Error Message: " + e.Message + "**");
                    bError = true;
                }
            } while (bError);                   
        }
        //-----------------------------------------------------------------------------------------------------------
        public void Delete()
        {
            System.Console.WriteLine("\nDELETE A MOVIE FROM THE DATABASE\nEnter ID# of movie to delete: ");
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
                        System.Console.WriteLine($"\nMovie Removed: {deleteMovieId.Id} - {deleteMovieId.Title}");
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
         //-----------------------------------------------------------------------------------------------------------

        public void Display()
        {
           
            System.Console.WriteLine("\nLIST OF MOVIES IN THE DATABASE\nMovies Database List:\n");
            string listMore = "";
            int skip = 0;
            int take = 10;
            using (var db = new Context.MovieContext())
            {
                do
                { 
                    db.Movies.Skip(skip).Take(take).ToList()
                    .ForEach(b => Console.WriteLine("{0,8}  {1,-65}  {2,-45}", b.Id, b.Title, b.ReleaseDate));
                    skip = skip+10;
                    string oops5 = "";
                    do {
                        Console.WriteLine("\nWould you like to have more movies listed? Y/N");
                        listMore = Console.ReadLine().ToUpper();
                        oops5 = (listMore == "Y" || listMore == "N") ? "Y" : "N";
                    } while (oops5 != "Y");  
                } while (listMore !="N");
                
            }
        }
        //-----------------------------------------------------------------------------------------------------------
    }
}