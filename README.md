# ElectronicsStore – ASP.NET Core MVC E‑Commerce Application

ElectronicsStore is a full‑stack e‑commerce web application built with ASP.NET Core MVC, Entity Framework Core, and SQL Server. It provides a complete shopping experience for electronics products, including catalog browsing, cart management, checkout, order tracking, wishlist, and product reviews.

---

## Features

- User registration, login, and logout using ASP.NET Core Identity  
- Product catalog with categories, product details, images, and stock handling  
- Shopping cart with add, update quantity, and remove item functionality  
- Checkout flow with shipping details and payment method selection  
- Order creation, order confirmation page, and order history (“My Orders”)  
- Wishlist management for saving favourite products  
- Product reviews and ratings from authenticated users  
- Basic admin capabilities for managing products, categories, and orders  

---

## Technology Stack

- **Framework:** ASP.NET Core MVC  
- **Language:** C#  
- **Data Access:** Entity Framework Core (Code‑First)  
- **Authentication:** ASP.NET Core Identity  
- **Database:** SQL Server / Azure SQL Database  
- **Frontend:** Razor Views, Bootstrap 5, CSS, JavaScript  
- **IDE:** Visual Studio 2022  

---

## Project Structure (Overview)

- `Controllers/` – MVC controllers (Home, Product, Cart, Order, Wishlist, Account, etc.)  
- `Models/` – Domain models (Product, Category, Cart, CartItem, Order, OrderItem, Review, Wishlist) and view models (e.g. `CheckoutViewModel`, `LoginViewModel`, `RegisterViewModel`)  
- `Data/` – `ApplicationDbContext` and EF Core configuration  
- `Views/` – Razor views for each feature area plus shared layout and partial views  
- `wwwroot/` – Static assets (CSS, JS, images, Bootstrap)  

---

## Getting Started

### Prerequisites

- Visual Studio 2022  
- .NET SDK (version matching the project)  
- SQL Server (LocalDB, Express, or full edition)  
- Git (if cloning from the repository)  

### Setup

1. **Clone the repository:**

2. **Configure the connection string** in `appsettings.json`:
"ConnectionStrings": {
"DefaultConnection": "Server=.;Database=ElectronicsStore;Trusted_Connection=True;MultipleActiveResultSets=true"
}

3. **Apply migrations / create the database** (if migrations are included):
dotnet ef database update


4. **Run the application**  
- Using Visual Studio: set the web project as startup and press **F5**.  
- Using CLI:
  ```
  dotnet run
  ```

5. Open the browser at the URL shown in the console (typically `https://localhost:5001`).

---

## Deployment

The application can be deployed to Azure App Service with an Azure SQL Database:

- Publish the web project from Visual Studio to an Azure App Service.  
- Deploy the database schema and data to Azure SQL (for example using EF Core migrations or a DACPAC).  
- Update the production connection string in the application configuration on Azure.

---

## Contributing

Contributions, suggestions, and bug reports are welcome. To contribute:

1. Fork the repository.  
2. Create a feature branch (`git checkout -b feature/your-feature`).  
3. Commit your changes with clear messages.  
4. Push the branch and open a pull request.

---

## License

This project is intended for learning and portfolio purposes.  
Add a license file (e.g. MIT, Apache 2.0) if you plan to open‑source the project formally.

---

## Author

**Rahul Singhariya**  
B.Sc. IT – ASP.NET Core & Web Development  
