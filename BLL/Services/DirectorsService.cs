using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class DirectorsService : ServiceBase, IService<Directors, DirectorsModel>
    {
        public DirectorsService(Db db) : base(db)
        {
        }

        public IQueryable<DirectorsModel> Query()
        {
            return _db.Directors
                .Include(d => d.Movies) // Include related movies
                .OrderBy(d => d.Name)
                .Select(d => new DirectorsModel
                {
                    Record = d,
                    Movies = d.Movies.Select(m => new MoviesModel
                    {
                        Record = m
                    }).ToList() // Convert movies to MoviesModel
                });
        }


        public ServiceBase Create(Directors director)
        {
            if (_db.Directors.Any(d => d.Name.ToUpper() == director.Name.ToUpper().Trim() && d.Surname.ToUpper() == director.Surname.ToUpper().Trim()))
                return Error("Director with the same name and surname exists!");
            director.Name = director.Name.Trim();
            director.Surname = director.Surname.Trim();
            _db.Directors.Add(director);
            _db.SaveChanges();
            return Success("Director created successfully.");
        }

        public ServiceBase Update(Directors director)
        {
            if (_db.Directors.Any(d => d.Id != director.Id && d.Name.ToUpper() == director.Name.ToUpper().Trim() && d.Surname.ToUpper() == director.Surname.ToUpper().Trim()))
                return Error("Director with the same name and surname exists!");
            var entity = _db.Directors.SingleOrDefault(d => d.Id == director.Id);
            entity.Name = director.Name.Trim();
            entity.Surname = director.Surname.Trim();
            entity.IsRetired = director.IsRetired;
            _db.Directors.Update(entity);
            _db.SaveChanges();
            return Success("Director updated successfully.");
        }

        public ServiceBase Delete(int id)
        {
            var entity = _db.Directors.Include(d => d.Movies).SingleOrDefault(d => d.Id == id);
            if (entity.Movies.Any())
                return Error("Director has associated movies!");
            _db.Directors.Remove(entity);
            _db.SaveChanges();
            return Success("Director deleted successfully.");
        }
    }
}
