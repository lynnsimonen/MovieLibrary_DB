using Microsoft.EntityFrameworkCore;
using MovieLibrary_DB1.DataModels;

namespace MovieLibrary_DB1.Context
{
    public class MovieContext : DbContext
    {
        public DbSet<Genre> Genres {get;set;}
        public DbSet<Movie> Movies {get;set;}
        public DbSet<MovieGenre> MovieGenres {get;set;}
        public DbSet<Occupation> Occupations {get;set;}
        public DbSet<User> Users {get;set;}
        public DbSet<UserMovie> UserMovies {get;set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=bitsql.wctc.edu;Database=lsimonen_12090_Movie;User ID=lsimonen;Password=000259304;");
        }
    }
}