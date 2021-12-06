using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieLibrary_DB1.DataModels
{
    public class OccManager
    {       
        public long IsInOccList()
        {     
            long occupationId = 0;
            bool bError = false;
            do {
                try {
                    List <Occupation> userOccupations = new List<Occupation> ();
                    using (var db = new Context.MovieContext())
                    {
                        userOccupations = db.Occupations.ToList();
                        foreach (var name in userOccupations)
                        {
                            System.Console.WriteLine($"\t{name.Id}  {name.Name}");
                        }          
                        do {
                            System.Console.WriteLine("Enter ID# of user occupation (see above): ");
                            occupationId = long.Parse(Console.ReadLine());
                        } while (!(userOccupations.Contains(db.Occupations.FirstOrDefault(s => s.Id == occupationId))));
                    }
                }   
                catch (Exception e) 
                { 
                    System.Console.WriteLine("\n** Error Message: " + e.Message + "**");
                    bError = true;
                }
            } while (bError); 
            return occupationId;
           
        }

        public void ListOccupations()
        {
            List <Occupation> userOccupations = new List<Occupation> ();
            using (var db = new Context.MovieContext())
            {
                userOccupations = db.Occupations.ToList();
                foreach (var name in userOccupations)
                {
                    System.Console.WriteLine($"\t{name.Id}  {name.Name}");
                } 
            }
        }   
    }
}