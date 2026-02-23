# <div align="center">DevTrack</div>

<div align="center">
  Student project and skill tracking system built with ASP.NET Core MVC.
</div>

<div align="center">
  <img src="https://img.shields.io/badge/.NET-10-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt=".NET 10" />
  <img src="https://img.shields.io/badge/ASP.NET_Core-MVC-5C2D91?style=for-the-badge&logo=dotnet&logoColor=white" alt="ASP.NET Core MVC" />
  <img src="https://img.shields.io/badge/Status-Active_Development-1f6feb?style=for-the-badge" alt="Status" />
</div>

<div align="center">
  <img src="https://img.shields.io/badge/Language-C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white" alt="C#" />
  <img src="https://img.shields.io/badge/Language-Razor-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt="Razor" />
  <img src="https://img.shields.io/badge/Language-CSS-1572B6?style=for-the-badge&logo=css3&logoColor=white" alt="CSS" />
  <img src="https://img.shields.io/badge/Language-JavaScript-F7DF1E?style=for-the-badge&logo=javascript&logoColor=black" alt="JavaScript" />
</div>

## Overview

DevTrack is a web application focused on helping students manage software projects, track skill growth, and present progress in a clean, structured way.

The current implementation provides a polished MVC application shell with a modern interface and is set up for the next stages: database integration, authentication/authorization, CRUD workflows, and GitHub API integration.

> [!NOTE]
> This repository currently represents the project shell + interface foundation. Full data-backed features are in active development.

## Table of Contents

- [Overview](#overview)
- [Problem and Goal](#problem-and-goal)
- [Current Implementation](#current-implementation)
- [Tech Stack](#tech-stack)
- [Project Structure](#project-structure)
- [Getting Started](#getting-started)
- [Roadmap](#roadmap)
- [Documentation](#documentation)

## Problem and Goal

Students often track their development work across multiple disconnected tools (notes, repository lists, folders, and task boards). DevTrack consolidates this into one system to make progress visible and easier to manage.

Core goals:

- Centralize project history
- Track skills used in each project
- Show progress over time
- Support role-based workflows for Student and Admin users

## Current Implementation

Implemented in this repository:

- ASP.NET Core MVC shell with `.NET 10`
- Core pages:
  - `Overview`
  - `Dashboard`
  - `Projects`
  - `Admin`
  - `Privacy`
- Shared layout and navigation architecture
- Responsive, GitHub-inspired visual style
- Dark/light mode toggle with saved preference
- Smooth UI interactions and lightweight animations
- In-code placeholders for upcoming core features

## Tech Stack

- .NET 10
- ASP.NET Core MVC
- Razor Views
- Custom CSS (responsive layout)
- Vanilla JavaScript (theme + interactions)

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
├── PROJECT_STATUS.md
└── README.md
```

## Getting Started

### Prerequisites

- .NET 10 SDK

> [!IMPORTANT]
> Use .NET 10 SDK to match the project target framework and avoid compatibility issues.

### Run Locally

```bash
dotnet restore DevTrack.slnx
dotnet build DevTrack.slnx
dotnet run --project DevTrack.Web/DevTrack.Web.csproj
```

Open the local URL shown in the terminal (for example: `http://localhost:5092`).

> [!TIP]
> Keep `dotnet watch run --project DevTrack.Web/DevTrack.Web.csproj` running during development for faster feedback.

## Roadmap

Planned next milestones:

- Add Entity Framework Core data layer
- Add ASP.NET Core Identity (Student/Admin roles)
- Implement full CRUD for projects and skills
- Integrate GitHub API repository sync
- Deploy public production build

## Documentation

- [`PROJECT_STATUS.md`](./PROJECT_STATUS.md) contains the active implementation checklist and phase progress.

> [!WARNING]
> This is an actively evolving academic project. Project structure and feature scope may change between milestone phases.