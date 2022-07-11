using BookStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.Models
{
    public class CartModel
    {
        public CartModel() { }

        public CartModel(Cart cart)
        {
            CartId  = cart.CartId;
            UserId = cart.UserId;
            BookId = cart.BookId;
            Quantity = cart.Quantity;
        }

        public int CartId { get; set; }
        public int UserId { get; set;}
        public int BookId { get; set; }
        public int Quantity { get; set; }
    }
}
