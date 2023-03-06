## Sutil Template for Full App

A full that demonstrates some commonly required features in an SPA

Features:
- Sveltish transitions (in response to media query)
- Elmish architecture
- Reactivity using Sveltish stores
- Navbar
- Login
- CRUD
- Routing
- Multipage
- Sidebar
- Bulma styling

### Quick Start

```
dotnet new --install Fable.Sutil.Templates
dotnet new sutil-app
```

Alternatively, from repo:

```
    git clone -s https://github.com/davedawkins/sutil-template-app.git
    cd sutil-template-app
    dotnet tool restore
    npm install
    npm run start
```

![Screenshot of App](images/screenshot.png)