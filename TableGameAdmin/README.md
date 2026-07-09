# 🎲 Table Game Admin Dashboard

Aplicación de escritorio para administrar un catálogo de juegos de mesa (alquiler por hora).
Demuestra una arquitectura cliente–servidor completa en **C# / .NET**: una app de escritorio
con patrón **MVVM** que consume una **REST API** propia, la cual persiste los datos en **SQLite**
mediante **Entity Framework Core**.

> **Nota técnica:** la UI usa **Avalonia** (en lugar de WPF) porque el desarrollo se hizo en macOS.
> Avalonia es XAML + MVVM, prácticamente idéntico a WPF; los mismos conceptos (data binding,
> `ObservableObject`, `RelayCommand`, `DataGrid`) se transfieren directamente a WPF.

## 🏗️ Arquitectura

```
┌─────────────────────────────┐         HTTP/JSON        ┌──────────────────────────┐
│   TableGames.Desktop        │  ───────────────────▶    │   TableGames.Api         │
│   (Avalonia · MVVM)         │   GET/POST/PUT/DELETE    │   (ASP.NET Core Web API)  │
│                             │  ◀───────────────────    │                          │
│   View ──► ViewModel ──►    │                          │   Controller ──► EF Core │
│   GameApiClient (HttpClient)│                          │   AppDbContext           │
└─────────────────────────────┘                          └───────────┬──────────────┘
                                                                      │
                                                              ┌───────▼────────┐
                                                              │  SQLite (.db)  │
                                                              └────────────────┘
```

## 🧰 Stack técnico

| Capa        | Tecnología                                              |
|-------------|---------------------------------------------------------|
| Frontend    | Avalonia 12, MVVM, CommunityToolkit.Mvvm, DataGrid      |
| Backend     | ASP.NET Core Web API (controllers), .NET 10             |
| Datos       | Entity Framework Core + SQLite                           |
| Patrones    | MVVM, Repository (vía DbContext), DTOs, Dependency Injection |

## ✨ Funcionalidades

- **CRUD completo** de juegos: crear, listar, editar y eliminar.
- Tabla (`DataGrid`) con binding a una `ObservableCollection`.
- Formulario de edición con binding bidireccional y validación.
- Validación tanto en cliente (ViewModel) como en servidor (API).
- Datos de ejemplo (seed) para arrancar con contenido.
- Barra de estado con feedback de operaciones y manejo de errores.

## 🚀 Cómo ejecutarlo

Necesitas el **.NET SDK 10**. Abre **dos terminales**:

**Terminal 1 — la API:**
```bash
cd src/TableGames.Api
dotnet run --launch-profile http
# Queda escuchando en http://localhost:5264
```

**Terminal 2 — la app de escritorio:**
```bash
cd src/TableGames.Desktop
dotnet run
```

La app abre una ventana, llama a la API y muestra los juegos. La base de datos
`tablegames.db` se crea automáticamente la primera vez.

## 🔌 Endpoints de la API

| Método | Ruta              | Descripción              |
|--------|-------------------|--------------------------|
| GET    | `/api/games`      | Lista todos los juegos   |
| GET    | `/api/games/{id}` | Obtiene un juego         |
| POST   | `/api/games`      | Crea un juego            |
| PUT    | `/api/games/{id}` | Actualiza un juego       |
| DELETE | `/api/games/{id}` | Elimina un juego         |

Ejemplo:
```bash
curl http://localhost:5264/api/games
curl -X POST http://localhost:5264/api/games \
  -H "Content-Type: application/json" \
  -d '{"name":"Dominion","category":"Cartas","minPlayers":2,"maxPlayers":4,"pricePerHour":4.0,"isAvailable":true}'
```

## 📁 Estructura

```
TableGameAdmin/
├── TableGameAdmin.slnx
└── src/
    ├── TableGames.Api/            # REST API + EF Core + SQLite
    │   ├── Controllers/GamesController.cs
    │   ├── Data/AppDbContext.cs
    │   ├── Dtos/GameDtos.cs
    │   ├── Models/Game.cs
    │   └── Program.cs
    └── TableGames.Desktop/        # App Avalonia (MVVM)
        ├── Models/Game.cs
        ├── Services/GameApiClient.cs
        ├── ViewModels/MainWindowViewModel.cs
        └── Views/MainWindow.axaml
```
