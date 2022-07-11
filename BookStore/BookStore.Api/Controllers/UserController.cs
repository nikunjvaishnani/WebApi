using BookStore.Models.Models;
using BookStore.Models.ViewModels;
using BookStore.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BookStore.Api.Controllers
{
    [ApiController]
    [Route("api/User")]
    public class UserController : Controller
    {
        private readonly UserRepository _repository = new UserRepository();

        [HttpGet]
        [Route("GetUsers")]
        public IActionResult GetUsers(int pageIndex = 1, int pageSize = 10, string keyword = "")
        {
            ListResponse<User> response = _repository.GetUsers(pageIndex, pageSize, keyword);
            ListResponse<UserModel> users = new ListResponse<UserModel>()
            {
                Results = response.Results.Select(u => new UserModel(u)),
                TotalRecords = response.TotalRecords,
            };

            return Ok(users);
        }

        [HttpPut]
        [Route("update")]
        public IActionResult UpdateUser(int id,string firstname,string lastname,string email,string password,int roleid)
        {
            User user = new User()
            {
                UserId = id,
                FirstName = firstname,
                LastName = lastname,
                Email = email,
                RoleId = roleid,
                Password = password,
            };

            if(user == null)
            {
                return BadRequest();
            }

            user = _repository.UpdateUser(user);
            return Ok(user);
        }

        [HttpDelete]
        [Route("delete")]
        public IActionResult DeleteUser(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }
            bool isDeleted = _repository.DeleteUser(id);
            return Ok(isDeleted);
        }
    }
}
