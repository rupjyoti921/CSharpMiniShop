using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpMiniShop.Models;
namespace CSharpMiniShop.Services.Interfaces
{
    public interface ICartService
    {
        Task<bool> AddProduct(Product product, int quantity);
        void RemoveProduct(int productId);
        Task<List<CartItem>> GetCartItems();
        Task<double> GetTotalAmount();
        void ClearCart();
    }
}
