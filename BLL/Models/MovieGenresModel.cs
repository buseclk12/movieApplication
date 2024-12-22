using BLL.DAL;
using System.ComponentModel;

namespace BLL.Models
{
    public class MovieGenresModel
    {
        public MovieGenres Record { get; set; }

        [DisplayName("Movie Name")]
        public string MovieName => Record.Movie?.Name ?? "Unknown";

        [DisplayName("Genre Name")]
        public string GenreName => Record.Genre?.Name ?? "Unknown";

        public int MovieId 
        { 
            get => Record.MovieId; 
            set => Record.MovieId = value; 
        }

        public int GenreId 
        { 
            get => Record.GenreId; 
            set => Record.GenreId = value; 
        }
    }
}