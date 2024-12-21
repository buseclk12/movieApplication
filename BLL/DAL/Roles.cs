namespace BLL.DAL;

public class Role
{
    public int Id { get; set; }
    public string Name { get; set; }

    // Navigation property
    public ICollection<User> Users { get; set; }
} 