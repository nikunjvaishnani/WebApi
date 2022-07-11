using System;
using System.Collections.Generic;

#nullable disable

namespace BookStore.Models.ViewModels
{
    public partial class Category
    {
        public Category()
        {
            Books = new HashSet<Book>();
        }

        public int CategoryId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
