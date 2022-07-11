using System;
using System.Collections.Generic;

#nullable disable

namespace BookStore.Models.ViewModels
{
    public partial class Orderdtl
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public int Totalprice { get; set; }
        public int OrdermstId { get; set; }
        public int Price { get; set; }

        public virtual Book Book { get; set; }
        public virtual Ordermst Ordermst { get; set; }
    }
}
