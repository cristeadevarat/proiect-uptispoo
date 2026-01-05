# proiect-uptispoo

A C# project for UPT ISPOO course.

## Project Setup

This project uses C# and includes a comprehensive .gitignore file for Visual Studio and .NET projects.

## Getting Started

### Prerequisites

- .NET SDK (version 6.0 or later recommended)
- Visual Studio 2022 or Visual Studio Code
- Git

### Building the Project

```bash
# Clone the repository
git clone https://github.com/cristeadevarat/proiect-uptispoo.git
cd proiect-uptispoo

# Build the project (once you have .NET project files)
dotnet build

# Run the project
dotnet run
```

## Collaboration

### Adding Collaborators to the Repository

To give your friends access to this GitHub repository:

1. **Repository Owner**: Go to the repository on GitHub: https://github.com/cristeadevarat/proiect-uptispoo
2. Click on **Settings** (top navigation bar)
3. Click on **Collaborators** (or **Collaborators and teams**) in the left sidebar
4. Click **Add people** button
5. Enter your friend's GitHub username or email
6. Select the appropriate permission level:
   - **Write**: Can read, clone, push, and create pull requests
   - **Maintain**: Write access plus managing issues and PRs
   - **Admin**: Full access including settings
7. Click **Add [username] to this repository**
8. Your friend will receive an email invitation

### For Collaborators

Once you receive an invitation:
1. Check your email for the GitHub invitation
2. Click the link or go to https://github.com/cristeadevarat/proiect-uptispoo
3. Accept the invitation
4. Clone the repository:
   ```bash
   git clone https://github.com/cristeadevarat/proiect-uptispoo.git
   ```

### Working Together - Best Practices

1. **Pull before you start working**:
   ```bash
   git pull origin main
   ```

2. **Create a branch for your work**:
   ```bash
   git checkout -b feature/your-feature-name
   ```

3. **Commit your changes**:
   ```bash
   git add .
   git commit -m "Description of your changes"
   ```

4. **Push your branch**:
   ```bash
   git push origin feature/your-feature-name
   ```

5. **Create a Pull Request** on GitHub to merge your changes into the main branch

## About the .gitignore

This repository includes a comprehensive C# .gitignore file that excludes:
- Build artifacts (bin/, obj/, Debug/, Release/)
- Visual Studio temporary files (.vs/, *.user, *.suo)
- NuGet packages
- Build logs and test results
- User-specific settings

The .gitignore is based on the official Visual Studio template and is suitable for C# projects, not C projects.
