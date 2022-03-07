using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MovieLibrary
{
    public class MovieFile
    {
        private string filePath { get; set; }
        public List<Movie> Movies { get; set; }
        
        static IServiceCollection serviceCollection = new ServiceCollection();
        static ServiceProvider serviceProvider = serviceCollection
            .AddLogging(x=>x.AddConsole())
            .BuildServiceProvider();
        static ILogger<Program> logger = serviceProvider.GetService<ILoggerFactory>().CreateLogger<Program>();

        public MovieFile(string path)
        {
            filePath = path;
            Movies = new List<Movie>();
            try
            {
                StreamReader streamReader = new(filePath);
                streamReader.ReadLine();
                while (!streamReader.EndOfStream)
                {
                    Movie movie = new();
                    string entry = streamReader.ReadLine();

                    int quote = entry.IndexOf('"') - 1;
                    if (quote == 1)
                    {
                        entry = entry.Replace('"', ' ');
                    }
                    string[] movieDetails = entry.Split(',');
                    movie.movieID = int.Parse(movieDetails[0]);
                    movie.title = movieDetails[1].Trim();
                    movie.genre = movieDetails[2].Split('|').ToList();
                    Movies.Add(movie);
                }
                streamReader.Close();
                logger.LogInformation("Movies in file {movieCount}", Movies.Count);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                Console.WriteLine("File not found.");
            }
        }
        
        /*public void formatEntry(string entry)
        {
        }*/
        
        public bool testTitle(string title)
        {
            var movieTitle = title.Replace('"', ' ').Trim().ToLower();
            if (Movies.ConvertAll(mov => mov.title.ToLower()).Contains(movieTitle))
            {
                logger.LogInformation("Duplicate movie title {title}", title);
                return false;
            }
            return true;
        }

        public void AddMovie(Movie movie)
        {
            try
            {
                movie.movieID = Movies.Max(mov => mov.movieID) + 1;
                
                string title;
                if (movie.title.IndexOf(',') != -1)
                {
                   title = $"\"{movie.title}\"";
                }
                else
                {
                  title = movie.title;
                }
                
                StreamWriter streamWriter = new (filePath, true);
                streamWriter.WriteLine($"{movie.movieID},{title},{string.Join("|", movie.genre)}");
                streamWriter.Close();
                Movies.Add(movie);
                
                logger.LogInformation("Movie id {movieID} added", movie.movieID);
            } 
            catch(Exception e)
            {
                logger.LogError(e.Message);
                Console.WriteLine("Error adding movie");
            }
        }
    }
}