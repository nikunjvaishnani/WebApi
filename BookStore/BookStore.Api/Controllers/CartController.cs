using BookStore.Models.Models;
using BookStore.Models.ViewModels;
using BookStore.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace BookStore.Api.Controllers
{
    [ApiController]
    [Route("Cart")]
    public class CartController : Controller
    {
        CartRepository _repository = new CartRepository();

        [HttpGet]
        [Route("GetCartItem")]
        [ProducesResponseType(typeof(IEnumerable<CartModel>),(int)HttpStatusCode.OK)]
        public IActionResult GetCartItem(string keyword)
        {
            List<Cart> carts = _repository.GetCartItem(keyword);
            IEnumerable<CartModel> cartModels = carts.Select(c => new CartModel(c));
            return Ok(cartModels);
        }


        [HttpPost]
        [Route("add")]
        [ProducesResponseType(typeof(CartModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult AddCart(CartModel model)
        {
            if(model == null)
            {
                return BadRequest();
            }

            Cart cart = new Cart()
            { 
                CartId = model.CartId,
                BookId = model.BookId,
                UserId = model.UserId,
                Quantity = model.Quantity,
            };

            cart = _repository.AddCart(cart);
            CartModel cartModel = new CartModel(cart);
            return Ok(cartModel);
        }

        [HttpPut]
        [Route("update")]
        [ProducesResponseType(typeof(CartModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult UpdateCart(CartModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            Cart cart = new Cart()
            {
                CartId = model.CartId,
                BookId = model.BookId,
                UserId = model.UserId,
                Quantity = model.Quantity,
            };

            cart = _repository.UpdateCart(cart);
            CartModel cartModel = new CartModel(cart);
            return Ok(cartModel);
        }

        [HttpDelete]
        [Route("delete")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult DeleteCart(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }

            bool isDeleted = _repository.DeleteCart(id);
            return Ok(isDeleted);
        }
    }
}
