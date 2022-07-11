using BookStore.Models.Models;
using BookStore.Models.ViewModels;
using BookStore.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;

namespace BookStore.Api.Controllers
{
    [ApiController]
    [Route("api/User")]
    public class UserController : Controller
    {
        private readonly UserRepository _repository = new UserRepository();

        [HttpGet]
        [Route("GetUsers")]
        [ProducesResponseType(typeof(ListResponse<UserModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
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

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(UserModel),(int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult),(int)HttpStatusCode.BadRequest)]
        public IActionResult GetUser(int id)
        {
            User user = _repository.GetUser(id);
            if (user == null)
                return BadRequest();

            UserModel userModel = new UserModel(user);
            return Ok(userModel);
        }

        [HttpPut]
        [Route("update")]
        [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
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
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
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
