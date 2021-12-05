using System;
using System.Linq;
using NLog;
using NLog.Web;

namespace MovieLibrary_DB1
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger log = LogManager.GetCurrentClassLogger();            
            log.Trace("Logging starts now");

            string libraryOption = "";
            do
            {
                string oops = "";
                do 
                {
                    Console.WriteLine("\nMOVIE LIBRARY.  HOW CAN WE HELP YOU?"
                    +"\nA Add a movie to the library"
                    +"\nB Search for a movie or view all items in library" 
                    +"\nC Update a movie in library"
                    +"\nD Delete a movie from the library" 
                    +"\nE Add a new user"
                    +"\nF Rate a movie"
                    +"\nG List the top rated movie by age or occupation bracket"
                    +"\nH List all movies of genre type"
                    +"\nQUIT program");
                    libraryOption = Console.ReadLine().ToUpper();
                    oops = (libraryOption == "A" || libraryOption == "QUIT" ||libraryOption == "B" 
                    || libraryOption == "C" || libraryOption == "D" || libraryOption == "E"
                    || libraryOption == "F") ? "Y" : "N";
                } while (oops != "Y");  

                //ADD MOVIE
                if (libraryOption.ToUpper() == "A")
                {
                    DataModels.MovieManager movieManager = new DataModels.MovieManager();
                    movieManager.Add();
                }

                //SEARCH FOR MOVIE
                else if (libraryOption.ToUpper() == "B")
                {
                    Console.WriteLine("\nWould you like to search for ONE movie or view ALL movies in library?");
                    string searchOption = Console.ReadLine().ToUpper();
                    if (searchOption == "ONE")
                        {
                            DataModels.MovieManager movieManager = new DataModels.MovieManager();
                            movieManager.SearchKeyword();
                        }
                    else if (searchOption == "ALL")
                        {
                            DataModels.MovieManager movieManager = new DataModels.MovieManager();
                            movieManager.Display();
                        }
                }
                   
                //UPDATE MOVIE
                else if (libraryOption.ToUpper() == "C")
                {
                    DataModels.MovieManager movieManager = new DataModels.MovieManager();
                    System.Console.WriteLine("UPDATE A MOVIE IN THE DATABASE\n");
                    movieManager.SearchKeyword();
                    movieManager.Update();
                }

                //DELETE MOVIE
                else if (libraryOption.ToUpper() == "D")
                {
                    DataModels.MovieManager movieManager = new DataModels.MovieManager();
                    movieManager.Delete();
                }

                //ADD NEW USER
                else if (libraryOption.ToUpper() == "E")
                {
                    DataModels.UserManager userManager = new DataModels.UserManager();
                    userManager.Add();
                }

                //RATE A MOVIE
                else if (libraryOption.ToUpper() == "F")
                {
                    DataModels.UserManager userManager = new DataModels.UserManager();
                    userManager.Rate();
                }

                // //LIST TOP RATED MOVIE BY AGE OR OCCUPATION BRACKET
                // else if (libraryOption.ToUpper() == "G")
                // {
                //     DataModels.UserManager userManager = new DataModels.UserManager();
                //     userManager.Bracket();
                // }

                // //LIST OF MOVIES BY GENRE TYPE
                // else if (libraryOption.ToUpper() == "H")
                // {
                //     DataModels.UserManager userManager = new DataModels.UserManager();
                //     userManager.MovieGenreList();
                // }

            } while (libraryOption.ToUpper() != "QUIT");    
        }
    }
}

