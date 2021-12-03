using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Data;
using NLog;
using NLog.Web;
using System.Text.RegularExpressions;

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
                        System.Console.WriteLine("\nUser gender (M/F): ");
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

                    using (var db = new Context.MovieContext())
                    {
                        var user = new User() {Age = age, Gender = gender, ZipCode = zipCode};
                        db.Users.Add(user);
                        db.SaveChanges();
                        System.Console.WriteLine($"The new user ID# is: {user.Id}");
                    }
                }
                catch (Exception e)
                {
                    System.Console.WriteLine("\n** Error Message: " + e.Message + "**");
                    bError = true;
                }
            } while (bError);     
        } 
        
    }
}