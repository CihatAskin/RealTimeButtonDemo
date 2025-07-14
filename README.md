# RealTimeButtonDemo

A real-time Blazor Hybrid demo application for educational purposes. This application demonstrates cross-platform button color synchronization using SignalR across Web and Mobile platforms with shared Blazor components.

## Features

- **Real-time synchronization** using SignalR
- **Cross-platform support** - Web, Android, iOS, Windows, macOS
- **Shared Blazor components** between Web and Mobile
- **Simple JWT authentication** with hardcoded credentials
- **SQLite database** for data persistence
- **Responsive UI** with real-time updates

## Tech Stack

- .NET 8.0
- ASP.NET Core Web API
- Blazor WebAssembly
- .NET MAUI (Multi-platform App UI)
- SignalR for real-time communication
- Entity Framework Core with SQLite
- JWT Authentication

## Prerequisites

Before running this application, ensure you have the following installed:

### Required Software

1. **.NET 8.0 SDK** or later
   ```bash
   # Check your .NET version
   dotnet --version
   ```
   Download from: https://dotnet.microsoft.com/download

2. **Visual Studio 2022** (Recommended) or **Visual Studio Code**
   - Visual Studio 2022 with .NET MAUI workload
   - Or VS Code with C# extension

3. **.NET MAUI Workload** (for mobile development)
   ```bash
   dotnet workload install maui
   ```

### Platform-Specific Requirements

#### For Android Development:
- Android SDK (API level 24 or higher)
- Android Emulator or physical device

#### For iOS Development (macOS only):
- Xcode 14.0 or later
- iOS Simulator or physical device

#### For Windows Development:
- Windows 10 version 1903 or later
- Windows App SDK

## Installation & Setup

### 1. Clone the Repository

```bash
git clone https://github.com/CihatAskin/RealTimeButtonDemo.git
cd RealTimeButtonDemo
```

### 2. Restore Dependencies

```bash
# Restore NuGet packages for all projects
dotnet restore
```

### 3. Database Setup

```bash
# Install Entity Framework tools (if not already installed)
dotnet tool install --global dotnet-ef

# Navigate to the API project
cd src/RealTimeButtonDemo.API

# Create database migrations
dotnet ef migrations add InitialCreate

# Apply migrations and create database
dotnet ef database update
```

### 4. Build the Solution

```bash
# Build all projects
dotnet build
```

## Windows Specific Setup

### Database Connection String
On Windows, you may need to update the connection string in `src/RealTimeButtonDemo.API/appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=..\\..\\database\\realtimebutton.db"
  }
}
```

**Important:** Always include `Data Source=` prefix for SQLite connections.

### PowerShell Commands
Use PowerShell for all commands on Windows:
```powershell
# Create database directory
mkdir database -Force

# Run migrations
dotnet ef database update

# Run projects
dotnet run
```

## Running the Application

### Development Mode

You need to run both the API and Web projects simultaneously:

#### Terminal 1 - API Server (Backend)
```bash
cd src/RealTimeButtonDemo.API
dotnet run
```
The API will be available at: `https://localhost:5001`

#### Terminal 2 - Web Client
```bash
cd src/RealTimeButtonDemo.Web
dotnet run
```
The Web application will be available at: `https://localhost:5002`

### Running Mobile App

#### Using Visual Studio 2022:
1. Open `RealTimeButtonDemo.sln`
2. Set `RealTimeButtonDemo.Mobile` as startup project
3. Select your target platform (Android, iOS, Windows, macOS)
4. Press F5 to run

#### Using Command Line:

**Android:**
```bash
cd src/RealTimeButtonDemo.Mobile
dotnet build -f net8.0-android
```

**iOS (macOS only):**
```bash
cd src/RealTimeButtonDemo.Mobile
dotnet build -f net8.0-ios
```

**Windows:**
```bash
cd src/RealTimeButtonDemo.Mobile
dotnet build -f net8.0-windows10.0.19041.0
```

## Development Credentials & Security

**⚠️ Educational Project - Not for Production Use**

This project uses hardcoded credentials for educational purposes:

### Default Login Credentials:
- **Username:** `admin` | **Password:** `admin123`
- **Username:** `demo` | **Password:** `admin123`  
- **Username:** `test` | **Password:** `admin123`

