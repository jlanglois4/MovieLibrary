using System.Linq;
using Xunit;

namespace MovieLibrary.Test
{
    public class UnitTest1
    {
        [Fact]
        public void TestReadFile()
        {
            // arrange
            Movie movie = new Movie();
            //act
            var entry = "1,\"Toy Story (1995)\",Adventure|Animation|Children|Comedy|Fantasy";

            int quote = entry.IndexOf('"') - 1;
            if (quote == 1)
            {
                entry = entry.Replace('"', ' ');
            }

            string[] movieDetails = entry.Split(',');
            movie.movieID = int.Parse(movieDetails[0]);
            movie.title = movieDetails[1].Trim();
            movie.genre = movieDetails[2].Split('|').ToList();

            //assert

            Assert.Equal(1, movie.movieID);
            Assert.Equal("Toy Story (1995)", movie.title);

            string[] genres =
            {
                "Adventure", "Animation", "Children", "Comedy", "Fantasy"
            };

            Assert.Equal(genres, movie.genre);
        }
    }
}