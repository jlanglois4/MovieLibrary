using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MovieLibrary
{
    class Program
    {
        static string file = "movies.csv";
        private static string pickedChoice;
       private static MovieFile movieFile = new(file);
      
       
        // Allows selection for the user to choose from
        public static void Main(string[] args)
        {
            var choice = true;
            do
            {
                MainMenu();
                switch (pickedChoice)
                {
                    case "1":
                        ListMovies();
                        break;
                    case "2":
                        AddMovie();
                        break;
                    default:
                        choice = false;
                        break;
                }
            } while (choice);
        }

        // Allows you to enter an option for Main to run
        private static void MainMenu()
        {
            Console.WriteLine("1) List movies.");
            Console.WriteLine("2) Add movie.");
            Console.WriteLine("Enter anything else to exit.");
            pickedChoice = Console.ReadLine();
        }



        private static void ListMovies()
        {
            List<String> list = new();
            foreach (Movie m in movieFile.Movies)
            {
                list.Add(m.Display());
            }
            int index = 0;
            int anotherTen = 10;
            while (list.Count != index)
            {
                if (index < (list.Count - 10))
                {
                    for (int i = index; i < (anotherTen); i++)
                    {
                        Console.WriteLine(list[i]);
                        index += 1;
                    }
                    anotherTen = index + 10;
                    Console.WriteLine("Enter 1 to exit. Enter anything else to continue.");
                    var lineRead = Console.ReadLine();

                    if (lineRead.Equals("1"))
                    {
                        index = list.Count;
                        Console.WriteLine("Exit.");
                    }
                    else
                    {
                        Console.WriteLine("Continue.");
                    }
                }
                else
                {
                    anotherTen = (list.Count - index);
                    for (int i = 0; i < anotherTen; i++)
                    {
                        Console.WriteLine(list[i + index]);
                    }
                    index = list.Count;
                }
            }
        }

        private static void AddMovie()
        {
            Movie movie = new();
            Console.WriteLine("Enter movie title");
            movie.title = Console.ReadLine();
            if (movieFile.testTitle(movie.title)){
                bool b = true;
                while (b)
                {
                    Console.WriteLine(string.Format("1: Enter genre\n" +
                                                    "2: Exit"));
                    string input = Console.ReadLine();
                    switch (input)
                    {
                        case "1":
                            Console.Write("Enter genre: ");
                            string genreInput = Console.ReadLine();
                            
                            if (genreInput != "")
                            {
                                movie.genre.Add(genreInput);
                            }
                            else
                            {
                                Console.Write("Please enter a genre.");
                            }
                            break;
                        case "2" :
                            Console.WriteLine("Exit");
                            movie.genre.Add("N/A");
                            b = false;
                            break;
                    }
                }
                movieFile.AddMovie(movie);
            }
            else
            {
                Console.WriteLine("Movie title already exists\n");
            }
        }


    }
}

