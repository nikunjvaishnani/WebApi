using System;
using System.Collections.Generic;

#nullable disable

namespace BookStore.Models.ViewModels
{
    public partial class Ordermst
    {
        public Ordermst()
        {
            Orderdtls = new HashSet<Orderdtl>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public int Totalprice { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Orderdtl> Orderdtls { get; set; }
    }
}
