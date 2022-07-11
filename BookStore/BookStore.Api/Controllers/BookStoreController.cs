using BookStore.Models.Models;
using BookStore.Models.ViewModels;
using BookStore.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BookStore.Api.Controllers
{
    [ApiController]
    [Route("api/Public")]
    public class BookStoreController : Controller
    {
        private readonly UserRepository _repository = new UserRepository();

        [HttpPost]
        [Route("Login")]
        [ProducesResponseType(typeof(ListResponse<User>),(int)HttpStatusCode.OK)]
        public IActionResult Login(LoginModel model)
        {
            User user = _repository.Login(model);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        [Route("Register")]
        [ProducesResponseType(typeof(ListResponse<User>), (int)HttpStatusCode.OK)]
        public IActionResult Register(RegisterModel model)
        {
            User user = _repository.Register(model);
            if (user == null)
            {
                return BadRequest();
            }

            return Ok(user);
        }
    }
}
