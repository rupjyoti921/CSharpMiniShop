using System.ComponentModel;
using System.ComponentModel.Design;
using System.Threading.Tasks;
using CSharpMiniShop.Models;
using CSharpMiniShop.Services;
using CSharpMiniShop.Services.Interfaces;

public class Program
{
    //creating instances for the services
    public static ProductService ps = new ProductService(new HttpClient());
    public static CartService cs = new CartService(ps);
    public static OrderService os = new OrderService();
    public static async Task Main()
    {

        while (true)
        {
            Console.WriteLine("-----------------Main Menue---------------\n1. View All Products\n2. Seach Product\n3. Filter Products By Category\n4. View Cart\n5. Checkout\n6. Exit\n------------------------------------------\nPlease Select One Option..");
            var option = Console.ReadLine();
            if (option == "6")
            {
                ClearScreen();
                Console.WriteLine("\n--------Thank you--------\n-----Please Visit Again-----\n\n");
                break;
            }
            switch (option)
            {
                case "1":
                    {
                        await ViewAllProducts();
                    }
                    break;
                case "2":
                    {
                        await SeachForProduct();
                    }
                    break;
                case "3":
                    {
                        await FilterProductsByCategory();
                    }
                    break;
                case "4":
                    {
                        await ViewCart();
                    }
                    break;
                case "5":
                    {
                        await GoForPayment();
                    }
                    break;
                default:
                    Console.WriteLine("\n------------Please Enter a Valid Option------------\n\n");
                    Console.ReadKey();
                    ClearScreen();
                    break;
            }
        }
    }


    public static void ClearScreen()
    {
        Console.Clear();
        Console.SetBufferSize(Console.BufferWidth, Console.WindowHeight);
    }

    public static async Task ViewAllProducts()
    {
        ClearScreen();
        Console.WriteLine("Loading Products..");
        List<Product> products;
        try
        {
            products = await ps.GetALLProductsAsync();
        }
        catch (Exception ex) 
        {
            Console.WriteLine($"\n----Something went wrong: {ex.Message}----\n");
            Console.ReadKey();
            ClearScreen();
            return;
        }

        ClearScreen();
        await ShowProducts(products);
    }

    public static async Task SeachForProduct()
    {
        ClearScreen();
        Console.WriteLine("\nEnter the Keyword to search:");
        string keyWord = Console.ReadLine();
        Console.WriteLine("Searching Products..");
        List<Product> products = await ps.SearchProductAsync(keyWord);
        ClearScreen();
        if (products.Count > 0) { await ShowProducts(products); } 
        else
        {
            Console.WriteLine("\nNo Products Found!\n");
            Console.ReadKey();
            ClearScreen() ;
            return;
        }

    }

    public static async Task FilterProductsByCategory()
    {
        ClearScreen();
        Console.WriteLine("\nBelow are the Category(s) available\n");
        List<string> categories = await ps.GetCategories();
        for(int i=1;i<=categories.Count;i++)
        {
            Console.WriteLine($"{i}. {categories[i-1]}");
        }
        Console.WriteLine("-------------------------------------\nSelect one :");
        int cate = int.Parse(Console.ReadLine());
        string category = categories[cate-1];
        Console.WriteLine($"\nFilterig Products based on Category : {category}..");
        List<Product> products = await ps.FilterByCategory(category);
        ClearScreen();
        if (products.Count > 0) { await ShowProducts(products); }
        else
        {
            Console.WriteLine("\nNo Products Found!\n");
            Console.ReadKey();
            ClearScreen();
            return;
        }
    }

    public static async Task ShowProducts(List<Product> products)
    {
        int count = 1;
        foreach (var product in products)
        {
            Console.WriteLine($"ID : {count}\nTitle : {product.title}\nPrice : {product.price}\nDescription : {product.description}\nCategory : {product.category}\nRating : {product.rating.rate}\n--------------------------------------------------------------------------------------\n");
            count++;
        }
        Console.WriteLine("\n----To add on Cart Press (Y) to Exit (X)----\n");
        string choice = Console.ReadLine();
        if (choice == "Y" || choice == "y")
        {
            Console.WriteLine("To add on Cart, Please select ID :");
            int proID = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter Total Number of Quantity :");
            int proQuan = int.Parse(Console.ReadLine());
            Console.WriteLine("\nAdding to the Cart...");
            bool IsSuccess = await cs.AddProduct(products[proID - 1], proQuan);
            if (IsSuccess)
            {
                Console.WriteLine("*****Status:: Product Successfully Added*****\n\n");
            }
            else
            {
                Console.WriteLine("\n*****Status:: Product Failed to Add!*****\n\n");
            }
        }
        else
        {
            Console.WriteLine("\n------------Thank you, Please Visit Again------------\n\n");
            Console.ReadKey();
            ClearScreen();
            return;
        }
    }   

