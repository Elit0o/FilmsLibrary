using System;
using Business;
using Data;
using Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Eventing.Reader;

namespace FilmLibrary
{
    public class ConsoleInterface
    {
        public ConsoleInterface()
        {
            UserInput();
            HardcodedData();
        }


        private FilmBusiness filmBusiness = new FilmBusiness();
        private FilmLibraryContext filmContext = new FilmLibraryContext();
        private int closeOperationId = 6;
        private int closeFilterId = 5;

        private void UserMenu()
        {
            Console.WriteLine(new string('-', 40));
            Console.WriteLine(new string(' ', 18) + "MENU" + new string(' ', 18));
            Console.WriteLine(new string('-', 40));
            Console.WriteLine("1. Add new film");
            Console.WriteLine("2. Remove film");
            Console.WriteLine("3. Update film");
            Console.WriteLine("4. Fetch film");
            Console.WriteLine("5. Filters");
            Console.WriteLine("6. Exit");
        }

        private void FiltersMenu()
        {
            Console.WriteLine(new string('-', 40));
            Console.WriteLine(new string(' ', 18) + "FILTERS" + new string(' ', 18));
            Console.WriteLine(new string('-', 40));
            Console.WriteLine("1. Films sorted by title");
            Console.WriteLine("2. Films sorted by actor");
            Console.WriteLine("3. Films sorted by genre");
            Console.WriteLine("4. Back to main menu");
            Console.WriteLine("5. Exit");
        }

        private void FiltersInput()
        {
            var operation = -1;

            do
            {
                FiltersMenu();
                operation = int.Parse(Console.ReadLine());
                switch (operation)
                {
                    case 1:
                        FilmSortedByTitle();
                        break;
                    case 2:
                        FilmSortedByActor();
                        break;
                    case 3:
                        FilmSortedByGenre();
                        break;
                    case 4:
                        UserInput();
                        break;
                    case 5:
                        Environment.Exit(0);
                        break;
                    default:
                        break;
                }
            } while (operation != closeFilterId);
        }

        private void UserInput()
        {
            var operation = -1;

            do
            {
                UserMenu();
                operation = int.Parse(Console.ReadLine());
                switch (operation)
                {
                    case 1:
                        AddNewFilm();
                        break;
                    case 2:
                        RemoveFilm();
                        break;
                    case 3:
                        UpdateFilm();
                        break;
                    case 4:
                        FetchFilm();
                        break;
                    case 5:
                        FiltersInput();
                        break;
                    case 6:
                        Environment.Exit(0);
                        break;
                    default:
                        break;
                }
            } while (operation != closeOperationId);
        }

        private void FilmSortedByTitle()
        {
            Console.WriteLine(new string('-', 40));
            Console.WriteLine(new string(' ', 9) + "FILMS SORTED BY TITLE" + new string(' ', 18));
            Console.WriteLine(new string('-', 40));
            var films = filmBusiness.SortByTitle();
            foreach (var item in films)
            {
                Console.WriteLine($"{item.ID} {item.Title} {item.Genre} {item.ReleaseDate} {item.Actor}");
            }
            Console.WriteLine(new string('-', 40));
            UserInput();
        }

        private void FilmSortedByActor()
        {
            Console.WriteLine(new string('-', 40));
            Console.WriteLine(new string(' ', 9) + "FILMS SORTED BY ACTOR" + new string(' ', 18));
            Console.WriteLine(new string('-', 40));
            var films = filmBusiness.SortByActor();
            foreach (var item in films)
            {
                Console.WriteLine($"{item.ID} {item.Title} {item.Genre} {item.ReleaseDate} {item.Actor}");
            }
            Console.WriteLine(new string('-', 40));
            UserInput();
        }

        private void FilmSortedByGenre()
        {
            Console.WriteLine(new string('-', 40));
            Console.WriteLine(new string(' ', 9) + "FILMS SORTED BY GENRE" + new string(' ', 18));
            Console.WriteLine(new string('-', 40));
            var films = filmBusiness.SortByGenre();
            foreach (var item in films)
            {
                Console.WriteLine($"{item.ID} {item.Title} {item.Genre} {item.ReleaseDate} {item.Actor}");
            }
            Console.WriteLine(new string('-', 40));
            UserInput();
        }

