using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpMiniShop.Models
{
    public class Order
    {
        public long orderId {  get; set; }
        public List<CartItem> cartItems { get; set; }
        public double totalAmount { get; set; }
        public string orderStatus {  get; set; }
    }
}
