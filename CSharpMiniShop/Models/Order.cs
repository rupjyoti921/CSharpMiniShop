using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpMiniShop.Models
{
    public class Order
    {
        public int OrderId {  get; set; }
        public List<CartItem> CartItems { get; set; }
        public double TotalAmount { get; set; }
        public string OrderStatus {  get; set; }
    }
}
