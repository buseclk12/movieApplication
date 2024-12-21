namespace BLL.DAL;

public class Movies
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public decimal TotalRevenue { get; set; }
    public int DirectorId { get; set; }

    // Navigation properties
    public Directors Director { get; set; }
    public ICollection<MovieGenre> MovieGenres { get; set; }
} 