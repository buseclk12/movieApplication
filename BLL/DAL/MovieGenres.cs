namespace BLL.DAL;

public class MovieGenres
{
    public int Id { get; set; }
    public int MovieId { get; set; }
    public int GenreId { get; set; }

    // Navigation properties
    public Movies Movie { get; set; }
    public Genres Genre { get; set; }
} 