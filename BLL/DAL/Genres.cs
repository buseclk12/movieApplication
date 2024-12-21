namespace BLL.DAL;

public class Genres
{
    public int Id { get; set; }
    public string Name { get; set; }

    // Navigation property
    public ICollection<MovieGenre> MovieGenres { get; set; }
} 