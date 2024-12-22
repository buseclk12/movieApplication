using BLL.DAL;

namespace BLL.Models
{
    public class MoviesModel
    {
        public Movies Record { get; set; }

        public string Name => Record.Name;

        public string DirectorName => Record.Director != null 
            ? $"{Record.Director.Name} {Record.Director.Surname}" 
            : "Unknown";

        public string ReleaseDateFormatted => Record.ReleaseDate?.ToString("yyyy-MM-dd") ?? "Unknown";

        public string TotalRevenueFormatted => Record.TotalRevenue.ToString("C");

        public List<string> Genres { get; set; } = new List<string>(); // Varsayılan boş liste
    }
}