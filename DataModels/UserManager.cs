using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Data;
using NLog;
using NLog.Web;
using System.Text.RegularExpressions;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MovieLibrary_DB1.DataModels
{
    public class UserManager
    {
        public void Add()
        {
            bool bError;
            do
            {                
                try 
                {
                    long age = 0;
                    long occupationId = 1;
                    var gender = "";
                    var zipCode = "";
                    bError = false;
                    do {
                        System.Console.WriteLine("\nADD A NEW USER TO THE MOVIE DATABASE:\nUser gender (M/F): ");
                        gender = (Console.ReadLine()).ToUpper();
                    }
                    while (!(gender == "M" || gender == "F")); 
                    do {
                        System.Console.WriteLine("Age of User between 5 and 100 years: ");
                        age = long.Parse(Console.ReadLine());
                        if (age < 5 || age > 100)
                            {System.Console.WriteLine("\nHint: try age between 5 and 100 years.\n");}
                    }
                    while (age < 5 || age > 100);

                    do {
                        System.Console.WriteLine("User's zipcode (5-digits): ");
                        zipCode = Console.ReadLine();
                    }
                    while (!Regex.IsMatch(zipCode, @"\d{5}$"));

                    DataModels.OccManager occManager = new DataModels.OccManager();
                    occupationId = occManager.IsInOccList();     

                    using (var db = new Context.MovieContext())
                    {
                        DataModels.Occupation occupation = new DataModels.Occupation();
                        occupation = db.Occupations.Where(s => s.Id == occupationId).First();

                        var user = new User() {Age = age, Gender = gender, ZipCode = zipCode, Occupation = occupation};
                        //var user = new User() {Age = 34, Gender = "M", ZipCode = "12343", Occupation = occupation};
                        db.Users.Add(user);    
                        db.SaveChanges();
                        System.Console.WriteLine($"\nNEW USER INFORMATION IS: "+
                        $"\nID: {user.Id}"+
                        $"\nAGE: {user.Age}"+
                        $"\nGENDER: {user.Gender}"+
                        $"\nZIPCODE: {user.ZipCode}"+
                        $"\nOCCUPATION: {occupation.Name}");
                    }
                }
                catch (Exception ex)
                {
                    //System.Console.WriteLine("\n** Error Message: " + e.Message + "**");
                    System.Console.WriteLine((string.Format("An Error has occured.\nError Message: {0}\nInner Exception: {1}",ex.Message.ToString(), ex.InnerException.ToString())));
                    bError = true;
                }
            } while (bError);     
        } 

        public void Rate()
        {
            bool bError;            
            do
            {        
                bError = false;        
                try 
                {       
                    User user = new User();
                    Movie movie = new Movie();
                    UserMovie userMovie = new UserMovie();
                    var userCurrent = user;
                    var movieCurrent = movie;
                    var dateRated = DateTime.Now;
                    long ratingForMovie = 1;
                    long movieToRateId = 0;  
                    long userId = 0;         
                    using (var db = new Context.MovieContext())
                    {
                        var userIdList = db.Users.ToList();
                        do {
                            System.Console.WriteLine("\nRATE A MOVIE!\nEnter your User ID#: ");
                            userId = long.Parse(Console.ReadLine());
                        }
                        while (!(userIdList.Contains(db.Users.FirstOrDefault(s => s.Id == userId))));
                  
                        MovieManager movieManager = new MovieManager();
                        movieManager.SearchKeyword();
                        var movieIdList = db.Movies.ToList();
                        do{                        
                            System.Console.WriteLine("\nEnter the ID# of movie you would like to rate: ");
                            movieToRateId = long.Parse(Console.ReadLine());  
                        }    
                        while (!(movieIdList.Contains(db.Movies.FirstOrDefault(s => s.Id == movieToRateId)))); 

                        do {           
                            System.Console.WriteLine("Enter a rating of 1 (Bad) to 5 (Great!): ");             
                            ratingForMovie = long.Parse(Console.ReadLine());
                        } while (ratingForMovie <1 && ratingForMovie>5);

                        user = db.Users.Where(s => s.Id == userId).First();
                        movie = db.Movies.Where(s => s.Id == movieToRateId).First();
                        var userRating = new UserMovie(){Rating = ratingForMovie, User = user, Movie = movie, RatedAt = dateRated};
                        db.UserMovies.Add(userRating);    
                        db.SaveChanges();
                        System.Console.WriteLine($"\nUSER MOVIE RATING: "+
                        $"\nUser ID: {user.Id}"+
                        $"\nMovie: {movie.Title}"+
                        $"\nRating Given: {userRating.Rating}");                      
                    }                    
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine("\n** Error Message: " + ex.Message + "**");
                    // System.Console.WriteLine((string.Format("An Error has occured.\nError Message: {0}\nInner Exception: {1}",
                    // ex.Message.ToString(), ex.InnerException.ToString())));
                    bError = true;
                }
            } while (bError);     
        } 
        public void Bracket()
        {
            bool bError;            
            do
            {     
                bError = false;
                User user = new User();
                Movie movie = new Movie();
                UserMovie userMovie = new UserMovie();
                long ageBracket = 5;
                long occupationIdBracket = 1;
                var bracketType = "";                    
                do {
                        System.Console.WriteLine("\nFIND TOP RATED MOVIE BY AGE OR OCCUPATION BRACKET!\n(A) Age OR (B) Occupation?: ");
                        bracketType = Console.ReadLine().ToUpper();
                } while (!((bracketType == "A") || (bracketType == "B"))); 
                
                if (bracketType == "A")
                {
                    do {
                        System.Console.WriteLine("Select an age (5 to 100 years): ");
                        ageBracket = long.Parse(Console.ReadLine());
                        if (ageBracket < 5 || ageBracket > 100)
                            {System.Console.WriteLine("\nHint: try age between 5 and 100 years.\n");}
                    } while (ageBracket < 5 || ageBracket > 100);                           
                    try 
                    {   
                        using (var db = new Context.MovieContext())
                        {
                            var topRating = db.UserMovies.Where(s => s.User.Age == ageBracket).Select(s => s.Rating).Max();
                            var topMovieB = db.UserMovies
                                .Where(s => s.User.Age == ageBracket)
                                .Where(s => s.Rating == topRating)                                
                                .OrderBy(s =>s.Movie.Title)
                                .Reverse()
                                .Include(s => s.User)
                                .Include(m => m.Movie)
                                .Last();
                        
                            System.Console.WriteLine($"\nTOP RATED MOVIE\n" +
                                $"Age Bracket: {ageBracket}\n" +
                                $"Top Rating: {topMovieB.Rating}\n" +
                                $"First Movie (alphabetically): {topMovieB.Movie.Title}");
                        }  
                    }
                    catch (Exception ex)
                    {
                        System.Console.WriteLine("\n** Error Message: " + ex.Message + "**");
                        bError = true;
                    }   
                }               
                else if (bracketType == "B")
                {
                    using (var db = new Context.MovieContext()) {
                        var userOccupations = db.Occupations.ToList();
                        do {
                            OccManager occManager = new OccManager();
                            occManager.ListOccupations();
                            System.Console.WriteLine("\nEnter ID# of user occupation (see above): ");
                            occupationIdBracket = long.Parse(Console.ReadLine());                            
                        } while (!(userOccupations.Contains(db.Occupations.FirstOrDefault(s => s.Id == occupationIdBracket))));           
                    }                    
                    try 
                    {   
                        using (var db = new Context.MovieContext())
                        {
                            var occName = db.Occupations.Where(s => s.Id == occupationIdBracket).Select(n => n.Name).First();
                            var topRating = db.UserMovies.Where(s => s.User.Occupation.Id == occupationIdBracket).Select(s => s.Rating).Max();
                            var topMovieB = db.UserMovies
                                .Where(s => s.User.Occupation.Id == occupationIdBracket)
                                .Where(s => s.Rating == topRating)
                                .OrderBy(s =>s.Movie.Title)
                                .Reverse()
                                .Include(s => s.User)
                                .Include(m => m.Movie)
                                .Last();

                            System.Console.WriteLine($"\nTOP RATED MOVIE\n" +
                                $"Occupation Bracket: {occName}\n" +
                                $"Top Rating: {topMovieB.Rating}\n" +
                                $"First Movie (alphabetically): {topMovieB.Movie.Title}");

                            // var topMovieList = db.UserMovies
                            //     .Where(s => s.User.Occupation.Id == occupationIdBracket)
                            //     .Where(s => s.Rating == topRating)
                            //     .OrderBy(s =>s.Movie.Title)
                            //     .Reverse()
                            //     .Include(s => s.User)
                            //     .Include(m => m.Movie)
                            //     .ToList();

                            // foreach (var n in topMovieList)
                            // {
                            //     System.Console.WriteLine($"User ID: {n.User.Id} Rating: {n.Rating} Movie: {n.Movie.Title}");
                            // }
                        }  
                    }
                    catch (Exception ex)
                    {
                        System.Console.WriteLine("\n** Error Message: " + ex.Message + "**");
                        bError = true;
                    }   
                } 
            } while (bError);
        }
        
    }
}