# 🛠️ SuperAuth Development Environment Setup

[![Setup Status](https://img.shields.io/badge/setup-automated-brightgreen.svg)](https://github.com/superauth/superauth/actions)
[![Platform Support](https://img.shields.io/badge/platform-Windows%20%7C%20macOS%20%7C%20Linux-blue.svg)](#platform-support)
[![Development Time](https://img.shields.io/badge/setup%20time-30%20minutes-blue.svg)](#quick-setup)

**Complete development environment setup for SuperAuth contributors and maintainers.**

This guide helps you set up a full development environment for SuperAuth, including all necessary tools, dependencies, and configurations for contributing to the codebase.

## 📋 Table of Contents

1. [Overview](#overview)
1. [Prerequisites](#prerequisites)
1. [Quick Setup (Automated)](#quick-setup-automated)
1. [Manual Setup](#manual-setup)
1. [IDE Configuration](#ide-configuration)
1. [Database Setup](#database-setup)
1. [Environment Variables](#environment-variables)
1. [Running in Development Mode](#running-in-development-mode)
1. [Development Workflow](#development-workflow)
1. [Troubleshooting](#troubleshooting)
1. [Advanced Configuration](#advanced-configuration)

-----

## 🎯 Overview

SuperAuth development environment consists of:

### **Technology Stack**

|Component             |Technology  |Version|Purpose                      |
|----------------------|------------|-------|-----------------------------|
|**Backend Framework** |ASP.NET Core|8.0+   |API server and services      |
|**Frontend Framework**|Angular     |17+    |Admin dashboard & user portal|
|**Database**          |PostgreSQL  |15+    |Primary data storage         |
|**Cache**             |Redis       |7.0+   |Session and performance cache|
|**Vector Database**   |Qdrant      |1.7+   |ML embeddings and analysis   |
|**Package Manager**   |NuGet + npm |Latest |Dependency management        |
|**Container Platform**|Docker      |20.0+  |Local services orchestration |

### **Development Services**

```yaml
Services_Architecture:
  
Backend_Services:
  - "SuperAuth.API (Port: 7001)"
  - "SuperAuth.Dashboard (Port: 4200)"  
  - "SuperAuth.UserPortal (Port: 4201)"
  
Infrastructure_Services:
  - "PostgreSQL (Port: 5432)"
  - "Redis (Port: 6379)"
  - "Qdrant (Port: 6333)"
  - "Mailhog (Port: 8025) # Email testing"
  
Development_Tools:
  - "pgAdmin (Port: 5050) # Database management"
  - "Redis Commander (Port: 8081) # Redis UI"
  - "Seq (Port: 5341) # Structured logging"
```

-----

## 📋 Prerequisites

### **Required Software**

|Tool              |Version|Download Link                                                |Verification Command|
|------------------|-------|-------------------------------------------------------------|--------------------|
|**Git**           |2.30+  |[Get Git](https://git-scm.com/downloads)                     |`git --version`     |
|**Docker Desktop**|20.0+  |[Get Docker](https://www.docker.com/products/docker-desktop/)|`docker --version`  |
|**.NET SDK**      |8.0+   |[Get .NET](https://dotnet.microsoft.com/download)            |`dotnet --version`  |
|**Node.js**       |18.0+  |[Get Node.js](https://nodejs.org/)                           |`node --version`    |
|**Angular CLI**   |17.0+  |`npm install -g @angular/cli`                                |`ng version`        |

### **Recommended IDEs**

|IDE                   |Best For        |Extensions/Plugins                           |
|----------------------|----------------|---------------------------------------------|
|**Visual Studio Code**|Full-stack dev  |C#, Angular Language Service, Docker, GitLens|
|**Visual Studio 2022**|Backend-focused |ASP.NET Core, Entity Framework Tools         |
|**JetBrains Rider**   |Professional dev|Built-in .NET + Angular support              |
|**WebStorm**          |Frontend-focused|Angular, TypeScript support                  |

### **Quick Prerequisites Check**

```bash
# Run this script to verify all prerequisites
cat > check-prerequisites.sh << 'EOF'
#!/bin/bash

echo "🔍 Checking SuperAuth Development Prerequisites..."
echo ""

# Check Git
if command -v git &> /dev/null; then
    echo "✅ Git: $(git --version)"
else
    echo "❌ Git: Not installed"
fi

# Check Docker
if command -v docker &> /dev/null; then
    echo "✅ Docker: $(docker --version)"
else
    echo "❌ Docker: Not installed"
fi

# Check .NET SDK
if command -v dotnet &> /dev/null; then
    echo "✅ .NET SDK: $(dotnet --version)"
else
    echo "❌ .NET SDK: Not installed"
fi

# Check Node.js
if command -v node &> /dev/null; then
    echo "✅ Node.js: $(node --version)"
else
    echo "❌ Node.js: Not installed"
fi

# Check npm
if command -v npm &> /dev/null; then
    echo "✅ npm: $(npm --version)"
else
    echo "❌ npm: Not installed"
fi

# Check Angular CLI
if command -v ng &> /dev/null; then
    echo "✅ Angular CLI: $(ng version --quiet 2>/dev/null | head -n1)"
else
    echo "❌ Angular CLI: Not installed (run: npm install -g @angular/cli)"
fi

echo ""
echo "🎯 Prerequisites check complete!"
EOF

chmod +x check-prerequisites.sh
./check-prerequisites.sh
```

-----

## 🚀 Quick Setup (Automated)

### **Option 1: Automated Development Setup**

**For developers who want to get started immediately:**

```bash
# Clone SuperAuth repository
git clone https://github.com/superauth/superauth.git
cd superauth

# Run automated development setup
chmod +x scripts/setup-dev.sh
./scripts/setup-dev.sh

# Wait for setup completion (5-10 minutes)
# Script will:
# 1. Install all dependencies
# 2. Start Docker services
# 3. Run database migrations
# 4. Seed development data
# 5. Start all development servers
```

**What the automated setup does:**

```yaml
Automated_Setup_Process:
  
Infrastructure:
    - "Start PostgreSQL, Redis, Qdrant containers"
    - "Create development database"
    - "Run EF Core migrations"
    - "Seed test data (users, applications)"
    
Backend:
    - "Restore NuGet packages"
    - "Build SuperAuth.API project"
    - "Generate JWT signing keys"
    - "Configure development appsettings"
    
Frontend:
    - "Install npm dependencies"
    - "Build Angular Dashboard"
    - "Build Angular UserPortal"
    - "Configure proxy settings"
    
Development_Tools:
    - "Start pgAdmin for database management"
    - "Start Redis Commander for cache inspection"
    - "Start Seq for structured logging"
    - "Configure VS Code workspace"
```

**Expected Output:**

```
🚀 SuperAuth Development Environment Setup Complete!

📋 Services Status:
✅ PostgreSQL Database    → http://localhost:5432
✅ Redis Cache           → http://localhost:6379  
✅ Qdrant Vector DB      → http://localhost:6333
✅ SuperAuth API         → https://localhost:7001
✅ Admin Dashboard       → http://localhost:4200
✅ User Portal          → http://localhost:4201

🛠️ Development Tools:
✅ pgAdmin              → http://localhost:5050
✅ Redis Commander      → http://localhost:8081  
✅ Seq Logs             → http://localhost:5341
✅ MailHog (Email)      → http://localhost:8025

🔑 Development Credentials:
👤 Super Admin: admin@superauth.io / SuperAuth123!
👤 Test User: user@demo.superauth.io / Demo123!
🔑 Database: superauth_dev / superauth123

🎯 Next Steps:
1. Open VS Code: code .
2. Press F5 to start debugging
3. Visit: http://localhost:4200
```

-----

## 🔧 Manual Setup

### **Step 1: Clone and Navigate**

```bash
# Clone repository
git clone https://github.com/superauth/superauth.git
cd superauth

# Create development branch
git checkout -b feature/your-feature-name
```

### **Step 2: Infrastructure Services**

```bash
# Start infrastructure services with Docker Compose
docker-compose -f docker-compose.dev.yml up -d

# Verify services are running
docker-compose -f docker-compose.dev.yml ps

# Expected output:
# NAME                    SERVICE             STATUS              PORTS
# superauth_postgres_1    postgres           running             0.0.0.0:5432->5432/tcp
# superauth_redis_1       redis              running             0.0.0.0:6379->6379/tcp
# superauth_qdrant_1      qdrant             running             0.0.0.0:6333->6333/tcp
# superauth_pgadmin_1     pgadmin            running             0.0.0.0:5050->80/tcp
```

### **Step 3: Backend Setup**

```bash
# Navigate to backend directory
cd src/SuperAuth.API

# Restore NuGet packages
dotnet restore

# Update database with latest migrations
dotnet ef database update --connection "Host=localhost;Database=superauth_dev;Username=superauth;Password=superauth123"

# Seed development data
dotnet run --environment Development -- --seed-data

# Verify backend can start
dotnet run --environment Development

# Expected output:
# info: Microsoft.Hosting.Lifetime[14]
#       Now listening on: https://localhost:7001
# info: Microsoft.Hosting.Lifetime[14]
#       Now listening on: http://localhost:7000
# info: Microsoft.Hosting.Lifetime[0]
#       Application started. Press Ctrl+C to shut down.
```

### **Step 4: Frontend Setup**

```bash
# Setup Admin Dashboard
cd src/SuperAuth.Dashboard
npm install
npm run build
npm start

# In new terminal: Setup User Portal
cd src/SuperAuth.UserPortal  
npm install
npm run build
npm start

# Verify frontend applications
# Dashboard: http://localhost:4200
# User Portal: http://localhost:4201
```

-----

## 🎨 IDE Configuration

### **Visual Studio Code Setup**

**Install Required Extensions:**

```bash
# Install VS Code extensions via command line
code --install-extension ms-dotnettools.csharp
code --install-extension angular.ng-template
code --install-extension ms-vscode.vscode-typescript-next
code --install-extension ms-azuretools.vscode-docker
code --install-extension eamodio.gitlens
code --install-extension bradlc.vscode-tailwindcss
code --install-extension esbenp.prettier-vscode
```

**Workspace Configuration:**

Create `.vscode/settings.json`:

```json
{
  "dotnet.defaultSolution": "SuperAuth.sln",
  "typescript.preferences.importModuleSpecifier": "relative",
  "editor.formatOnSave": true,
  "editor.codeActionsOnSave": {
    "source.organizeImports": true,
    "source.fixAll.eslint": true
  },
  "files.exclude": {
    "**/bin": true,
    "**/obj": true,
    "**/node_modules": true,
    "**/.angular": true
  },
  "search.exclude": {
    "**/node_modules": true,
    "**/dist": true,
    "**/coverage": true,
    "**/.angular": true
  },
  "emmet.includeLanguages": {
    "typescript": "html"
  },
  "angular.suggest-angular-cli-command": true,
  "terminal.integrated.defaultProfile.windows": "Command Prompt"
}
```

**Launch Configuration:**

Create `.vscode/launch.json`:

```json
{
  "version": "0.2.0",
  "configurations": [
    {
      "name": "Launch SuperAuth API",
      "type": "coreclr",
      "request": "launch",
      "preLaunchTask": "build",
      "program": "${workspaceFolder}/src/SuperAuth.API/bin/Debug/net8.0/SuperAuth.API.dll",
      "args": [],
      "cwd": "${workspaceFolder}/src/SuperAuth.API",
      "console": "internalConsole",
      "stopAtEntry": false,
      "env": {
        "ASPNETCORE_ENVIRONMENT": "Development",
        "ASPNETCORE_URLS": "https://localhost:7001;http://localhost:7000"
      }
    },
    {
      "name": "Launch Chrome (Dashboard)",
      "type": "pwa-chrome",
      "request": "launch",
      "url": "http://localhost:4200",
      "webRoot": "${workspaceFolder}/src/SuperAuth.Dashboard"
    },
    {
      "name": "Launch Chrome (User Portal)",
      "type": "pwa-chrome",
      "request": "launch",
      "url": "http://localhost:4201",
      "webRoot": "${workspaceFolder}/src/SuperAuth.UserPortal"
    }
  ],
  "compounds": [
    {
      "name": "Launch Full Stack",
      "configurations": [
        "Launch SuperAuth API",
        "Launch Chrome (Dashboard)",
        "Launch Chrome (User Portal)"
      ]
    }
  ]
}
```

**Tasks Configuration:**

Create `.vscode/tasks.json`:

```json
{
  "version": "2.0.0",
  "tasks": [
    {
      "label": "build",
      "command": "dotnet",
      "type": "process",
      "args": [
        "build",
        "${workspaceFolder}/SuperAuth.sln",
        "/property:GenerateFullPaths=true",
        "/consoleloggerparameters:NoSummary"
      ],
      "problemMatcher": "$msCompile"
    },
    {
      "label": "publish",
      "command": "dotnet",
      "type": "process",
      "args": [
        "publish",
        "${workspaceFolder}/SuperAuth.sln",
        "/property:GenerateFullPaths=true",
        "/consoleloggerparameters:NoSummary"
      ],
      "problemMatcher": "$msCompile"
    },
    {
      "label": "watch",
      "command": "dotnet",
      "type": "process",
      "args": [
        "watch",
        "run",
        "--project",
        "${workspaceFolder}/src/SuperAuth.API"
      ],
      "problemMatcher": "$msCompile"
    }
  ]
}
```

### **Visual Studio 2022 Setup**

**Project Configuration:**

1. Open `SuperAuth.sln` in Visual Studio 2022
1. Right-click Solution → Properties
1. Set Startup Projects → Multiple startup projects:
- `SuperAuth.API` → Start
- `SuperAuth.Dashboard` → Start without debugging
- `SuperAuth.UserPortal` → Start without debugging

**Debugging Configuration:**

```xml
<!-- Add to SuperAuth.API/Properties/launchSettings.json -->
{
  "profiles": {
    "SuperAuth.API (Development)": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": false,
      "applicationUrl": "https://localhost:7001;http://localhost:7000",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development",
        "SUPERAUTH_ENABLE_DETAILED_LOGGING": "true",
        "SUPERAUTH_ENABLE_SWAGGER": "true"
      }
    }
  }
}
```

-----

## 🗄️ Database Setup

### **PostgreSQL Configuration**

**Connection String:**

```yaml
Database_Configuration:
  
Development:
    Host: "localhost"
    Port: "5432" 
    Database: "superauth_dev"
    Username: "superauth"
    Password: "superauth123"
    Connection_String: "Host=localhost;Database=superauth_dev;Username=superauth;Password=superauth123"
    
Testing:
    Host: "localhost"
    Port: "5432"
    Database: "superauth_test"
    Username: "superauth"
    Password: "superauth123"
    Connection_String: "Host=localhost;Database=superauth_test;Username=superauth;Password=superauth123"
```

**Entity Framework Migrations:**

```bash
# Create new migration
cd src/SuperAuth.Infrastructure
dotnet ef migrations add YourMigrationName --startup-project ../SuperAuth.API

# Update database
dotnet ef database update --startup-project ../SuperAuth.API

# Generate SQL script
dotnet ef migrations script --startup-project ../SuperAuth.API --output migration.sql

# Reset database (⚠️ Data loss!)
dotnet ef database drop --startup-project ../SuperAuth.API --force
dotnet ef database update --startup-project ../SuperAuth.API
```

**Seed Development Data:**

```bash
# Seed development data
cd src/SuperAuth.API
dotnet run --environment Development -- --seed-data

# Seed specific data types
dotnet run --environment Development -- --seed-users --seed-applications --seed-security-events
```

**Database Management:**

```bash
# Access pgAdmin
open http://localhost:5050

# Login credentials:
# Email: admin@superauth.io
# Password: SuperAuth123!

# Connect to SuperAuth database:
# Host: postgres (Docker network)
# Port: 5432
# Database: superauth_dev
# Username: superauth
# Password: superauth123
```

-----

## 🔧 Environment Variables

### **Development Environment Variables**

Create `.env.development`:

```bash
# SuperAuth API Configuration
ASPNETCORE_ENVIRONMENT=Development
ASPNETCORE_URLS=https://localhost:7001;http://localhost:7000
SUPERAUTH_ENABLE_SWAGGER=true
SUPERAUTH_ENABLE_DETAILED_LOGGING=true

# Database Configuration
SUPERAUTH_DATABASE_CONNECTION_STRING=Host=localhost;Database=superauth_dev;Username=superauth;Password=superauth123

# Redis Configuration  
SUPERAUTH_REDIS_CONNECTION_STRING=localhost:6379

# Qdrant Configuration
SUPERAUTH_QDRANT_HOST=localhost
SUPERAUTH_QDRANT_PORT=6333

# JWT Configuration
SUPERAUTH_JWT_ISSUER=https://localhost:7001
SUPERAUTH_JWT_AUDIENCE=superauth-dev
SUPERAUTH_JWT_SECRET_KEY=development-secret-key-change-in-production-min-32-chars

# Security Configuration
SUPERAUTH_ENABLE_SECURITY_ANALYSIS=true
SUPERAUTH_ML_MODEL_PATH=/app/models/
SUPERAUTH_THREAT_DETECTION_ENABLED=true

# Email Configuration (Development - uses MailHog)
SUPERAUTH_EMAIL_PROVIDER=MailHog
SUPERAUTH_EMAIL_SMTP_HOST=localhost
SUPERAUTH_EMAIL_SMTP_PORT=1025
SUPERAUTH_EMAIL_FROM_ADDRESS=noreply@superauth.dev

# External Integrations (Development)
SUPERAUTH_AZURE_AD_ENABLED=false
SUPERAUTH_GOOGLE_SSO_ENABLED=false
SUPERAUTH_GITHUB_SSO_ENABLED=false

# Monitoring and Logging
SUPERAUTH_SEQ_URL=http://localhost:5341
SUPERAUTH_APPLICATION_INSIGHTS_ENABLED=false

# Development Features
SUPERAUTH_ENABLE_HOT_RELOAD=true
SUPERAUTH_ENABLE_CORS_ANY_ORIGIN=true
SUPERAUTH_ENABLE_DEVELOPMENT_USER_SECRETS=true
```

**Load Environment Variables:**

```bash
# Option 1: Using dotenv (recommended)
npm install -g dotenv-cli
dotenv -e .env.development -- dotnet run

# Option 2: Export manually (Linux/macOS)
export $(grep -v '^#' .env.development | xargs)
dotnet run

# Option 3: PowerShell (Windows)
Get-Content .env.development | foreach {
  $name, $value = $_.split('=')
  if ([string]::IsNullOrWhiteSpace($name) -or $name.Contains('#')) {
    continue
  }
  Set-Item -Path "env:$name" -Value $value
}
dotnet run
```

-----

## 🏃‍♂️ Running in Development Mode

### **Full Stack Development**

**Option 1: All Services (Recommended)**

```bash
# Terminal 1: Infrastructure services
docker-compose -f docker-compose.dev.yml up -d

# Terminal 2: Backend API with hot reload
cd src/SuperAuth.API
dotnet watch run --environment Development

# Terminal 3: Admin Dashboard with hot reload
cd src/SuperAuth.Dashboard  
ng serve --port 4200 --open

# Terminal 4: User Portal with hot reload
cd src/SuperAuth.UserPortal
ng serve --port 4201 --open
```

**Option 2: Selective Services**

```bash
# Start only infrastructure
docker-compose -f docker-compose.dev.yml up -d postgres redis qdrant

# Start backend only
cd src/SuperAuth.API
dotnet run --environment Development

# Start frontend only (connects to running backend)
cd src/SuperAuth.Dashboard
ng serve --configuration development
```

### **Development URLs**

|Service            |URL                           |Purpose                 |Credentials                       |
|-------------------|------------------------------|------------------------|----------------------------------|
|**SuperAuth API**  |https://localhost:7001        |Backend API & Swagger   |N/A                               |
|**Admin Dashboard**|http://localhost:4200         |Administration interface|admin@superauth.io / SuperAuth123!|
|**User Portal**    |http://localhost:4201         |End-user authentication |user@demo.superauth.io / Demo123! |
|**Swagger UI**     |https://localhost:7001/swagger|API documentation       |N/A                               |
|**pgAdmin**        |http://localhost:5050         |Database management     |admin@superauth.io / SuperAuth123!|
|**Redis Commander**|http://localhost:8081         |Redis cache management  |N/A                               |
|**Seq Logs**       |http://localhost:5341         |Structured logging      |N/A                               |
|**MailHog**        |http://localhost:8025         |Email testing           |N/A                               |

### **Health Checks**

```bash
# API Health Check
curl -k https://localhost:7001/health

# Expected Response:
{
  "status": "Healthy",
  "totalDuration": "00:00:00.0123456",
  "entries": {
    "database": {
      "status": "Healthy",
      "duration": "00:00:00.0050000"
    },
    "redis": {
      "status": "Healthy", 
      "duration": "00:00:00.0020000"
    },
    "qdrant": {
      "status": "Healthy",
      "duration": "00:00:00.0030000"
    }
  }
}

# Database Connection Test
curl -k https://localhost:7001/api/v1/health/database

# Authentication Test
curl -k -X POST https://localhost:7001/api/v1/auth/login \
  -H "Content-Type: application/json" \
  -d '{
    "email": "admin@superauth.io",
    "password": "SuperAuth123!"
  }'
```

-----

## 🔄 Development Workflow

### **Daily Development Routine**

```bash
# 1. Start development session
cd /path/to/superauth
git pull origin main
docker-compose -f docker-compose.dev.yml up -d

# 2. Check service health
curl -k https://localhost:7001/health

# 3. Start coding with hot reload
# Terminal 1: Backend
cd src/SuperAuth.API && dotnet watch run

# Terminal 2: Frontend  
cd src/SuperAuth.Dashboard && ng serve

# 4. Run tests during development
dotnet test
npm run test

# 5. End development session
docker-compose -f docker-compose.dev.yml down
```

### **Git Workflow Integration**

```bash
# Pre-commit hooks setup
cd .git/hooks
cat > pre-commit << 'EOF'
#!/bin/bash
echo "🔍 Running pre-commit checks..."

# Format .NET code
dotnet format --verify-no-changes

# Build backend
dotnet build

# Test backend
dotnet test --no-build

# Lint frontend
cd src/SuperAuth.Dashboard && npm run lint
cd ../SuperAuth.UserPortal && npm run lint

# Build frontend
cd src/SuperAuth.Dashboard && npm run build
cd ../SuperAuth.UserPortal && npm run build

echo "✅ Pre-commit checks passed!"
EOF

chmod +x pre-commit
```

### **Code Generation and Scaffolding**

```bash
# Generate new API controller
cd src/SuperAuth.API
dotnet aspnet-codegenerator controller \
  -name YourController \
  -api \
  -outDir Controllers \
  -namespace SuperAuth.API.Controllers

# Generate Entity Framework entity
cd src/SuperAuth.Infrastructure
dotnet ef dbcontext scaffold \
  "Host=localhost;Database=superauth_dev;Username=superauth;Password=superauth123" \
  Npgsql.EntityFrameworkCore.PostgreSQL \
  -o Models \
  -t YourTableName

# Generate Angular component
cd src/SuperAuth.Dashboard
ng generate component features/your-feature --module=app

# Generate Angular service
ng generate service core/services/your-service
```

-----

## 🚨 Troubleshooting

### **Common Issues and Solutions**

#### **Issue 1: Docker Services Won’t Start**

**Error:**

```
ERROR: Port 5432 is already in use
```

**Solution:**

```bash
# Check what's using the port
lsof -i :5432  # macOS/Linux
netstat -ano | findstr :5432  # Windows

# Stop conflicting services
brew services stop postgresql  # macOS
sudo systemctl stop postgresql  # Linux
net stop postgresql-x64-14  # Windows

# Or use different ports in docker-compose.dev.yml
ports:
  - "5433:5432"  # Map to different local port
```

#### **Issue 2: Database Migration Failures**

**Error:**

```
Unable to connect to database
```

**Solution:**

```bash
# Verify PostgreSQL is running
docker-compose -f docker-compose.dev.yml ps postgres

# Check database logs
docker-compose -f docker-compose.dev.yml logs postgres

# Reset database completely
docker-compose -f docker-compose.dev.yml down -v
docker-compose -f docker-compose.dev.yml up -d postgres
sleep 10  # Wait for startup
dotnet ef database update --startup-project src/SuperAuth.API
```

#### **Issue 3: Angular Build Failures**

**Error:**

```
Module not found: Error: Can't resolve 'some-package'
```

**Solution:**

```bash
# Clear npm cache
npm cache clean --force

# Delete node_modules and reinstall
rm -rf node_modules package-lock.json
npm install

# Check Node.js version compatibility
node --version  # Should be 18.0+
npm --version   # Should be 9.0+

# Update Angular CLI if needed
npm install -g @angular/cli@latest
ng update @angular/core @angular/cli
```

#### **Issue 4: SSL Certificate Issues**

**Error:**

```
SSL connection error
```

**Solution:**

```bash
# Trust development certificate (Windows)
dotnet dev-certs https --trust

# Trust development certificate (macOS)
dotnet dev-certs https --trust

# Manual certificate creation (Linux)
dotnet dev-certs https
dotnet dev-certs https --trust

# For API testing, use -k flag with curl
curl -k https://localhost:7001/health
```

#### **Issue 5: Performance Issues**

**Symptoms:**

- Slow API responses
- High memory usage
- Database connection timeouts

**Solutions:**

```bash
# Monitor resource usage
docker stats

# Check database performance
docker-compose -f docker-compose.dev.yml exec postgres \
  psql -U superauth -d superauth_dev -c "
    SELECT query, calls, total_time, mean_time 
    FROM pg_stat_statements 
    ORDER BY total_time DESC 
    LIMIT 10;"

# Optimize Docker resources (Docker Desktop → Resources)
# Recommended: 4GB RAM, 2 CPUs minimum

# Clear Redis cache
docker-compose -f docker-compose.dev.yml exec redis redis-cli FLUSHALL

# Reset Qdrant collections
curl -X DELETE http://localhost:6333/collections/security_events
```

### **Debug Mode Configuration**

**Enable Detailed Logging:**

```json
// appsettings.Development.json
{
  "Logging": {
    "LogLevel": {
      "Default": "Debug",
      "Microsoft.AspNetCore": "Warning",
      "Microsoft.EntityFrameworkCore.Database.Command": "Information",
      "SuperAuth.Core": "Debug",
      "SuperAuth.Infrastructure": "Debug"
    }
  },
  "SuperAuth": {
    "EnableDetailedErrorMessages": true,
    "EnableSensitiveDataLogging": true,
    "LogSqlQueries": true
  }
}
```

**Visual Studio Debugging:**

1. Set breakpoints in your code
1. Press F5 or use Debug → Start Debugging
1. Use Debug → Windows → Output to see detailed logs
1. Use Debug → Windows → Immediate to execute commands

**VS Code Debugging:**

1. Open Debug panel (Ctrl+Shift+D)
1. Select “Launch Full Stack” configuration
1. Press F5 to start debugging
1. Use Debug Console for commands

-----

## 🔧 Advanced Configuration

### **Custom Database Configuration**

**Using Different Database Provider:**

```csharp
// In SuperAuth.Infrastructure/DependencyInjection.cs
public static IServiceCollection AddInfrastructure(
    this IServiceCollection services, 
    IConfiguration configuration)
{
    var connectionString = configuration.GetConnectionString("DefaultConnection");
    
    // Option 1: PostgreSQL (Default)
    services.AddDbContext<SuperAuthDbContext>(options =>
        options.UseNpgsql(connectionString));
    
    // Option 2: SQL Server
    // services.AddDbContext<SuperAuthDbContext>(options =>
    //     options.UseSqlServer(connectionString));
    
    // Option 3: SQLite (Development only)
    // services.AddDbContext<SuperAuthDbContext>(options =>
    //     options.UseSqlite(connectionString));
    
    return services;
}
```

### **Hot Reload Configuration**

```json
// src/SuperAuth.API/Properties/launchSettings.json
{
  "profiles": {
    "SuperAuth.API": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "hotReloadProfile": "aspnetcore",
      "launchBrowser": false,
      "applicationUrl": "https://localhost:7001;http://localhost:7000",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development",
        "DOTNET_WATCH_RESTART_ON_RUDE_EDIT": "true"
      }
    }
  }
}
```

### **Performance Optimization**

```yaml
Development_Performance_Tuning:
  
Docker_Optimization:
    Resources:
      Memory: "4GB minimum, 8GB recommended"
      CPU: "2 cores minimum, 4 cores recommended"
      Disk: "SSD recommended for database"
    
    Container_Settings:
      PostgreSQL:
        shared_buffers: "256MB"
        effective_cache_size: "1GB"
        work_mem: "16MB"
        maintenance_work_mem: "256MB"
        
      Redis:
        maxmemory: "512MB"
        maxmemory_policy: "allkeys-lru"
        save: "900 1 300 10 60 10000"
        
      Qdrant:
        collection_memory_threshold: "512MB"
        indexing_threshold: "20000"

.NET_Development_Optimization:
    Compilation:
      - "Use Release configuration for performance testing"
      - "Enable ReadyToRun images"
      - "Use Ahead-of-Time (AOT) compilation for critical paths"
      
    Entity_Framework:
      - "Enable connection pooling"
      - "Use compiled queries for frequent operations"
      - "Implement query splitting for complex joins"
      - "Use AsNoTracking() for read-only queries"
      
    Memory_Management:
      - "Configure garbage collection for server workloads"
      - "Use object pooling for frequently allocated objects"
      - "Implement proper disposal patterns"

Angular_Development_Optimization:
    Build_Performance:
      - "Use ng serve with --aot for production-like builds"
      - "Enable source maps for debugging"
      - "Use webpack bundle analyzer"
      
    Runtime_Performance:
      - "Implement OnPush change detection strategy"
      - "Use trackBy functions in ngFor loops"
      - "Lazy load feature modules"
      - "Optimize Angular bundle size"
```

**Docker Performance Configuration:**

```yaml
# docker-compose.dev.yml - Performance Optimized
version: '3.8'

services:
  postgres:
    image: postgres:15-alpine
    environment:
      POSTGRES_DB: superauth_dev
      POSTGRES_USER: superauth
      POSTGRES_PASSWORD: superauth123
    ports:
      - "5432:5432"
    volumes:
      - postgres_data:/var/lib/postgresql/data
      - ./scripts/postgres-init.sql:/docker-entrypoint-initdb.d/init.sql
    command: |
      postgres
      -c shared_buffers=256MB
      -c effective_cache_size=1GB
      -c work_mem=16MB
      -c maintenance_work_mem=256MB
      -c checkpoint_timeout=10min
      -c wal_buffers=16MB
      -c default_statistics_target=100
    deploy:
      resources:
        limits:
          memory: 1GB
        reservations:
          memory: 512MB

  redis:
    image: redis:7-alpine
    ports:
      - "6379:6379"
    command: |
      redis-server
      --maxmemory 512MB
      --maxmemory-policy allkeys-lru
      --save 900 1 300 10 60 10000
      --appendonly yes
      --appendfsync everysec
    volumes:
      - redis_data:/data
    deploy:
      resources:
        limits:
          memory: 768MB
        reservations:
          memory: 256MB

  qdrant:
    image: qdrant/qdrant:v1.7.4
    ports:
      - "6333:6333"
      - "6334:6334"
    volumes:
      - qdrant_data:/qdrant/storage
    environment:
      QDRANT__SERVICE__ENABLE_CORS: "true"
      QDRANT__SERVICE__GRPC_PORT: "6334"
      QDRANT__SERVICE__HTTP_PORT: "6333"
    deploy:
      resources:
        limits:
          memory: 2GB
        reservations:
          memory: 512MB

volumes:
  postgres_data:
  redis_data:
  qdrant_data:
```

**Entity Framework Performance Configuration:**

```csharp
// src/SuperAuth.Infrastructure/Persistence/EntityFramework/SuperAuthDbContext.cs
public class SuperAuthDbContext : DbContext
{
    public SuperAuthDbContext(DbContextOptions<SuperAuthDbContext> options) : base(options)
    {
        // Performance optimizations
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        ChangeTracker.AutoDetectChangesEnabled = false;
        ChangeTracker.LazyLoadingEnabled = false;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            // Development performance settings
            optionsBuilder
                .EnableSensitiveDataLogging(true)
                .EnableDetailedErrors(true)
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableServiceProviderCaching()
                .EnableModelValidation(false); // Disable in development for speed
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Apply configurations from assembly
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SuperAuthDbContext).Assembly);
        
        // Performance indexes
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique()
            .HasDatabaseName("IX_Users_Email");
            
        modelBuilder.Entity<SecurityEvent>()
            .HasIndex(e => new { e.UserId, e.CreatedAt })
            .HasDatabaseName("IX_SecurityEvents_UserId_CreatedAt");
            
        modelBuilder.Entity<UserSession>()
            .HasIndex(s => s.LastActivityAt)
            .HasDatabaseName("IX_UserSessions_LastActivity");
    }
}
```

**Angular Performance Configuration:**

```typescript
// src/SuperAuth.Dashboard/src/app/app.module.ts
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ServiceWorkerModule } from '@angular/service-worker';

@NgModule({
  imports: [
    BrowserModule,
    // Enable service worker for caching
    ServiceWorkerModule.register('ngsw-worker.js', {
      enabled: !isDevMode(),
      registrationStrategy: 'registerWhenStable:30000'
    }),
    // Lazy load feature modules
    RouterModule.forRoot(routes, {
      enableTracing: false, // Disable in production
      preloadingStrategy: PreloadAllModules,
      scrollPositionRestoration: 'enabled'
    })
  ]
})
export class AppModule { }
```

```json
// src/SuperAuth.Dashboard/angular.json - Development Optimization
{
  "projects": {
    "superauth-dashboard": {
      "architect": {
        "serve": {
          "builder": "@angular-devkit/build-angular:dev-server",
          "configurations": {
            "development": {
              "buildTarget": "superauth-dashboard:build:development",
              "proxyConfig": "proxy.conf.json",
              "host": "localhost",
              "port": 4200,
              "poll": 2000,
              "liveReload": true,
              "hmr": true
            }
          },
          "defaultConfiguration": "development"
        },
        "build": {
          "builder": "@angular-devkit/build-angular:browser",
          "configurations": {
            "development": {
              "optimization": false,
              "extractLicenses": false,
              "sourceMap": true,
              "namedChunks": true,
              "aot": true,
              "buildOptimizer": false,
              "vendorChunk": true,
              "commonChunk": true
            }
          }
        }
      }
    }
  }
}
```

-----

## 🔒 Security Configuration for Development

### **Development Security Settings**

```csharp
// src/SuperAuth.API/appsettings.Development.json
{
  "SuperAuth": {
    "Security": {
      "AllowInsecureHttp": true,
      "RequireHttps": false,
      "EnableCorsAnyOrigin": true,
      "JwtSettings": {
        "Issuer": "https://localhost:7001",
        "Audience": "superauth-dev",
        "SecretKey": "development-secret-key-change-in-production-min-32-chars",
        "AccessTokenExpirationMinutes": 60,
        "RefreshTokenExpirationDays": 30,
        "AllowInsecureTokenValidation": true
      },
      "PasswordPolicy": {
        "RequiredLength": 6,
        "RequireNonAlphanumeric": false,
        "RequireDigit": false,
        "RequireUppercase": false,
        "RequireLowercase": false
      },
      "RateLimiting": {
        "Enabled": false,
        "RequestsPerMinute": 1000,
        "BurstLimit": 100
      },
      "SecurityAnalysis": {
        "Enabled": true,
        "MockMode": true,
        "LogAllDecisions": true,
        "AlwaysAllowDevelopmentUsers": true
      }
    }
  }
}
```

**CORS Configuration for Development:**

```csharp
// src/SuperAuth.API/Program.cs
public static void Main(string[] args)
{
    var builder = WebApplication.CreateBuilder(args);
    
    if (builder.Environment.IsDevelopment())
    {
        // Allow any origin in development
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("DevelopmentCors", policy =>
            {
                policy
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithExposedHeaders("X-SuperAuth-Analysis-Id", "X-Request-Id");
            });
        });
    }
    
    var app = builder.Build();
    
    if (app.Environment.IsDevelopment())
    {
        app.UseCors("DevelopmentCors");
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "SuperAuth API V1");
            c.RoutePrefix = "swagger";
            c.EnableTryItOutByDefault();
        });
    }
}
```

### **Development Authentication Bypass**

```csharp
// src/SuperAuth.Infrastructure/Security/DevelopmentAuthenticationHandler.cs
public class DevelopmentAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    public DevelopmentAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
        : base(options, logger, encoder, clock)
    {
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Context.RequestServices.GetRequiredService<IWebHostEnvironment>().IsDevelopment())
        {
            return Task.FromResult(AuthenticateResult.NoResult());
        }

        // Check for development bypass header
        if (Request.Headers.TryGetValue("X-SuperAuth-Dev-User", out var userIdValues))
        {
            var userId = userIdValues.FirstOrDefault();
            if (!string.IsNullOrEmpty(userId))
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userId),
                    new Claim(ClaimTypes.Name, $"Dev User {userId}"),
                    new Claim(ClaimTypes.Email, $"dev.user.{userId}@superauth.dev"),
                    new Claim("superauth:dev_mode", "true")
                };

                var identity = new ClaimsIdentity(claims, "Development");
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, "Development");

                return Task.FromResult(AuthenticateResult.Success(ticket));
            }
        }

        return Task.FromResult(AuthenticateResult.NoResult());
    }
}
```

-----

## 🧪 Testing Configuration

### **Test Database Setup**

```bash
# Create test database
createdb -h localhost -U superauth superauth_test

# Run migrations for test database
cd src/SuperAuth.API
dotnet ef database update --connection "Host=localhost;Database=superauth_test;Username=superauth;Password=superauth123"
```

**Test Configuration:**

```csharp
// tests/SuperAuth.IntegrationTests/TestWebApplicationFactory.cs
public class SuperAuthTestFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration((context, config) =>
        {
            config.AddInMemoryCollection(new Dictionary<string, string>
            {
                ["ConnectionStrings:DefaultConnection"] = "Host=localhost;Database=superauth_test;Username=superauth;Password=superauth123",
                ["SuperAuth:Security:JwtSettings:SecretKey"] = "test-secret-key-for-integration-tests-min-32-chars",
                ["SuperAuth:Security:AllowInsecureHttp"] = "true",
                ["SuperAuth:Security:SecurityAnalysis:MockMode"] = "true"
            });
        });

        builder.ConfigureServices(services =>
        {
            // Remove production DbContext
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<SuperAuthDbContext>));
            if (descriptor != null)
                services.Remove(descriptor);

            // Add test DbContext
            services.AddDbContext<SuperAuthDbContext>(options =>
            {
                options.UseNpgsql("Host=localhost;Database=superauth_test;Username=superauth;Password=superauth123");
                options.EnableSensitiveDataLogging();
            });

            // Mock external services
            services.AddScoped<IEmailService, MockEmailService>();
            services.AddScoped<ISmsService, MockSmsService>();
        });
    }
}
```

### **Running Tests**

```bash
# Run all tests
dotnet test

# Run specific test project
dotnet test tests/SuperAuth.UnitTests

# Run tests with coverage
dotnet test --collect:"XPlat Code Coverage"
reportgenerator -reports:"**/coverage.cobertura.xml" -targetdir:"coverage-report" -reporttypes:Html

# Run integration tests
dotnet test tests/SuperAuth.IntegrationTests --environment Testing

# Run E2E tests (requires running application)
cd tests/SuperAuth.E2ETests
npm test

# Watch mode for continuous testing
dotnet watch test tests/SuperAuth.UnitTests
```

-----

## 📊 Development Monitoring

### **Structured Logging with Seq**

```csharp
// src/SuperAuth.API/Program.cs
public static void Main(string[] args)
{
    var builder = WebApplication.CreateBuilder(args);
    
    // Configure Serilog for structured logging
    builder.Host.UseSerilog((context, configuration) =>
    {
        configuration
            .ReadFrom.Configuration(context.Configuration)
            .Enrich.FromLogContext()
            .Enrich.WithMachineName()
            .Enrich.WithThreadId()
            .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}")
            .WriteTo.Seq("http://localhost:5341")
            .WriteTo.File("logs/superauth-.log", rollingInterval: RollingInterval.Day);
    });
}
```

**Custom Logging Middleware:**

```csharp
// src/SuperAuth.API/Middleware/RequestLoggingMiddleware.cs
public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var requestId = Guid.NewGuid().ToString();
        context.Items["RequestId"] = requestId;
        
        using (_logger.BeginScope(new Dictionary<string, object>
        {
            ["RequestId"] = requestId,
            ["UserId"] = context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value,
            ["UserAgent"] = context.Request.Headers["User-Agent"].ToString(),
            ["IpAddress"] = context.Connection.RemoteIpAddress?.ToString()
        }))
        {
            var stopwatch = Stopwatch.StartNew();
            
            _logger.LogInformation("Request started: {Method} {Path}",
                context.Request.Method, context.Request.Path);

            try
            {
                await _next(context);
            }
            finally
            {
                stopwatch.Stop();
                
                _logger.LogInformation("Request completed: {Method} {Path} {StatusCode} in {ElapsedMs}ms",
                    context.Request.Method,
                    context.Request.Path,
                    context.Response.StatusCode,
                    stopwatch.ElapsedMilliseconds);
            }
        }
    }
}
```

### **Application Insights Integration**

```csharp
// src/SuperAuth.API/Program.cs - Application Insights
builder.Services.AddApplicationInsightsTelemetry(options =>
{
    options.ConnectionString = builder.Configuration.GetConnectionString("ApplicationInsights");
    options.EnableAdaptiveSampling = true;
    options.EnableQuickPulseMetricStream = true;
});

// Custom telemetry initializer
builder.Services.AddSingleton<ITelemetryInitializer, SuperAuthTelemetryInitializer>();
```

```csharp
// src/SuperAuth.Infrastructure/Telemetry/SuperAuthTelemetryInitializer.cs
public class SuperAuthTelemetryInitializer : ITelemetryInitializer
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public SuperAuthTelemetryInitializer(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public void Initialize(ITelemetry telemetry)
    {
        var context = _httpContextAccessor.HttpContext;
        if (context != null)
        {
            telemetry.Context.User.Id = context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            telemetry.Context.Session.Id = context.Session?.Id;
            
            if (context.Items.TryGetValue("RequestId", out var requestId))
            {
                telemetry.Context.Operation.Id = requestId.ToString();
            }
            
            // Add custom properties
            if (telemetry is ISupportProperties supportProperties)
            {
                supportProperties.Properties["SuperAuth.Version"] = "1.0.0";
                supportProperties.Properties["SuperAuth.Environment"] = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            }
        }
    }
}
```

-----

## 📁 Project Structure Best Practices

### **Recommended Folder Organization**

```
src/SuperAuth.API/
├── Controllers/
│   ├── v1/                          # API version 1
│   │   ├── AuthController.cs
│   │   ├── UsersController.cs
│   │   └── SecurityController.cs
│   └── v2/                          # Future API version 2
├── Middleware/
│   ├── RequestLoggingMiddleware.cs
│   ├── ErrorHandlingMiddleware.cs
│   └── SecurityHeadersMiddleware.cs
├── Filters/
│   ├── ValidationFilter.cs
│   ├── AuthorizationFilter.cs
│   └── RateLimitFilter.cs
├── Extensions/
│   ├── ServiceCollectionExtensions.cs
│   ├── ApplicationBuilderExtensions.cs
│   └── ClaimsPrincipalExtensions.cs
├── Configuration/
│   ├── SwaggerConfiguration.cs
│   ├── CorsConfiguration.cs
│   └── AuthenticationConfiguration.cs
└── Program.cs
```

### **Configuration Management**

```csharp
// src/SuperAuth.API/Configuration/SuperAuthSettings.cs
public class SuperAuthSettings
{
    public const string SectionName = "SuperAuth";
    
    public SecuritySettings Security { get; set; } = new();
    public DatabaseSettings Database { get; set; } = new();
    public CacheSettings Cache { get; set; } = new();
    public EmailSettings Email { get; set; } = new();
    public LoggingSettings Logging { get; set; } = new();
}

public class SecuritySettings
{
    public JwtSettings JwtSettings { get; set; } = new();
    public bool AllowInsecureHttp { get; set; }
    public bool RequireHttps { get; set; } = true;
    public PasswordPolicySettings PasswordPolicy { get; set; } = new();
    public RateLimitingSettings RateLimiting { get; set; } = new();
}

// Usage in Program.cs
builder.Services.Configure<SuperAuthSettings>(
    builder.Configuration.GetSection(SuperAuthSettings.SectionName));
```

### **Dependency Injection Organization**

```csharp
// src/SuperAuth.API/Extensions/ServiceCollectionExtensions.cs
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSuperAuthServices(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        services
            .AddSuperAuthCore()
            .AddSuperAuthInfrastructure(configuration)
            .AddSuperAuthSecurity(configuration)
            .AddSuperAuthSwagger()
            .AddSuperAuthCors(configuration);
            
        return services;
    }
    
    private static IServiceCollection AddSuperAuthCore(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ISecurityAnalysisService, SecurityAnalysisService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        
        return services;
    }
    
    private static IServiceCollection AddSuperAuthSecurity(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection("SuperAuth:Security:JwtSettings").Get<JwtSettings>();
        
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
                };
            });
            
        return services;
    }
}
```

-----

## 🎯 Development Success Checklist

### **Environment Setup Verification**

Use this checklist to verify your development environment is properly configured:

#### **✅ Infrastructure Services**

```bash
# Check all services are running
docker-compose -f docker-compose.dev.yml ps

# Verify database connectivity
psql -h localhost -U superauth -d superauth_dev -c "SELECT version();"

# Test Redis connectivity
redis-cli -h localhost ping

# Test Qdrant connectivity
curl http://localhost:6333/health
```

#### **✅ Backend Services**

```bash
# API health check
curl -k https://localhost:7001/health

# Authentication test
curl -k -X POST https://localhost:7001/api/v1/auth/login \
  -H "Content-Type: application/json" \
  -d '{"email":"admin@superauth.io","password":"SuperAuth123!"}'

# Swagger documentation accessible
open https://localhost:7001/swagger
```

#### **✅ Frontend Services**

```bash
# Dashboard accessibility
curl -I http://localhost:4200

# User portal accessibility  
curl -I http://localhost:4201

# Login flow works end-to-end
# 1. Navigate to http://localhost:4200
# 2. Login with admin@superauth.io / SuperAuth123!
# 3. Dashboard loads successfully
```

#### **✅ Development Tools**

```bash
# pgAdmin accessible
open http://localhost:5050

# Redis Commander accessible
open http://localhost:8081

# Seq logging accessible
open http://localhost:5341

# MailHog email testing accessible
open http://localhost:8025
```

#### **✅ Code Quality Tools**

```bash
# .NET formatting works
dotnet format --verify-no-changes

# Build succeeds without warnings
dotnet build --no-restore --verbosity normal

# Tests pass
dotnet test --no-build

# Angular linting passes
cd src/SuperAuth.Dashboard && npm run lint
cd ../SuperAuth.UserPortal && npm run lint
```

### **Performance Baseline**

Ensure your development environment meets these performance baselines:

|Metric             |Target                   |Command to Test                                              |
|-------------------|-------------------------|-------------------------------------------------------------|
|API Response Time  |< 100ms local            |`curl -w "@curl-format.txt" -k https://localhost:7001/health`|
|Database Query Time|< 50ms for simple queries|Check pgAdmin query performance                              |
|Frontend Build Time|< 30 seconds             |`time ng build --configuration development`                  |
|Hot Reload Time    |< 5 seconds              |Make a change and observe reload time                        |
|Test Execution Time|< 2 minutes all tests    |`time dotnet test`                                           |

**curl-format.txt for response time testing:**

```
     time_namelookup:  %{time_namelookup}s\n
        time_connect:  %{time_connect}s\n
     time_appconnect:  %{time_appconnect}s\n
    time_pretransfer:  %{time_pretransfer}s\n
       time_redirect:  %{time_redirect}s\n
  time_starttransfer:  %{time_starttransfer}s\n
                     ----------\n
          time_total:  %{time_total}s\n
```

-----

## 🎉 Congratulations!

You now have a fully configured SuperAuth development environment!

### **What You’ve Accomplished**

✅ **Complete development stack** running locally  
✅ **Hot reload** for both backend and frontend  
✅ **Database** with migrations and seed data  
✅ **Monitoring and logging** tools configured  
✅ **Testing framework** ready for TDD/BDD  
✅ **Performance optimizations** for smooth development

### **Next Steps**

1. **🏗️ Start Development**: Check out the [Coding Standards](coding-standards.md) guide
1. **🧪 Write Tests**: Follow the [Testing Guide](testing-guide.md) for best practices
1. **🔄 Learn Git Workflow**: Read the [Git Workflow](git-workflow.md) documentation
1. **📚 Explore Architecture**: Deep dive into [Architecture Overview](../concepts/architecture-overview.md)
1. **🤝 Join Community**: Connect with other developers on [Discord](https://discord.gg/superauth)

### **Quick Reference**

**🔗 Essential URLs:**

- API: https://localhost:7001
- Dashboard: http://localhost:4200
- User Portal: http://localhost:4201
- Swagger: https://localhost:7001/swagger
- pgAdmin: http://localhost:5050

**👤 Development Credentials:**

- Super Admin: admin@superauth.io / SuperAuth123!
- Test User: user@demo.superauth.io / Demo123!

**📞 Need Help?**

- 💬 [Discord Community](https://discord.gg/superauth)
- 📖 [Documentation](https://docs.superauth.io)
- 🐛 [GitHub Issues](https://github.com/superauth/superauth/issues)

**Happy coding with SuperAuth!** 🚀

-----

*Last updated: January 2025*  
*Version: 1.0.0*  
*Environment tested on: Windows 11, macOS Ventura, Ubuntu 22.04*