        private void AddNewFilm()
        {
            if (filmContext.films.Count() <= 132)
            {
                Film film = new Film();
                Console.Write("Enter title: ");
                film.Title = Console.ReadLine();
                Console.Write("Enter genre: ");
                film.Genre = Console.ReadLine();
                Console.Write("Enter release date: ");
                film.ReleaseDate = DateTime.Parse(Console.ReadLine());
                Console.Write("Enter actors: ");
                film.Actor = Console.ReadLine();
                filmBusiness.Add(film);
                UserInput();
            }
            else
            {
                Console.WriteLine("The library is full.");
                UserInput();
            }
        }

        private void RemoveFilm()
        {
            Console.WriteLine(new string('-', 40));
            Console.WriteLine(new string(' ', 18) + "LIBRARY" + new string(' ', 18));
            Console.WriteLine(new string('-', 40));
            var films = filmBusiness.SortByTitle();

            foreach (var item in films)
            {
                Console.WriteLine($"{item.ID} {item.Title} {item.Genre} {item.ReleaseDate} {item.Actor}");
            }
            Console.WriteLine(new string('-', 40));

            Console.Write("Enter ID to remove a film from the list: ");
            int id = int.Parse(Console.ReadLine());
            filmBusiness.Remove(id);
            Console.WriteLine("The film has been removed.");
        }

        private void UpdateFilm()
        {
            Console.WriteLine(new string('-', 40));
            Console.WriteLine(new string(' ', 18) + "LIBRARY" + new string(' ', 18));
            Console.WriteLine(new string('-', 40));
            var films = filmBusiness.SortByTitle();

            foreach (var item in films)
            {
                Console.WriteLine($"{item.ID} {item.Title} {item.Genre} {item.ReleaseDate} {item.Actor}");
            }
            Console.WriteLine(new string('-', 40));

            Console.Write("Eneter ID to update a film from the list: ");
            int id = int.Parse(Console.ReadLine());
            Film film = filmBusiness.Get(id);

            if (film != null)
            {
                Console.Write("Enter title: ");
                film.Title = Console.ReadLine();
                Console.Write("Enter genre: ");
                film.Genre = Console.ReadLine();
                Console.Write("Enter release date: ");
                film.ReleaseDate = DateTime.Parse(Console.ReadLine());
                Console.Write("Enter actors: ");
                film.Actor = Console.ReadLine();
                filmBusiness.Update(film);
                Console.WriteLine("Data has been changed successfully.");
                UserInput();
            }
            else
            {
                Console.WriteLine("Film not found.");
                UserInput();
            }
        }

        public void FetchFilm()
        {
            Console.Write("Enter ID to fetch: ");
            int id = int.Parse(Console.ReadLine());
            Film film = filmBusiness.Get(id);
            if (film != null)
            {
                Console.WriteLine(new string('-', 40));
                Console.WriteLine("ID: " + film.ID);
                Console.WriteLine("Title: " + film.Title);
                Console.WriteLine("Genre: " + film.Genre);
                Console.WriteLine("Release date: " + film.ReleaseDate);
                Console.WriteLine("Actors: " + film.Actor);
                Console.WriteLine(new string('-', 40));
                UserInput();
            }
            else
            {
                Console.WriteLine("Film not found.");
                UserInput();
            }
        }

        public void HardcodedData()
        {
            filmBusiness = new FilmBusiness();
            var film = filmBusiness.SortByTitle();

            Film hardcodedFilm = new Film();
            hardcodedFilm.Title = "The Godfather";
            hardcodedFilm.Genre = "Crime";
            hardcodedFilm.ReleaseDate = new DateTime(1972, 3, 24);
            hardcodedFilm.Actor = "Marlon Brando";

            if (film.Count == 0)
            {
                filmBusiness.Add(hardcodedFilm);
            }
        }
    }
}
