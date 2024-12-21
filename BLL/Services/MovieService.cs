using BLL.DAL;

namespace BLL.Services
{
    public interface IMovieService
    {
        public IQueryable<MoviesModel> Query();

        public ServiceBase Create(Movies record); 
    }
    public class IMovieService
    {
    
    }
}