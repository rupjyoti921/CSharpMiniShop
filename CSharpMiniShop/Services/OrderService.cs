using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpMiniShop.Models;
using CSharpMiniShop.Services.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CSharpMiniShop.Services
{
    public class OrderService : IOrderService
    {
        public event Action<Order> OrderPaymentCompleted; // Event triggered after payment
        public Order CreateOrder(List<CartItem> cartItems, double toatlAmount)
        {
            Random random = new Random();
            long number = random.NextInt64(1000000, 10000000);
            Order order = new Order() 
            {
                cartItems = cartItems,
                totalAmount = toatlAmount,
                orderStatus = "Pending",
                orderId = number
            };
            return order;

        }

        public void CompleteOrderPayment(Order order)
        { 
            order.orderStatus= "Success";
            OrderPaymentCompleted?.Invoke(order);
        }
    }
}
