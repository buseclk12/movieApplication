namespace BLL.DAL;

public class Directors
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public bool IsRetired { get; set; }

    // Navigation property
    public ICollection<Movie> Movies { get; set; }
} 