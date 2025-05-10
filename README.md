# 🚗 SwiftWheels - ASP.NET Core Vehicle Rental System

SwiftWheels is a full-featured vehicle rental web application built with **ASP.NET Core**. It allows users to browse, rent, and manage vehicles, while giving admins the tools to oversee rentals and users.

---

## 🔧 Features

- 🧾 User registration, login, and profile management
- 📅 Smart rental date selection with dynamic total price calculation
- 🚫 Disabled unavailable dates (already-rented) via datepicker
- 🛒 Shopping cart system for managing selected rentals
- 📊 Admin dashboard with:
  - Vehicle list management
  - Rental history viewing
  - Full user list with edit capability
- 🎨 Responsive UI built with Razor Pages and Bootstrap
- 📂 Entity Framework Core integration for database management

---

## 🛠️ Technologies

- ASP.NET Core 7 / Razor Pages
- Entity Framework Core (Code-First)
- Bootstrap 5
- jQuery & datetimepicker plugin

---

## 🚀 Getting Started

### Prerequisites

- [.NET SDK 7.0+](https://dotnet.microsoft.com/download)
- SQL Server (LocalDB or full SQL Server instance)
- Visual Studio 2022+ (recommended)

### Setup Steps

1. **Clone the repository:**
   ```bash
   git clone https://github.com/yourusername/swiftwheels.git
   cd swiftwheels
Set up the database:
In the appsettings.json, configure your connection string:

"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=SwiftWheelsDB;Trusted_Connection=True;"
}
Apply migrations and create the database:
dotnet ef database update

Run the application:
dotnet run

Open your browser and go to:
https://localhost:5001

📁 Folder Structure
graphql
Copy
Edit
SwiftWheels/
│
├── Models/             # Entity classes (Vehicle, Rental, User, etc.)
├── Pages/              # Razor Pages (UI + logic)
│   ├── Admin/          # Admin pages (vehicles, rentals, users)
│   ├── Rentals/        # Rental booking pages
│   └── Shared/         # Layouts and shared components
├── Data/               # DbContext and seed data
├── wwwroot/            # Static assets (CSS, JS, images)
├── appsettings.json    # Configuration settings
└── Program.cs          # App entry point
🧑‍💻 Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

📄 License
This project is licensed under the MIT License.


Let me know if you'd like me to include screenshots or a demo GIF section.






