using System;
using System.Collections.Generic;

#nullable disable

namespace BookStore.Models.ViewModels
{
    public partial class Book
    {
        public Book()
        {
            Carts = new HashSet<Cart>();
            Orderdtls = new HashSet<Orderdtl>();
        }

        public int BookId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public int PublisherId { get; set; }
        public int Quantity { get; set; }

        public virtual Category Category { get; set; }
        public virtual Publisher Publisher { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<Orderdtl> Orderdtls { get; set; }
    }
}
