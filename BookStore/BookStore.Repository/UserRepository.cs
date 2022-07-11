using BookStore.Models.ViewModels;
using BookStore.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    public class UserRepository : BaseRepository
    { 
        public ListResponse<User> GetUsers(int pageIndex, int pageSize, string keyword)
        {
            keyword = keyword?.ToLower().Trim();
            var query = _context.Users.Where(u => keyword == null || u.FirstName.ToLower().Contains(keyword) || u.LastName.ToLower().Contains(keyword)).AsQueryable();

            int totalRecords = query.Count();
            IEnumerable<User> users = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);

            return new ListResponse<User>()
            {
                Results = users,
                TotalRecords = totalRecords
            };
        }

        public List<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public User GetUser(int id)
        {
            return _context.Users.FirstOrDefault(w => w.UserId == id);
        }

        public User Login(LoginModel model)
        {
            return _context.Users.FirstOrDefault(a => a.Email.ToLower().Equals(model.Email.ToLower()) && a.Password.Equals(model.Password));
        }

        public User Register(RegisterModel model)
        {
            User user = new User()
            {
                Email = model.Email,
                Password = model.Password,
                FirstName = model.FirstName,
                LastName = model.LastName,
                RoleId = model.RoleId,
            };
            var entry = _context.Users.Add(user);
            _context.SaveChanges();
            return entry.Entity;
        }

        public User UpdateUser(User model)
        {
            var entry = _context.Users.Update(model);
            _context.SaveChanges();
            return entry.Entity;
        }

        public bool DeleteUser(int id)
        {
            User user = _context.Users.FirstOrDefault(w => w.UserId == id);
            if (user == null)
                return false;

            _context.Users.Remove(user);
            _context.SaveChanges();

            return true;
        }
    }
}
