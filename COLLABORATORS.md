# GitHub Collaboration Guide

This guide explains how to collaborate on this project with your friends using GitHub.

## For Repository Owner: Adding Collaborators

### Step-by-Step Instructions

1. **Navigate to the Repository**
   - Go to https://github.com/cristeadevarat/proiect-uptispoo

2. **Access Settings**
   - Click the **Settings** tab (top right of the repository page)
   - You need to be the repository owner or have admin access

3. **Go to Collaborators**
   - In the left sidebar, click **Collaborators** (or **Collaborators and teams**)
   - You may need to confirm your password

4. **Add a Collaborator**
   - Click the **Add people** button
   - Enter your friend's:
     - GitHub username (e.g., `johnsmith`)
     - Full name (if you know their GitHub profile)
     - Email address associated with their GitHub account

5. **Set Permission Level**
   - **Read**: Can only view and clone (default for public repos)
   - **Triage**: Can manage issues and pull requests
   - **Write**: Can push to the repository (recommended for active collaborators)
   - **Maintain**: Write access plus managing issues and PRs
   - **Admin**: Full access including settings and adding collaborators

6. **Send Invitation**
   - Click **Add [username] to this repository**
   - GitHub will send an email invitation to that person

### Managing Multiple Collaborators

- You can add as many collaborators as needed
- View all collaborators in the Settings > Collaborators page
- Remove collaborators by clicking the **Remove** button next to their name
- Change permission levels by selecting a different role

## For Collaborators: Accepting Invitation

### Accepting the Invitation

1. **Check Your Email**
   - Look for an email from GitHub with subject "You're invited to collaborate on..."
   - Click the link in the email

2. **Or via GitHub**
   - Go to https://github.com/cristeadevarat/proiect-uptispoo
   - You'll see a notification: "You've been invited to collaborate"
   - Click **Accept invitation**

3. **Verify Access**
   - After accepting, you should see the repository in your GitHub dashboard
   - You can now clone, push, and pull from the repository

## Git Workflow for Team Collaboration

### Initial Setup

```bash
# Clone the repository
git clone https://github.com/cristeadevarat/proiect-uptispoo.git

# Navigate to the project
cd proiect-uptispoo

# Check the repository status
git status
```

### Daily Workflow

#### 1. Before Starting Work

Always pull the latest changes:
```bash
git pull origin main
```

#### 2. Create a Feature Branch

Work on a separate branch to avoid conflicts:
```bash
# Create and switch to a new branch
git checkout -b feature/my-new-feature

# Or for bug fixes
git checkout -b fix/bug-description
```

#### 3. Make Your Changes

- Edit files as needed
- Test your changes

#### 4. Commit Your Changes

```bash
# See what files changed
git status

# Add specific files
git add filename.cs

# Or add all changed files
git add .

# Commit with a descriptive message
git commit -m "Add feature: description of what you did"
```

#### 5. Push Your Branch

```bash
# Push your branch to GitHub
git push origin feature/my-new-feature
```

#### 6. Create a Pull Request

1. Go to the repository on GitHub
2. You'll see a prompt: "Compare & pull request" - click it
3. Add a title and description explaining your changes
4. Click **Create pull request**
5. Wait for team members to review
6. Address any feedback
7. Once approved, merge the pull request

### Handling Conflicts

If someone else pushed changes to the same files:

```bash
# Pull the latest changes
git pull origin main

# If conflicts occur, Git will mark them in your files
# Open the files and resolve conflicts manually
# Look for markers like: <<<<<<<, =======, >>>>>>>

# After resolving, add the files
git add resolved-file.cs

# Continue the merge
git commit -m "Resolve merge conflicts"

# Push your changes
git push origin your-branch-name
```

## Best Practices for Team Collaboration

### Communication

- **Use Issues**: Track bugs, features, and tasks
- **Use Pull Requests**: Review each other's code before merging
- **Comment on Code**: Use GitHub's commenting features during reviews
- **Use Descriptive Commit Messages**: Help teammates understand changes

### Branching Strategy

- `main` - Stable, production-ready code
- `develop` - Integration branch for features (optional)
- `feature/*` - New features
- `fix/*` - Bug fixes
- `hotfix/*` - Urgent fixes for production

### Code Reviews

- Review pull requests before merging
- Test the changes locally if possible
- Provide constructive feedback
- Approve when ready

### Commit Message Guidelines

Good commit messages:
```
Add user authentication feature
Fix null reference exception in login
Update README with setup instructions
Refactor database connection logic
```

Bad commit messages:
```
fixed stuff
update
changes
asdf
```

## Troubleshooting

### "Permission denied" Error

- Verify you've accepted the invitation
- Check that you have Write access or higher
- Ensure you're authenticated (use SSH keys or HTTPS with credentials)

### "Repository not found"

- Check the repository URL
- Verify you've accepted the invitation
- Ensure you're logged into the correct GitHub account

### Merge Conflicts

- Pull the latest changes: `git pull origin main`
- Resolve conflicts in your code editor
- Commit the resolved changes
- Push again

## Additional Resources

- [GitHub Documentation](https://docs.github.com/)
- [Git Basics](https://git-scm.com/book/en/v2/Getting-Started-Git-Basics)
- [GitHub Flow Guide](https://guides.github.com/introduction/flow/)
- [Collaborating with Pull Requests](https://docs.github.com/en/pull-requests/collaborating-with-pull-requests)
