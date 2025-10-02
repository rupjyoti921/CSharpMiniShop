using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpMiniShop.Services.Interfaces;

namespace CSharpMiniShop.Services
{
    public class UPIPayment : IPaymentMethod
    {
        public async Task<bool> PayAsync(double amount, Action<int> progressCallback)
        {
            for (int i = 10; i <= 100; i += 10)
            {
                await Task.Delay(700);
                progressCallback(i);
            }
            return true;
        }
    }
}
