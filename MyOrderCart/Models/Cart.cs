using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyOrderCart.Models
{
    public class Cart 
    {
        public List<CartProduct> Items { get; set; } = new List<CartProduct>();

        public Decimal Total
        {
            get
            {
                decimal total = (decimal)0.0;
                foreach (var item in Items)
                {
                    total += item.Total;
                }
                return total;
            }
        }
        public DateTime LastAccessed { get; set; }
        public int TimeToLiveInSeconds { get; set; } = 30; // default
    }
}
