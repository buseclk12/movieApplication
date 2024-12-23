using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class UsersService : ServiceBase, IService<Users, UsersModel>
    {
        public UsersService(Db db) : base(db)
        {
        }

        public ServiceBase Register(Users user)
        {
            if (_db.Users.Any(u => u.UserName == user.UserName.Trim()))
                return Error("User with the same user name exists!");
            user.UserName = user.UserName.Trim();
            user.Password = user.Password.Trim();
            user.IsActive = true;
            user.RoleId = (int)Role.User;
            _db.Users.Add(user);
            _db.SaveChanges();
            return Success("User registered successfully.");
        }

        public IQueryable<UsersModel> Query()
        {
            return _db.Users.Include(u => u.Roles).OrderByDescending(u => u.IsActive).ThenBy(u => u.UserName).Select(u => new UsersModel() { Record = u });
        }

        public ServiceBase Create(Users user)
        {
            if (_db.Users.Any(u => u.UserName == user.UserName.Trim()))
                return Error("User with the same user name exists!");
            user.UserName = user.UserName.Trim();
            user.Password = user.Password.Trim();
            user.IsActive = user.IsActive;
            user.RoleId = user.RoleId;
            _db.Users.Add(user);
            _db.SaveChanges();
            return Success("User created successfully.");
        }

        public ServiceBase Update(Users user)
        {
            if (_db.Users.Any(u => u.Id != user.Id && u.UserName == user.UserName.Trim()))
                return Error("User with the same user name exists!");
            var entity = _db.Users.SingleOrDefault(u => u.Id == user.Id);
            entity.UserName = user.UserName.Trim();
            entity.Password = user.Password.Trim();
            entity.IsActive = user.IsActive;
            entity.RoleId = user.RoleId;
            _db.Users.Update(entity);
            _db.SaveChanges();
            return Success("User updated successfully.");
        }

        public ServiceBase Delete(int id)
        {
            var entity = _db.Users.SingleOrDefault(u => u.Id == id);
            entity.IsActive = false;
            var result = Update(entity);
            if (!result.IsSuccessful)
                return result;
            return Success("User deleted successfully.");
        }
    }
}