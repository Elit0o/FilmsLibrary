using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Film
    {
        [Key]
        private int id;
        private string title;
        private string genre;
        private DateTime releaseDate;
        private string actor;

        public int ID { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Actor { get; set; }
    }
}
