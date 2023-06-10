using Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class FilmLibraryContext : DbContext
    {
        public FilmLibraryContext() : base("FilmsLibraryContext")
        {

        }
        public DbSet<Film> films { get; set; }
    }
}
