# 🎫 Help Desk Ticket Management System

A modern full-stack Help Desk application designed to simplify IT support operations. The platform enables employees to create, monitor, update, and manage software, hardware, and network support requests through an intuitive web interface.

---

# 📂 Project Architecture

The application follows a clean multi-layer architecture, separating presentation, business logic, and data access for better maintainability and scalability.

```text
HelpDeskManagement/
│
├── HelpDesk.Api/
│   ├── Controllers/
│   ├── Data/
│   ├── Migrations/
│   ├── Models/
│   ├── Repositories/
│   ├── appsettings.json
│   └── Program.cs
│
├── HelpDesk.Mvc/
│   ├── Controllers/
│   ├── Models/
│   ├── Services/
│   ├── Views/
│   ├── appsettings.json
│   └── Program.cs
│
├── HelpDesk.Tests/
│
├── HelpDeskManagement.sln
├── README.md
└── .gitignore
```

---

# ✨ Highlights

- 📊 Dashboard displaying Total, Open, and Closed ticket statistics.
- 📝 Complete CRUD functionality for support tickets.
- 🔍 Filter tickets based on their current status.
- 🚦 New tickets are automatically assigned an **Open** status.
- ✅ Unit testing implemented using xUnit and Moq for API validation.
- 🏗️ Layered architecture following clean coding principles.
- 📱 Responsive user interface built with Bootstrap.

---

# 🛠 Tech Stack

| Category | Technologies |
|----------|--------------|
| Framework | .NET 10 |
| Backend | ASP.NET Core Web API, C# |
| Frontend | ASP.NET Core MVC, Razor Views, HTML5, CSS3, Bootstrap 5 |
| ORM | Entity Framework Core |
| Database | SQL Server LocalDB |
| Testing | xUnit, Moq |
| Version Control | Git & GitHub |

---

# ⚙ Prerequisites

Before running the application, install:

- Visual Studio 2022 (or later) with the **ASP.NET and Web Development** workload
- .NET 10 SDK
- SQL Server Express or LocalDB

---

# 🚀 Installation & Setup

## Clone the Repository

```bash
git clone https://github.com/your-username/HelpDeskManagement.git
cd HelpDeskManagement
```

## Open the Solution

Launch Visual Studio and open the **HelpDeskManagement.sln** solution file.

---

## Configure the Database

Open **Package Manager Console**, select **HelpDesk.Api** as the default project, and execute:

```powershell
Update-Database
```

This command creates the required SQL Server database and tables using Entity Framework Core migrations.

---

## Launch the Application

To run both projects together:

1. Right-click the solution.
2. Select **Configure Startup Projects**.
3. Choose **Multiple Startup Projects**.
4. Set both **HelpDesk.Api** and **HelpDesk.Mvc** to **Start**.
5. Press **F5**.

> Ensure the API URL matches the BaseAddress configured in the MVC application.

---

# 📡 REST API

| Method | Route | Purpose |
|--------|-------|---------|
| GET | `/api/Ticket/All` | Retrieve all tickets |
| GET | `/api/Ticket/{id}` | Retrieve a ticket by ID |
| POST | `/api/Ticket` | Create a new ticket |
| PUT | `/api/Ticket/{id}` | Update an existing ticket |
| DELETE | `/api/Ticket/{id}` | Delete a ticket |
| GET | `/api/Ticket/Status/{status}` | Retrieve tickets by status |

---

# 📈 Project Features

### Dashboard
- View ticket analytics in real time.
- Monitor Open and Closed ticket counts.

### Ticket Management
- Create support requests.
- Edit ticket details.
- Delete tickets.
- View ticket history.

### Status Tracking
- Open
- In Progress
- Closed

### Search & Filter
- Quickly locate tickets by status.

### Testing
- Comprehensive controller testing using xUnit and Moq.

---

# 📌 Project Benefits

- Clean layered architecture
- Easy to extend and maintain
- RESTful API design
- Responsive Bootstrap interface
- Entity Framework Core integration
- Well-structured codebase
- Unit-tested backend

---

# 👨‍💻 Future Enhancements

- User authentication and authorization
- Email notifications
- Ticket priority analytics
- File attachment support
- Role-based dashboards
- Advanced search functionality

---
