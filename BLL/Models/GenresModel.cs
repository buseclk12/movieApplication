using BLL.DAL;
using System.ComponentModel;

namespace BLL.Models
{
    public class GenresModel
    {
        public Genres Record { get; set; }

        [DisplayName("Genre Name")]
        public string Name => Record.Name;

        [DisplayName("Associated Movies")]
        public string Movies => Record.MovieGenres != null
            ? string.Join("<br>", Record.MovieGenres.Select(mg => mg.Movie?.Name))
            : "None";

        public List<int> MovieIds
        {
            get => Record.MovieGenres?.Select(mg => mg.MovieId).ToList();
            set => Record.MovieGenres = value.Select(v => new MovieGenres() { MovieId = v }).ToList();
        }
    }
}