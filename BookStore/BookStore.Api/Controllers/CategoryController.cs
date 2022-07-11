using BookStore.Models.Models;
using BookStore.Models.ViewModels;
using BookStore.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;

namespace BookStore.Api.Controllers
{
    [ApiController]
    [Route("api/Category")]
    public class CategoryController : Controller
    {
        private readonly CategoryRepository _repository = new CategoryRepository();


        [HttpPost]
        [Route("add")]
        [ProducesResponseType(typeof(CategoryModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult AddCategory(CategoryModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            Category category = new Category()
            {
                CategoryId = model.CategoryId,
                Name = model.Name,
            };

            category = _repository.AddCategory(category);
            BookModel bookModel = new BookModel(category);
            return Ok(bookModel);
        }

        [HttpGet]
        [Route("GetGategories")]
        [ProducesResponseType(typeof(ListResponse<CategoryModel>), (int)HttpStatusCode.OK)]
        public IActionResult GetUsers(int pageIndex = 1, int pageSize = 10, string keyword = "")
        {
            var response = _repository.GetCategories(pageIndex, pageSize, keyword);
            ListResponse<CategoryModel> categories = new ListResponse<CategoryModel>()
            {
                Results = response.Results.Select(u => new CategoryModel(u)),
                TotalRecords = response.TotalRecords,
            };

            return Ok(categories);
        }

        [Route("{id}")]
        [HttpGet]
        [ProducesResponseType(typeof(CategoryModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult),(int)HttpStatusCode.BadRequest)]
        public IActionResult GetCategory(int id)
        {
            var category = _repository.GetCategory(id);

            if(category == null)
                return BadRequest();

            CategoryModel categoryModel = new CategoryModel(category);

            return Ok(categoryModel);
        }

        [HttpPut]
        [Route("update")]
        [ProducesResponseType(typeof(CategoryModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult UpdateCategory(CategoryModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            Category category = new Category()
            {
                CategoryId = model.CategoryId,
                Name = model.Name,  
            };

            category = _repository.UpdateCategory(category);
            CategoryModel categoryModel = new CategoryModel(category);
            return Ok(categoryModel);
        }

        [HttpDelete]
        [Route("delete")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult DeleteCategory(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }
            bool isDeleted = _repository.DeleteCategory(id);
            return Ok(isDeleted);
        }
    }
}
