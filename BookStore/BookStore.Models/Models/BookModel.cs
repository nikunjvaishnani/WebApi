﻿using BookStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.Models
{
    public class BookModel
    {
        public BookModel() { }

        public BookModel(Book book)
        {
            BookId = book.BookId;
            Name = book.Name;
            Price = book.Price;
            Description = book.Description;
            CategoryId = book.CategoryId;
            PublisherId = book.PublisherId; 
            Quantity = book.Quantity;
        }

        public int BookId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }  
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public int PublisherId { get; set; }    
        public int Quantity { get; set; }

    }
}
