using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpMiniShop.Models;
using CSharpMiniShop.Services.Interfaces;

namespace CSharpMiniShop.Services
{
    public class CartService : ICartService
    {
        private List<CartItem> cartItems;
        readonly IProductService _productService;

        public CartService(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<List<CartItem>> GetCartItems()
        {
            return cartItems?? new List<CartItem>();
        }
        
        //To add the product in the Collection of CartItems
        public async Task<bool> AddProduct(Product product, int quantity)
        {
            Product newProduct;
            try
            {
                newProduct = await _productService.GetProductByIdAsync(product.id);
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"\n----Something went wrong: {ex.Message}----\n");
                Console.ReadKey();
                return false;
            }
            if (newProduct != null) 
            {
                //if CartItem is null, initialize it first
                if (cartItems == null) { cartItems = new List<CartItem>();}
                //check if product already exists, if found increment the quantity.
                var existingProduct = cartItems.FirstOrDefault(x => x.product == product);
                if(existingProduct != null){
                    existingProduct.quantity += 1;
                }
                //else add as a new item in the list
                else {
                    cartItems.Add(new CartItem()
                    {
                        product = product,
                        quantity = quantity
                    });
                }
                return true;

            }
            else { return false; }
            
        }

        public void ClearCart()
        {
            cartItems?.Clear();
        }

        //Calculate the Total Amount
        public async Task<double> GetTotalAmount()
        {
            double baseAmount = 0;
            double totalAmount=0;
            if (cartItems != null)
            {
                foreach (var item in cartItems)
                {
                    baseAmount += Math.Round(item.product.price * item.quantity, 2);
                }
                totalAmount = await Task.Run(async () =>
                {
                    double totalAmount = baseAmount;
                    await Task.Delay(500);
                    totalAmount += (totalAmount / 100) * 5;
                    await Task.Delay(500);
                    totalAmount += (totalAmount / 100) * 2;
                    await Task.Delay(500);
                    return totalAmount;
                });
            }
            return Math.Round(totalAmount,2);
        }

        public void RemoveProduct(int productId)
        {
            var removeItem = cartItems.FirstOrDefault(x=>x.product.id == productId);
            if (removeItem != null)
            {
                cartItems.Remove(removeItem);
                Console.WriteLine($"\nProduct {removeItem.product.title} removed from cart.");
            }
            else
            {
                Console.WriteLine("Product not found in cart.");
            }            
        }
    }
}
