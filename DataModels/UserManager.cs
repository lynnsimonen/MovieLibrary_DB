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
                    occManager.IsInOccList();     

                    using (var db = new Context.MovieContext())
                    {
                        DataModels.Occupation occupation = new DataModels.Occupation();
                        occupation = db.Occupations.Where(s => s.Id == 20).First();

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
                    //System.Console.WriteLine("\n** Error Message: " + e.Message + "**");
                    System.Console.WriteLine((string.Format("An Error has occured.\nError Message: {0}\nInner Exception: {1}",
                    ex.Message.ToString(), ex.InnerException.ToString())));
                    bError = true;
                }
            } while (bError);     
        } 
    }
}