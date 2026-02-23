# DevTrack

DevTrack is a web application for tracking software projects, skills, and development progress over time.

This repository contains the current implementation of the project using **ASP.NET Core MVC (.NET 10)**. The app currently includes a polished UI shell and foundational pages, and is being expanded in upcoming milestones to include full data persistence, authentication, role-based access, and API integration.

## Why this project exists

Many students and early-career developers split project history across notes, repositories, and folders. DevTrack aims to centralize that information in one system where users can:

- Organize projects
- Track skills used in each project
- Monitor progress over time
- Prepare cleaner portfolio narratives

## Current Features

- ASP.NET Core MVC project scaffolded with .NET 10
- Multi-page application shell with:
  - Overview (landing page)
  - Dashboard
  - Projects
  - Admin
  - Privacy
- Shared layout/navigation architecture
- Responsive, GitHub-inspired UI styling
- Dark/Light mode toggle with persisted preference
- Smooth front-end transitions and basic animation effects
- Code comments and placeholders for upcoming implementation phases

## Tech Stack

- .NET 10
- ASP.NET Core MVC
- Razor Views
- CSS (custom, responsive)
- Vanilla JavaScript (theme and interaction behavior)

## Project Structure

```text
DevTrack/
├── DevTrack.slnx
├── DevTrack.Web/
│   ├── Controllers/
│   ├── Models/
│   ├── Views/
│   ├── wwwroot/
│   ├── Program.cs
│   └── DevTrack.Web.csproj
├── README.md
└── PROJECT_STATUS.md
```

## Getting Started (Local Development)

1. Ensure .NET 10 SDK is installed.
2. From the repository root, run:

```bash
dotnet restore DevTrack.slnx
dotnet build DevTrack.slnx
dotnet run --project DevTrack.Web/DevTrack.Web.csproj
```

3. Open the local URL printed in the terminal (for example, `http://localhost:5092`).

## Development Roadmap

Planned additions include:

- Entity Framework Core data layer
- Authentication and authorization (Student/Admin roles)
- Full CRUD operations for projects and skills
- GitHub API integration for repository metadata
- Production deployment for final submission

Progress tracking is maintained in `PROJECT_STATUS.md`.