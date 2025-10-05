# 🛒 CSharpMiniShop

**CSharpMiniShop** is a console-based mini e-commerce application developed in **C# (.NET 6)**.  
It demonstrates the use of core C# concepts such as **OOP**, **Dependency Injection (DI)**, **asynchronous programming**, **multithreading**, **delegates and events**, and **exception handling** — all integrated into a functional shopping simulation.

---

## 📦 Features

- Fetch real-time product data from the [FakeStore API](https://fakestoreapi.com/).
- Browse and search products by keyword.
- Filter products by category.
- Add or remove items from the cart.
- Calculate total amount using **multithreading**.
- Simulate payments via **Cash**, **UPI**, or **Credit Card**.
- Show payment progress using **callback functions**.
- Trigger **events** after successful payments to update order status and clear the cart.
- Implemented exception handling and structured console feedback.

---

## 🧱 Project Structure

CSharpMiniShop
│
├── Models
│ ├── Product.cs
│ ├── Rating.cs
│ ├── CartItem.cs
│ └── Order.cs
│
├── Services
│ ├── Interfaces
│ │ ├── ICartService.cs
│ │ ├── IOrderService.cs
│ │ ├── IPaymentMethod.cs
│ │ ├── IPaymentService.cs
│ │ └── IProductService.cs
│ ├── CartService.cs
│ ├── ProductService.cs
│ ├── OrderService.cs
│ ├── PaymentService.cs
│ ├── CashPayment.cs
│ ├── UPIPayment.cs
│ └── CreditCardPayment.cs
│
└── Program.cs

## 🧠 Key C# Concepts Implemented

### Object-Oriented Programming (OOP)
Encapsulation, inheritance, and abstraction are applied across services and models for clean, modular, and maintainable code.

### Dependency Injection (DI)
`PaymentService` receives `IPaymentMethod`, `OrderService`, and `CartService` instances via constructor injection to promote **loose coupling** and **testability**.

### Asynchronous Programming
`async/await` is used for:
- Fetching data from the FakeStore API.
- Payment processing simulation.
This prevents blocking the main thread during long-running operations.

### Multithreading
Implemented in `CartService` to calculate the total amount concurrently using `Task.Run`, improving responsiveness.

### Delegates & Callbacks
Used in payment progress tracking:
```csharp
Action<int> progressCallback = percent => 
    Console.Write($"\rPayment Progress: {percent}%");
This provides live feedback during the simulated payment process.

Events

The OrderService defines an event that is triggered after successful payment:
os.OrderPaymentCompleted += (order) => {
    Console.WriteLine($"\nOrder {order.OrderId} placed successfully!");
};
This updates order status and clears the cart automatically.

Exception Handling

Used across services (especially in cart operations and API calls) to ensure stability and provide user-friendly messages.

⚙️ How It Works

Products are fetched from FakeStore API using ProductService.

User can search, filter, and select products to add to the cart.

Total amount is calculated asynchronously with simulated processing delays.

User selects a payment method (Cash, UPI, or Credit Card).

Payment is processed with progress feedback via callback.

On success, an event triggers to update order status and clear the cart.

The app provides console feedback throughout the entire flow.

🧩 Technologies Used

Language: C#

Framework: .NET 8

API: FakeStore API

Concepts: OOP, DI, Events, Delegates, Multithreading, Async/Await, Exception Handling

📜 Author

Rupjyoti  — Software Developer | C# | .NET | Python
📧 [rupjyoti921@gmail.com]