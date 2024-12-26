namespace BLL.DAL;

public class Roles
{
    public int Id { get; set; }
    public string Name { get; set; }

    // Navigation property
    public ICollection<Users> Users { get; set; } = new List<Users>();
} 