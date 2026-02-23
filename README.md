# DevTrack

DevTrack is an ASP.NET Core MVC web application designed for software engineering students to track projects, skills, and growth over time. This repository currently contains the **Part II shell setup** for the course project, including a modern UI foundation, multiple pages, and project structure prepared for database and API integration in upcoming phases.

## Current Scope (Part II)

- ASP.NET Core MVC project scaffolded with .NET 10
- Solution file and web project configured for development in Visual Studio Code
- Initial pages implemented:
  - Overview (landing page)
  - Dashboard
  - Projects
  - Admin
  - Privacy
- Shared layout and navigation shell
- Responsive, GitHub-inspired UI styling
- Dark/Light theme toggle with persisted user preference
- Smooth UI transitions and basic front-end animation effects
- Inline code comments and placeholders for upcoming Part III/IV features

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

## Run Locally

1. Ensure .NET 10 SDK is installed.
2. From the repository root, run:

```bash
dotnet restore DevTrack.slnx
dotnet build DevTrack.slnx
dotnet run --project DevTrack.Web/DevTrack.Web.csproj
```

3. Open the local URL printed in the terminal (for example, `http://localhost:5092`).

## GitHub Setup

After creating a repository on GitHub, push this code with:

```bash
git branch -M main
git remote add origin https://github.com/<your-username>/<your-repository>.git
git push -u origin main
```

## Next Steps

Planned implementation for Part III and Part IV includes:

- Core models and data access layer (Entity Framework Core)
- Authentication and authorization (Student/Admin roles)
- CRUD features for projects and skills
- GitHub API integration
- Deployment and final demonstration deliverables

For detailed planning and progress tracking, see `PROJECT_STATUS.md`.