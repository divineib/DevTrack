# <div align="center">DevTrack</div>

<div align="center">
  Student developer portfolio and project tracking system built with ASP.NET Core MVC.
</div>

<p align="center">
  <a href="https://github.com/divineib/DevTrack"><img src="https://img.shields.io/badge/.NET-10-512BD4?logo=dotnet&logoColor=fff" alt=".NET 10" /></a>
  <a href="https://github.com/divineib/DevTrack"><img src="https://img.shields.io/badge/ASP.NET_Core-MVC-5C2D91?logo=dotnet&logoColor=fff" alt="ASP.NET Core MVC" /></a>
  <a href="https://github.com/divineib/DevTrack"><img src="https://img.shields.io/badge/Status-Complete-1a7f37" alt="Status" /></a>
  <a href="https://github.com/divineib/DevTrack"><img src="https://custom-icon-badges.demolab.com/badge/C%23-%23239120.svg?logo=cshrp&logoColor=white" alt="C#" /></a>
  <a href="https://github.com/divineib/DevTrack"><img src="https://img.shields.io/badge/Razor-512BD4?logo=dotnet&logoColor=fff" alt="Razor" /></a>
  <a href="https://github.com/divineib/DevTrack"><img src="https://img.shields.io/badge/SQLite-003B57?logo=sqlite&logoColor=fff" alt="SQLite" /></a>
  <a href="https://github.com/divineib/DevTrack"><img src="https://img.shields.io/badge/CSS-639?logo=css&logoColor=fff" alt="CSS" /></a>
  <a href="https://github.com/divineib/DevTrack"><img src="https://img.shields.io/badge/JavaScript-F7DF1E?logo=javascript&logoColor=000" alt="JavaScript" /></a>
</p>

---

## Overview

DevTrack is a web application that helps software engineering students track coursework projects, tag technical skills, sync GitHub repository metadata, and present portfolio progress in a clean, role-based workspace. Students manage their own projects while administrators review submissions across all users.

## Features

- Full CRUD for projects with category and skill tagging
- GitHub API integration for automatic repository metadata sync
- Role-based access control (Student and Admin roles)
- Student dashboard with live project metrics and activity feed
- Admin review center with moderation feed and KPI cards
- Responsive design with mobile hamburger navigation
- Dark/light theme toggle with saved preference
- Client-side and server-side form validation
- Database seeding with demo data for local exploration

## Tech Stack

| Layer | Technology | Rationale |
|-------|-----------|-----------|
| **Backend** | ASP.NET Core MVC (.NET 10) | Server-side MVC with Razor views, minimal hosting model |
| **Language** | C# | Strongly typed, first-class .NET support |
| **Views** | Razor (`.cshtml`) | HTML + C# templating with tag helpers and partials |
| **Styling** | Custom CSS (no Tailwind) | CSS variables, DM Sans + Source Serif 4, warm palette — avoids generic “AI dashboard” chrome |
| **Client Scripts** | Vanilla JavaScript | Theme toggle, mobile nav, scroll animations — no framework overhead |
| **Database** | SQLite | Lightweight, zero-config relational database suitable for local dev |
| **ORM** | Entity Framework Core | Code-first migrations, LINQ queries, relationship configuration |
| **Auth** | ASP.NET Core Identity | Cookie-based authentication, role management (Student/Admin) |
| **Validation** | jQuery Validate + Unobtrusive | Client-side form validation paired with server-side model annotations |
| **External API** | GitHub REST API v3 | Repository metadata sync (name, description, last updated) |

## External Integrations

| Service | Purpose |
|---------|---------|
| **GitHub API** | Fetches repository metadata when a user links a GitHub URL to a project |
| **ASP.NET Core Identity** | User registration, login/logout, password hashing, and roles (Student/Admin) |

## Project Structure

```text
DevTrack/
├── DevTrack.slnx                 # Solution file
├── dotnet-tools.json             # dotnet-ef tool manifest (for migrations)
├── DevTrack.Web/
│   ├── Program.cs
│   ├── DevTrack.Web.csproj
│   ├── appsettings.json
│   ├── Controllers/              # Home, Account, Projects
│   ├── Models/                   # Domain entities
│   ├── ViewModels/               # Form and list view models
│   ├── Views/                    # Razor pages
│   ├── Data/                     # DbContext + seed data
│   ├── Services/                 # GitHub API client
│   ├── Migrations/               # EF Core migrations
│   └── wwwroot/                  # CSS, JS, lib
```

## Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)

### Run locally

```bash
git clone https://github.com/divineib/DevTrack.git
cd DevTrack/DevTrack.Web
dotnet restore
dotnet run
```

Open **`http://localhost:5092`** in your browser (default HTTP profile; use **http**, not https, unless you run with the `https` launch profile and trust the dev certificate).

On first run, the app applies migrations and seeds roles, categories, skills, demo users, sample projects, and reviews.

### Demo accounts (after seed)

Password for both: **`DevTrack1`**

| Email | Role |
|-------|------|
| `student@devtrack.local` | Student |
| `admin@devtrack.local` | Admin |

You can also register a new account; new users receive the **Student** role by default.

### EF Core migrations (optional)

If you change models, install the local tool and add a migration:

```bash
dotnet tool restore
dotnet ef migrations add YourMigrationName --project DevTrack.Web
```

---

## License

This project was created for coursework. Use and modify as you like.