### JWT Secret Key:
```json
"SecretKey": "RealTimeButtonDemo_SuperSecretKey_ForDevelopmentOnly_2024!"
```

**Note:** These credentials are intentionally exposed for learning purposes. Never use hardcoded credentials in production applications.

## API Endpoints

- **Authentication:** `POST /api/auth/login`
- **SignalR Hub:** `https://localhost:5001/buttonhub`
- **Swagger UI:** `https://localhost:5001/swagger`

## Platform-Specific Notes

### Windows Users:
- Use **PowerShell** for all commands
- Connection string uses `\\` for path separators
- Ensure **Visual Studio 2022** with .NET MAUI workload is installed
- Database path: `..\\..\\database\\realtimebutton.db`

### Linux/macOS Users:
- Connection string uses `/` for path separators  
- Update paths in `appsettings.Development.json` accordingly
- For iOS development, **macOS with Xcode** is required
- Database path: `../../database/realtimebutton.db`

### Cross-Platform Development:
- All source code is platform-independent
- Only configuration files need path adjustments
- MAUI project targets all major platforms

## Project Structure

```
RealTimeButtonDemo/
├── src/
│   ├── RealTimeButtonDemo.API/          # Backend Web API + SignalR Hub
│   ├── RealTimeButtonDemo.Web/          # Blazor WebAssembly Client
│   ├── RealTimeButtonDemo.Mobile/       # .NET MAUI Blazor Hybrid App
│   └── RealTimeButtonDemo.Shared/       # Shared Models & Services
├── database/                            # SQLite database files
├── docs/                               # Documentation
└── README.md
```

## Configuration

### API Configuration (appsettings.json)
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=./database/realtimebutton.db"
  },
  "JwtSettings": {
    "SecretKey": "your-secret-key",
    "ExpirationMinutes": 60
  },
  "Authentication": {
    "HardcodedPassword": "admin123"
  }
}
```

### Ports
- **API Server:** `https://localhost:5001`
- **Web Client:** `https://localhost:5002`

## Troubleshooting

### Common Issues

1. **HTTPS Certificate Issues:**
   ```bash
   dotnet dev-certs https --trust
   ```

2. **Port Already in Use:**
   ```bash
   # Kill process using port 5001
   netstat -ano | findstr :5001
   taskkill /PID <PID> /F
   ```

3. **Database Connection Issues:**
   ```bash
   # Reset database
   dotnet ef database drop --force
   dotnet ef database update
   ```

4. **MAUI Workload Issues:**
   ```bash
   dotnet workload update
   dotnet workload repair
   ```

5. **SQLite Connection Issues:**
   ```bash
   # Error: Format of the initialization string does not conform to specification
   # Solution: Add "Data Source=" prefix to connection string
   "DefaultConnection": "Data Source=..\\..\\database\\realtimebutton.db"

   # Error: unable to open database file  
   # Solution: Create database directory and check path
   mkdir database
   dotnet ef database update
   ```

6. **Entity Framework Migration Issues:**
   ```bash
   # Error: PendingModelChangesWarning
   # Solution: Remove and recreate migrations
   dotnet ef migrations remove --force
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

7. **Path Issues (Windows vs Linux):**
   ```bash
   # Windows path format
   "Data Source=..\\..\\database\\realtimebutton.db"

   # Linux/macOS path format  
   "Data Source=../../database/realtimebutton.db"
   ```

### Development Tips

- Use **Visual Studio 2022** for the best MAUI development experience
- Enable **Hot Reload** for faster development
- Check **Output** and **Error List** windows for detailed error information
- Use **Swagger UI** at `https://localhost:5001/swagger` to test API endpoints

## Testing

### Manual Testing
1. Open Web application in browser
2. Open Mobile app on device/emulator
3. Login with credentials on both platforms
4. Click buttons and verify real-time synchronization

### API Testing
Use Swagger UI at `https://localhost:5001/swagger` to test API endpoints.

## Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Submit a pull request

## License

This project is for educational purposes only.

## Support

For issues and questions, please check the [GitHub Issues](https://github.com/CihatAskin/RealTimeButtonDemo/issues) page.