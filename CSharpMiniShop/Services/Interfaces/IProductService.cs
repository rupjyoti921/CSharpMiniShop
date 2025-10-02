using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpMiniShop.Models;

namespace CSharpMiniShop.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>> GetALLProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task<List<Product>> SearchProductAsync(string keyword);
        Task<List<Product>> FilterByCategory(string category);
    }
}
