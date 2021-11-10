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
                    MovieManager movieManager = new MovieManager();
                    movieManager.Add();
                }

                //SEARCH FOR MOVIE
                else if (libraryOption.ToUpper() == "B")
                {
                    MovieManager movieManager = new MovieManager();
                    blogManager.Search();
                }

                //UPDATE MOVIE
                else if (libraryOption.ToUpper() == "C")
                {
                     MovieManager movieManager = new MovieManager();
                    postManager.Update();
                }

                //DELETE MOVIE
                else if (libraryOption.ToUpper() == "D")
                {
                    MovieManager movieManager = new MovieManager();
                    postManager.Delete();
                }

            } while (!(libraryOption.ToUpper() == "QUIT"));    
        }
    }
}

