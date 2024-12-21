using BLL.DAL;
using System.ComponentModel;

namespace BLL.Models
{
    public class RolesModel
    {
        public Roles Record { get; set; }

        [DisplayName("Role Name")]
        public string RoleName => Record.Name;
    }
}