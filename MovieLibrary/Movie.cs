using System.Collections.Generic;

namespace MovieLibrary
{
    public class Movie
    {
        public int movieID { get; set; }
        public string title { get; set; }
        public List<string> genre { get; set; }
        
        public Movie()
        {
            genre = new List<string>();
        }
        
        public string Display()
        {
            return $"ID: {movieID}\n" +
                   $"Title: {title}\n" +
                   $"Genres: {string.Join(" | ", genre)}\n";
        }
    }
}