    public static async Task ViewCart()
    {
        var items = await cs.GetCartItems();
        if (items.Count == 0)
        {
            ClearScreen();
            Console.WriteLine("\n****Product not added to the Cart! Please add a product to Shop.****\n");
            return;
        }
        ClearScreen();
        Console.WriteLine("\n\n-------------Showing Cart Items-------------\n");
        foreach (var item in items)
        {
            Console.WriteLine($"ID : {item.product.id}\nTitle : {item.product.title} \nPrice :  {item.product.price}\nDescription : {item.product.description}\nCategory : {item.product.category}\nRating : {item.product.rating.rate}\nTotal Quantity : {item.quantity}\n--------------------------------------------------------------------------------------\n");
        }
        Console.WriteLine("\n");
    }

    public static async Task GoForPayment()
    {
        if (await ItemAvailableInCart())
        {
            ClearScreen();
            //Showing Cart product(s) before Payment
            ViewCart();
            //Calculate Toatl amount
            Console.WriteLine("Calculatin Total Amount...\n");

            Task<double> totalAmount = cs.GetTotalAmount();


            //Offering Payment Mode options
            Console.WriteLine("\n---------Please select a Payment Mode--------\n1. Credit Card\n2. UPI\n3. Cash\n4. Cancel Payment\n----------------------------------------------");

            Console.WriteLine("Total Amount : ");
            double tAmount = await totalAmount;
            Console.WriteLine($"{tAmount}/-\nEnter Payment Mode :");

            //Take Payment mode from user
            int mode = int.Parse(Console.ReadLine());

            //creating Payment method and injecting in paymentservice
            IPaymentMethod method;
            IPaymentService payService;
            if (mode == 1)
            {
                method = new CreditCardPayment();
                payService = new PaymentService(method, os, cs);
            }
            else if (mode == 2)
            {
                method = new UPIPayment();
                payService = new PaymentService(method, os, cs);
            }
            else if (mode == 3)
            {
                method = new CashPayment();
                payService = new PaymentService(method, os, cs);
            }
            else if (mode == 4)
            {
                Console.WriteLine("\n******Payment Canceled******\n");
                return;
            }
            else
            {
                Console.WriteLine("\n------------Please Enter a Valid Option------------\n\n");
                Console.ReadKey();
                ClearScreen();
                return;
            }

            //Event Listern for Paymetn Success
            os.OrderPaymentCompleted += (order) =>
            {
                Console.WriteLine($"\n\n\nStatus : {order.orderStatus}\nOrder Number : {order.orderId}\nOrder Date : {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}\nCart has been cleared after successful payment\n\n" +
                    "---------------------------- Thanks for The Order, Please Visit Again---------------------------------\n\n\n\n\n");
                Console.ReadKey();
                ClearScreen();
            };


            List<CartItem> items = await cs.GetCartItems();
            //Create Order
            Order order = os.CreateOrder(items, tAmount);

            //Perform Payment
            Console.WriteLine("\n");
            Action<int> progressCallback = percent =>
            {
                Console.Write($"\rProcessing Payment: {percent}%");
            };
            bool paymentStatus = await payService.ProcessPayment(order, progressCallback);
        }
        else
        {
            Console.WriteLine("\n****You Havn't Added Anything yet, Please add to Cart First before Payment****\n\n");
            Console.ReadKey();
            ClearScreen();
            return;
        }
    }

    public static async Task<bool> ItemAvailableInCart()
    {
        List<CartItem> cItems = await cs.GetCartItems();
        if (cItems.Count == 0) return false; else return true;
    }
}