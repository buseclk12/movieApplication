using BLL.DAL;
using System.ComponentModel;

namespace BLL.Models
{
    public class DirectorsModel
    {
        public Directors Record { get; set; }

        [DisplayName("Director Name")]
        public string FullName => $"{Record.Name} {Record.Surname}";

        [DisplayName("Retired")]
        public bool IsRetired => Record.IsRetired;

        [DisplayName("Directed Movies")]
        
        public List<MoviesModel> Movies { get; set; } = new List<MoviesModel>();

    }
}