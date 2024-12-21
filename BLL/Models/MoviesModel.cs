using BLL.DAL;

namespace BLL.Models
{
    public class MoviesModel
    {
        public Movies Record { get; set; }

        public string Name => Record.Name;

        public string DirectorName => Record.Director?.Name;

        public string ReleaseDateFormatted => Record.ReleaseDate?.ToString("yyyy-MM-dd");

        public decimal TotalRevenue => Record.TotalRevenue;

    }
}