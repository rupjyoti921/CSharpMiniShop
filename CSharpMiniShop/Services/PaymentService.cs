using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpMiniShop.Services.Interfaces;
using CSharpMiniShop.Models;

namespace CSharpMiniShop.Services
{
    public class PaymentService : IPaymentService
    {
        readonly IPaymentMethod _paymentMethod;
        readonly OrderService _orderService;
        readonly CartService _cartService;
        public PaymentService(IPaymentMethod paymentMethod, OrderService os, CartService cs) 
        {
            _paymentMethod = paymentMethod;
            _orderService = os;
            _cartService = cs;
        }

        public async Task<bool> ProcessPayment(Order order, Action<int> progressCallback)
        {
            bool payResult = await _paymentMethod.PayAsync(order.totalAmount,progressCallback);
            if (payResult) 
            {
                // payment success → update order and trigger event
                _orderService.CompleteOrderPayment(order);
                // clear the cart
                _cartService.ClearCart();

            }
            return payResult;
        }
    }
}
