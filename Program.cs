using System;
using System.Linq;

namespace MovieLibrary_DB1
{
    class Program
    {
        static void Main(string[] args)
        {
            string libraryOption = "";
            do
            {
                string oops = "";
                do 
                {
                    Console.WriteLine("\nMOVIE LIBRARY.  HOW CAN WE HELP YOU?"
                    +"\nA Add a movie to the library"
                    +"\nB Search for movie in library" 
                    +"\nC Update a movie in library"
                    +"\nD Delete a movie from the library" 
                    +"\nQUIT program");
                    libraryOption = Console.ReadLine().ToUpper();
                    oops = (libraryOption == "A" || libraryOption == "QUIT" ||libraryOption == "B" 
                    || libraryOption == "C" || libraryOption == "D") ? "Y" : "N";
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
                    DataModels.MovieManager movieManager = new DataModels.MovieManager();
                    movieManager.Search();
                }

                //UPDATE MOVIE
                else if (libraryOption.ToUpper() == "C")
                {
                    DataModels.MovieManager movieManager = new DataModels.MovieManager();
                    movieManager.Update();
                }

                //DELETE MOVIE
                else if (libraryOption.ToUpper() == "D")
                {
                    DataModels.MovieManager movieManager = new DataModels.MovieManager();
                    movieManager.Delete();
                }

            } while (!(libraryOption.ToUpper() == "QUIT"));    
        }
    }
}

