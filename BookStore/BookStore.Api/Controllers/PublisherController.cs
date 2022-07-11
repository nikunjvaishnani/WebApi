using BookStore.Models.Models;
using BookStore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using PublisherStore.Repository;
using System.Linq;
using System.Net;

namespace PublisherStore.Api.Controllers
{
    [ApiController]
    [Route("api/Publisher")]
    public class PublisherController : Controller
    {
        PublisherRepository _repository = new PublisherRepository();

        [HttpPost]
        [Route("add")]
        [ProducesResponseType(typeof(PublisherModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult AddPublisher(PublisherModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            Publisher Publisher = new Publisher()
            {
                PublisherId = model.PublisherId,
                Name = model.Name,
                Address = model.Address,
                Contact = model.Contact,
            };

            Publisher = _repository.AddPublisher(Publisher);
            PublisherModel PublisherModel = new PublisherModel(Publisher);
            return Ok(PublisherModel);
        }

        [HttpGet]
        [Route("GetPublishers")]
        [ProducesResponseType(typeof(PublisherModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult GetPublishers(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            var Publishers = _repository.GetPublishers(pageIndex, pageSize, keyword);

            ListResponse<PublisherModel> listResponse = new ListResponse<PublisherModel>()
            {
                Results = Publishers.Results.Select(c => new PublisherModel(c)),
                TotalRecords = Publishers.TotalRecords,
            };

            return Ok(listResponse);
        }

        [HttpPut]
        [Route("update")]
        [ProducesResponseType(typeof(PublisherModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult UpdatePublisher(PublisherModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            Publisher Publisher = new Publisher()
            {
                PublisherId = model.PublisherId,
                Name = model.Name,  
                Address = model.Address,    
                Contact = model.Contact,
            };

            Publisher = _repository.UpdatePublisher(Publisher);
            PublisherModel PublisherModel = new PublisherModel(Publisher);
            return Ok(PublisherModel);
        }

        [HttpDelete]
        [Route("delete")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult DeletePublisher(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            bool isDeleted = _repository.DeletePublisher(id);
            return Ok(isDeleted);
        }
    }
}

