using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class MovieGenresService : ServiceBase, IService<MovieGenres, MovieGenresModel>
    {
        public MovieGenresService(Db db) : base(db)
        {
        }

        public IQueryable<MovieGenresModel> Query()
        {
            return _db.MovieGenres.Include(mg => mg.Movie).Include(mg => mg.Genre).OrderBy(mg => mg.Movie.Name).Select(mg => new MovieGenresModel { Record = mg });
        }

        public ServiceBase Create(MovieGenres movieGenre)
        {
            if (_db.MovieGenres.Any(mg => mg.MovieId == movieGenre.MovieId && mg.GenreId == movieGenre.GenreId))
                return Error("This movie-genre combination already exists!");
            _db.MovieGenres.Add(movieGenre);
            _db.SaveChanges();
            return Success("Movie-Genre created successfully.");
        }

        public ServiceBase Update(MovieGenres movieGenre)
        {
            var entity = _db.MovieGenres.SingleOrDefault(mg => mg.Id == movieGenre.Id);
            if (entity == null)
                return Error("Movie-Genre not found!");
            entity.MovieId = movieGenre.MovieId;
            entity.GenreId = movieGenre.GenreId;
            _db.MovieGenres.Update(entity);
            _db.SaveChanges();
            return Success("Movie-Genre updated successfully.");
        }

        public ServiceBase Delete(int id)
        {
            var entity = _db.MovieGenres.SingleOrDefault(mg => mg.Id == id);
            if (entity == null)
                return Error("Movie-Genre not found!");
            _db.MovieGenres.Remove(entity);
            _db.SaveChanges();
            return Success("Movie-Genre deleted successfully.");
        }
    }
}