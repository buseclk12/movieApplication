using BLL.DAL;
using System.ComponentModel;

namespace BLL.Models
{
    public class UsersModel
    {
        public Users Record { get; set; }

        [DisplayName("User Name")]
        public string UserName => Record?.UserName ?? string.Empty;

        public string Password => Record?.Password ?? string.Empty;

        [DisplayName("Status")]
        public string IsActive => Record.IsActive ? "Active" : "Not Active";

        public string Role => Record.Roles?.Name;
    }
}