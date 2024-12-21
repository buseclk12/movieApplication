namespace BLL.DAL;

public class Users
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public bool IsActive { get; set; }
    public int RoleId { get; set; }

    // Navigation property
    public Roles Roles { get; set; }
} 