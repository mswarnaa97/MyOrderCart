using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyOrderCart.Models
{
    public class CartProduct
    {
        public int Quantity { get; set; }
        public Products Product { get; set; }

        public decimal Total
        {
            get
            {
                return Product.Price * Quantity;
            }
        }
    }
}
