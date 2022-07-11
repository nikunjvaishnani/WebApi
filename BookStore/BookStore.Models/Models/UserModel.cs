using BookStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.Models
{
    public class UserModel
    {
        public UserModel() { }

        public UserModel(User user)
        {
            UserId = user.UserId;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            RoleId = user.RoleId;
        }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
    }
}
