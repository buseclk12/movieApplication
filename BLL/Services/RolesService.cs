using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class RolesService : ServiceBase, IService<Roles, RolesModel>
    {

        public RolesService(Db db) : base(db)
        {
        }

        public IQueryable<RolesModel> Query()
        {
            return _db.Roles.Select(r => new RolesModel() { Record = r });
        }

        public ServiceBase Create(Roles role)
        {
            if (_db.Roles.Any(r => r.Name.ToUpper() == role.Name.ToUpper().Trim()))
                return Error("Role with the same name exists!");
            role.Name = role.Name.Trim();
            _db.Roles.Add(role);
            _db.SaveChanges();
            return Success("Role created successfully.");
        }

        public ServiceBase Update(Roles role)
        {
            if (_db.Roles.Any(r => r.Id != role.Id && r.Name.ToUpper() == role.Name.ToUpper().Trim()))
                return Error("Role with the same name exists!");
            var entity = _db.Roles.SingleOrDefault(r => r.Id == role.Id);
            entity.Name = role.Name.Trim();
            _db.Roles.Update(entity);
            _db.SaveChanges();
            return Success("Role updated successfully.");
        }

        public ServiceBase Delete(int id)
        {
            var entity = _db.Roles.Include(r => r.Users).SingleOrDefault(r => r.Id == id);
            if (entity.Users.Any())
                return Error("Role has relational users!");
            _db.Roles.Remove(entity);
            _db.SaveChanges();
            return Success("Role deleted successfully.");
        }
    }
}