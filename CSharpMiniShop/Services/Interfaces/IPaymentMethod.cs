using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpMiniShop.Services.Interfaces
{
    public interface IPaymentMethod
    {
        Task<bool> PayAsync(double amount, Action<int> progressCallback);
    }
}
