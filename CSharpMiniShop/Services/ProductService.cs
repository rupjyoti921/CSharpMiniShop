using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CSharpMiniShop.Models;
using CSharpMiniShop.Services.Interfaces;

namespace CSharpMiniShop.Services
{
    public class ProductService : IProductService
    {
        public readonly HttpClient _httpClient;

        //Injecting HttpClient
        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Product>> GetALLProductsAsync()
        {
            string url = "https://fakestoreapi.com/products";
            try
            {
                //send GET request
                HttpResponseMessage response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode(); // Throws if status is not 200-299

                //Read Json
                string data = await response.Content.ReadAsStringAsync();
                List<Product> products = JsonSerializer.Deserialize<List<Product>>(data);
                return products ?? new List<Product>();
            }
            catch (HttpRequestException ex) 
            {
                throw new Exception("Error fetching products from API.", ex);
            }
            catch(JsonException ex) 
            {
                throw new Exception("Error parsing product data.", ex);
            }

        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            string url = "https://fakestoreapi.com/products";
            try
            {
                //send GET request
                HttpResponseMessage response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode(); // Throws if status is not 200-299

                //Read Json
                string data = await response.Content.ReadAsStringAsync();
                List<Product> products = JsonSerializer.Deserialize<List<Product>>(data);
                Product product = products.Where(x=>x.id == id).FirstOrDefault();
                return product ?? new Product();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Error fetching products from API.", ex);
            }
            catch (JsonException ex)
            {
                throw new Exception("Error parsing product data.", ex);
            }
        }

        public async Task<List<string>> GetCategories()
        {
            string url = "https://fakestoreapi.com/products";

            //send GET request
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode(); // Throws if status is not 200-299

            //Read Json
            string data = await response.Content.ReadAsStringAsync();
            List<Product> products = JsonSerializer.Deserialize<List<Product>>(data);
            List<string> categories = products.Select(x=>x.category).Distinct().ToList();
            return categories ?? new List<string> { "No Category Found" };
        }
        public async Task<List<Product>> FilterByCategory(string category)
        {
            string url = "https://fakestoreapi.com/products";

            //send GET request
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode(); // Throws if status is not 200-299

            //Read Json
            string data = await response.Content.ReadAsStringAsync();
            List<Product> products = JsonSerializer.Deserialize<List<Product>>(data);
            List<Product> filterProducts = products.Where(x => x.category==category).ToList();
            return filterProducts;
        }
        public async Task<List<Product>> SearchProductAsync(string keyword)
        {
            string url = "https://fakestoreapi.com/products";

            //send GET request
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode(); // Throws if status is not 200-299

            //Read Json
            string data = await response.Content.ReadAsStringAsync();
            List<Product> products = JsonSerializer.Deserialize<List<Product>>(data);
            List<Product> result = products
                    .Where(x => x.title.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                                x.description.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                                x.category.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            return result;
        }
    }
}
