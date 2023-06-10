using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business;
using Data.Models;
using Data;
using System.Threading.Tasks;

namespace Business
{
    public class FilmBusiness
    {
        public FilmLibraryContext filmLibraryContext;

        public List<Film> SortByTitle()
        {
            using (filmLibraryContext = new FilmLibraryContext())
            {
                var filmMatrix = filmLibraryContext.films.Where(x => x.Title == "Матрицата");
                var remainingFilms = filmLibraryContext.films.Except(filmMatrix);
                remainingFilms.OrderBy(f => f.Title).ToList();
                List<Film> newFilmList = filmMatrix.Concat(remainingFilms).ToList();
                return newFilmList;
            }
        }

        public List<Film> SortByActor()
        {
            using (filmLibraryContext = new FilmLibraryContext())
            {
                var filmMatrix = filmLibraryContext.films.Where(x => x.Title == "Матрицата");
                var remainingFilms = filmLibraryContext.films.Except(filmMatrix);
                remainingFilms.OrderBy(f => f.Actor).ToList();
                List<Film> newFilmList = filmMatrix.Concat(remainingFilms).ToList();
                return newFilmList;
            }
        }

        public List<Film> SortByGenre()
        {
            using (filmLibraryContext = new FilmLibraryContext())
            {
                var filmMatrix = filmLibraryContext.films.Where(x => x.Title == "Матрицата");
                var remainingFilms = filmLibraryContext.films.Except(filmMatrix);
                remainingFilms.OrderBy(f => f.Genre).ToList();
                List<Film> newFilmList = filmMatrix.Concat(remainingFilms).ToList();
                return newFilmList;
            }
        }

        public void Remove(int id)
        {
            using (filmLibraryContext = new FilmLibraryContext())
            {
                var film = filmLibraryContext.films.Find(id);
                if (film != null)
                {
                    filmLibraryContext.films.Remove(film);
                    filmLibraryContext.SaveChanges();
                }
            }
        }

        public Film Get(int id)
        {
            using (filmLibraryContext = new FilmLibraryContext())
            {
                return filmLibraryContext.films.Find(id);
            }
        }

        public void Update(Film film)
        {
            using (filmLibraryContext = new FilmLibraryContext())
            {
                var item = filmLibraryContext.films.Find(film.ID);
                if (item != null)
                {
                    filmLibraryContext.Entry(item).CurrentValues.SetValues(film);
                    filmLibraryContext.SaveChanges();
                }
            }
        }

        public void Add(Film film)
        {
            using (filmLibraryContext = new FilmLibraryContext())
            {
                filmLibraryContext.films.Add(film);
                filmLibraryContext.SaveChanges();
            }
        }
    }
}
