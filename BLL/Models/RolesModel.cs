using BLL.DAL;
using System.ComponentModel;

namespace BLL.Models
{
    public class RoleModel
    {
        public Roles Record { get; set; }

        [DisplayName("Role Name")]
        public string RoleName => Record.RoleName;
    }
}