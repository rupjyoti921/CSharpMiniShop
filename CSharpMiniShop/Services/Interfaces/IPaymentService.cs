using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpMiniShop.Models;

namespace CSharpMiniShop.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<bool> ProcessPayment(Order order, Action<int> progressCallback);
    }
}
