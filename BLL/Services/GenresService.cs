using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class GenresService : ServiceBase, IService<Genres, GenresModel>
    {
        public GenresService(Db db) : base(db)
        {
        }

        public IQueryable<GenresModel> Query()
        {
            return _db.Genres.OrderBy(g => g.Name).Select(g => new GenresModel { Record = g });
        }

        public ServiceBase Create(Genres genre)
        {
            if (_db.Genres.Any(g => g.Name.ToUpper() == genre.Name.ToUpper().Trim()))
                return Error("Genre with the same name exists!");
            genre.Name = genre.Name.Trim();
            _db.Genres.Add(genre);
            _db.SaveChanges();
            return Success("Genre created successfully.");
        }

        public ServiceBase Update(Genres genre)
        {
            if (_db.Genres.Any(g => g.Id != genre.Id && g.Name.ToUpper() == genre.Name.ToUpper().Trim()))
                return Error("Genre with the same name exists!");
            var entity = _db.Genres.SingleOrDefault(g => g.Id == genre.Id);
            entity.Name = genre.Name.Trim();
            _db.Genres.Update(entity);
            _db.SaveChanges();
            return Success("Genre updated successfully.");
        }

        public ServiceBase Delete(int id)
        {
            var entity = _db.Genres.Include(g => g.MovieGenres).SingleOrDefault(g => g.Id == id);
            if (entity.MovieGenres.Any())
                return Error("Genre has associated movies!");
            _db.Genres.Remove(entity);
            _db.SaveChanges();
            return Success("Genre deleted successfully.");
        }
    }
}