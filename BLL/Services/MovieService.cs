using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class MovieService : ServiceBase, IService<Movies, MoviesModel>
    {
        public MovieService(Db db) : base(db)
        {
        }

        public IQueryable<MoviesModel> Query()
        {
            return _db.Movies
                .Include(m => m.Director) // Director ile ilişkili veriler dahil edilir
                .OrderBy(m => m.Name)
                .Select(m => new MoviesModel { Record = m });
        }

        public ServiceBase Create(Movies movie)
        {
            if (_db.Movies.Any(m => m.Name == movie.Name.Trim()))
                return Error("A movie with the same name already exists!");

            movie.Name = movie.Name.Trim();
            movie.ReleaseDate = movie.ReleaseDate; // Gerekirse tarih formatı kontrol edilir
            _db.Movies.Add(movie);
            _db.SaveChanges();
            return Success("Movie created successfully.");
        }

        public ServiceBase Update(Movies movie)
        {
            if (_db.Movies.Any(m => m.Id != movie.Id && m.Name == movie.Name.Trim()))
                return Error("A movie with the same name already exists!");

            var entity = _db.Movies.Find(movie.Id);
            if (entity == null)
                return Error("Movie not found!");

            entity.Name = movie.Name.Trim();
            entity.ReleaseDate = movie.ReleaseDate;
            entity.TotalRevenue = movie.TotalRevenue;
            entity.DirectorId = movie.DirectorId;

            _db.Movies.Update(entity);
            _db.SaveChanges();
            return Success("Movie updated successfully.");
        }

        public ServiceBase Delete(int id)
        {
            var entity = _db.Movies
                .Include(m => m.Director) // İlişkili veriler dahil edilir
                .FirstOrDefault(m => m.Id == id);

            if (entity == null)
                return Error("Movie not found!");

            _db.Movies.Remove(entity);
            _db.SaveChanges();
            return Success("Movie deleted successfully.");
        }
    }
}
