# 🚀 SuperAuth Installation Guide

[![Installation Status](https://img.shields.io/badge/installation-tested-brightgreen.svg)](https://github.com/superauth/superauth/actions)
[![Platform Support](https://img.shields.io/badge/platform-Windows%20%7C%20macOS%20%7C%20Linux-blue.svg)](#platform-support)
[![Docker](https://img.shields.io/badge/docker-supported-blue.svg)](https://hub.docker.com/r/superauth/superauth)

**Get SuperAuth running in under 5 minutes.** This comprehensive guide covers all installation methods from quick Docker setup to production-ready Kubernetes deployment.

## 📋 Overview

SuperAuth offers multiple installation paths:

|Method                                                   |Best For                    |Time Required|Complexity|
|---------------------------------------------------------|----------------------------|-------------|----------|
|[🐳 Docker Compose](#-docker-compose-recommended)         |**Quick start, development**|2 minutes    |⭐         |
|[🏗️ Local Development](#%EF%B8%8F-local-development-setup)|**Active development**      |10 minutes   |⭐⭐        |
|[☁️ Cloud Deployment](#%EF%B8%8F-cloud-deployment)        |**Production staging**      |15 minutes   |⭐⭐⭐       |
|[⚙️ Kubernetes](#%EF%B8%8F-kubernetes-deployment)         |**Production scale**        |30 minutes   |⭐⭐⭐⭐      |

-----

## 🎯 Quick Decision Tree

```
┌─ Need SuperAuth running now? 
│  └─ Yes → Docker Compose (2 min)
│
├─ Planning to modify SuperAuth code?
│  └─ Yes → Local Development (10 min)
│
├─ Need production deployment?
│  ├─ Small team → Cloud Deployment (15 min)
│  └─ Enterprise → Kubernetes (30 min)
│
└─ Just exploring? → Docker Compose (2 min)
```

-----

## 📋 Prerequisites

### **System Requirements**

#### **Minimum Requirements**

- **CPU**: 2 cores
- **RAM**: 4 GB
- **Storage**: 10 GB free space
- **Network**: Internet connection for downloads

#### **Recommended Requirements**

- **CPU**: 4+ cores
- **RAM**: 8+ GB
- **Storage**: 20+ GB SSD
- **Network**: Stable internet (100+ Mbps)

### **Platform Support**

|Operating System |Status           |Notes                       |
|-----------------|-----------------|----------------------------|
|**Windows 10/11**|✅ Fully Supported|WSL2 recommended for Docker |
|**macOS 10.15+** |✅ Fully Supported|Intel & Apple Silicon       |
|**Ubuntu 20.04+**|✅ Fully Supported|Primary development platform|
|**Debian 11+**   |✅ Supported      |                            |
|**CentOS 8+**    |✅ Supported      |                            |
|**Alpine Linux** |✅ Supported      |Container deployments       |

### **Required Software**

The following will be installed automatically if missing:

#### **For Docker Compose (Recommended)**

- [Docker Desktop](https://www.docker.com/products/docker-desktop/) 4.0+ or Docker Engine 20.0+
- [Docker Compose](https://docs.docker.com/compose/install/) 2.0+ (included in Docker Desktop)

#### **For Local Development**

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) 8.0.100+
- [Node.js](https://nodejs.org/) 18.0+ with npm 8.0+
- [PostgreSQL](https://www.postgresql.org/download/) 14+ (or Docker)
- [Redis](https://redis.io/download) 6.0+ (optional, for caching)

#### **For Cloud/Kubernetes**

- [kubectl](https://kubernetes.io/docs/tasks/tools/) 1.25+
- [Helm](https://helm.sh/docs/intro/install/) 3.8+
- Cloud CLI tools (optional):
  - [Azure CLI](https://docs.microsoft.com/en-us/cli/azure/install-azure-cli)
  - [AWS CLI](https://aws.amazon.com/cli/)
  - [Google Cloud SDK](https://cloud.google.com/sdk/docs/install)

-----

## 🐳 Docker Compose (Recommended)

**⏱️ Time Required: 2 minutes**  
**💡 Best for: Quick evaluation, development, demos**

This is the fastest way to get SuperAuth running with a complete stack including database, cache, and monitoring.

### **Step 1: Clone Repository**

```bash
# Clone the SuperAuth repository
git clone https://github.com/superauth/superauth.git
cd superauth

# Verify you're in the right directory
ls -la
# You should see: docker-compose.yml, src/, docs/, etc.
```

### **Step 2: Environment Configuration**

```bash
# Copy environment template
cp .env.example .env

# Edit configuration (optional for quick start)
# The defaults will work for local development
cat .env
```

**Default Configuration Preview:**

```bash
# SuperAuth Configuration
SUPERAUTH_DATABASE_URL=Host=postgres;Port=5432;Database=superauth_dev;Username=superauth;Password=superauth123
SUPERAUTH_REDIS_URL=redis:6379
SUPERAUTH_JWT_SECRET=your-super-secret-jwt-key-change-in-production
SUPERAUTH_API_URL=https://localhost:7001
SUPERAUTH_DASHBOARD_URL=http://localhost:4200

# Database Configuration
POSTGRES_DB=superauth_dev
POSTGRES_USER=superauth
POSTGRES_PASSWORD=superauth123

# Redis Configuration
REDIS_PASSWORD=superauth123
```

### **Step 3: Launch SuperAuth**

```bash
# Start all services (this will take 2-3 minutes on first run)
docker-compose up -d

# Watch the startup process (optional)
docker-compose logs -f
```

**Expected Output:**

```
✅ Creating network "superauth_superauth-network"
✅ Creating volume "superauth_postgres_dev_data"
✅ Creating volume "superauth_redis_dev_data"
✅ Creating superauth_postgres_1
✅ Creating superauth_redis_1
✅ Creating superauth_qdrant_1
✅ Creating superauth_api_1
✅ Creating superauth_dashboard_1

🚀 SuperAuth is starting up...
```

### **Step 4: Verify Installation**

```bash
# Check all services are running
docker-compose ps

# Expected output:
#         Name                     Command               State           Ports
# ---------------------------------------------------------------------------------
# superauth_api_1        dotnet SuperAuth.API.dll        Up      0.0.0.0:5000->5000/tcp, 0.0.0.0:5001->5001/tcp
# superauth_dashboard_1  nginx -g daemon off;             Up      0.0.0.0:4200->80/tcp
# superauth_postgres_1   docker-entrypoint.sh postgres   Up      0.0.0.0:5432->5432/tcp
# superauth_redis_1      docker-entrypoint.sh redis ...  Up      0.0.0.0:6379->6379/tcp
```

```bash
# Test API health
curl -k https://localhost:7001/health
# Expected: {"status":"Healthy","totalDuration":"00:00:00.0123456"}

# Test Dashboard
curl http://localhost:4200
# Expected: HTML response with SuperAuth dashboard
```

### **Step 5: Access SuperAuth**

🎉 **Congratulations!** SuperAuth is now running. Access it here:

|Service              |URL                             |Credentials                       |
|---------------------|--------------------------------|----------------------------------|
|**Admin Dashboard**  |<http://localhost:4200>         |admin@superauth.io / SuperAuth123!|
|**User Portal**      |<http://localhost:4201>         |user@demo.superauth.io / Demo123! |
|**API Endpoint**     |<https://localhost:7001>        |Bearer token required             |
|**API Documentation**|<https://localhost:7001/swagger>|Interactive API docs              |

### **Step 6: Create Your First Application**

```bash
# Using curl to create your first application
curl -X POST https://localhost:7001/api/v1/applications \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer $(curl -s -X POST https://localhost:7001/api/v1/auth/token \
    -H "Content-Type: application/x-www-form-urlencoded" \
    -d "grant_type=client_credentials&client_id=superauth-admin&client_secret=superauth-admin-secret" \
    | jq -r '.access_token')" \
  -d '{
    "name": "My First App",
    "description": "Getting started with SuperAuth",
    "redirectUris": ["http://localhost:3000/callback"],
    "grantTypes": ["authorization_code", "refresh_token"],
    "explainableSecurityEnabled": true
  }'
```

**Expected Response:**

```json
{
  "id": "app_1a2b3c4d5e6f",
  "name": "My First App", 
  "clientId": "client_1a2b3c4d5e6f",
  "clientSecret": "secret_abcdef123456",
  "redirectUris": ["http://localhost:3000/callback"],
  "createdAt": "2025-01-15T10:30:00Z"
}
```

### **Common Docker Issues & Solutions**

#### **Issue: Port Already in Use**

```bash
# Error: bind: address already in use
# Solution: Change ports in docker-compose.yml or stop conflicting services

# Check what's using the port
lsof -i :4200  # macOS/Linux
netstat -ano | findstr :4200  # Windows

# Stop conflicting service or modify docker-compose.yml
ports:
  - "4201:80"  # Change from 4200 to 4201
```

#### **Issue: Database Connection Failed**

```bash
# Check database container logs
docker-compose logs postgres

# Restart database if needed
docker-compose restart postgres
docker-compose logs -f postgres
```

#### **Issue: SSL Certificate Warnings**

```bash
# For development, accept self-signed certificates
# Chrome: Click "Advanced" → "Proceed to localhost (unsafe)"
# Firefox: Click "Advanced" → "Accept the Risk and Continue"

# Or disable SSL verification for testing
curl -k https://localhost:7001/health
```

-----

## 🏗️ Local Development Setup

**⏱️ Time Required: 10 minutes**  
**💡 Best for: Active development, debugging, customization**

This setup gives you full control over the SuperAuth source code for development and debugging.

### **Step 1: Verify Prerequisites**

```bash
# Check .NET SDK version
dotnet --version
# Required: 8.0.100 or higher

# Check Node.js version  
node --version
# Required: 18.0.0 or higher

# Check npm version
npm --version
# Required: 8.0.0 or higher

# Check PostgreSQL (if installing locally)
psql --version
# Required: 14.0 or higher
```

**Installing Missing Prerequisites:**

<details>
<summary><strong>Windows Installation</strong></summary>

```powershell
# Install via Chocolatey (recommended)
choco install dotnet-8.0-sdk nodejs postgresql

# Or download installers:
# .NET 8: https://dotnet.microsoft.com/download/dotnet/8.0
# Node.js: https://nodejs.org/en/download/
# PostgreSQL: https://www.postgresql.org/download/windows/
```

</details>

<details>
<summary><strong>macOS Installation</strong></summary>

```bash
# Install via Homebrew (recommended)
brew install dotnet nodejs postgresql@14

# Start PostgreSQL service
brew services start postgresql@14

# Or download installers:
# .NET 8: https://dotnet.microsoft.com/download/dotnet/8.0
# Node.js: https://nodejs.org/en/download/
```

</details>

<details>
<summary><strong>Ubuntu/Debian Installation</strong></summary>

```bash
# Install .NET 8 SDK
wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
sudo apt-get update
sudo apt-get install -y dotnet-sdk-8.0

# Install Node.js 18
curl -fsSL https://deb.nodesource.com/setup_18.x | sudo -E bash -
sudo apt-get install -y nodejs

# Install PostgreSQL
sudo apt-get install -y postgresql postgresql-contrib
sudo systemctl start postgresql
sudo systemctl enable postgresql
```

</details>

### **Step 2: Clone and Setup Repository**

```bash
# Clone repository
git clone https://github.com/superauth/superauth.git
cd superauth

# Restore .NET dependencies
dotnet restore

# Verify solution builds
dotnet build
# Expected: Build succeeded. 0 Warning(s). 0 Error(s).
```

### **Step 3: Database Setup**

#### **Option A: Using Docker (Recommended)**

```bash
# Start just the database services
docker-compose up -d postgres redis qdrant

# Verify databases are running
docker-compose ps postgres redis qdrant
```

#### **Option B: Local PostgreSQL**

```bash
# Create database and user
sudo -u postgres psql

-- In PostgreSQL prompt:
CREATE DATABASE superauth_dev;
CREATE USER superauth WITH PASSWORD 'superauth123';
GRANT ALL PRIVILEGES ON DATABASE superauth_dev TO superauth;
\q
```

#### **Option C: Using Connection String**

```bash
# Edit appsettings.Development.json
cd src/SuperAuth.API

# Update connection string for your local setup
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=superauth_dev;Username=superauth;Password=superauth123"
  }
}
```

### **Step 4: Run Database Migrations**

```bash
# Navigate to API project
cd src/SuperAuth.API

# Install Entity Framework tools (if not already installed)
dotnet tool install --global dotnet-ef

# Run database migrations
dotnet ef database update

# Verify migration success
dotnet ef migrations list
# Expected: Shows list of applied migrations
```

### **Step 5: Install Frontend Dependencies**

```bash
# Install Dashboard dependencies
cd src/SuperAuth.Dashboard
npm install

# Verify installation
npm list --depth=0
# Expected: No vulnerabilities found

# Install User Portal dependencies  
cd ../SuperAuth.UserPortal
npm install

# Return to root directory
cd ../..
```

### **Step 6: Start Development Servers**

**Terminal 1: Backend API**

```bash
cd src/SuperAuth.API

# Start API server with hot reload
dotnet watch run

# Expected output:
# info: Microsoft.Hosting.Lifetime[14]
#       Now listening on: https://localhost:7001
#       Now listening on: http://localhost:5000
# info: Microsoft.Hosting.Lifetime[0]
#       Application started. Press Ctrl+C to shut down.
```

**Terminal 2: Admin Dashboard**

```bash
cd src/SuperAuth.Dashboard

# Start development server
ng serve

# Expected output:
# ✔ Browser application bundle generation complete.
# Initial chunk files | Names         |  Raw size
# polyfills.js        | polyfills     | 314.27 kB | 
# main.js             | main          | 50.00 kB  |
# styles.css          | styles        | 95.33 kB  |
# 
# ** Angular Live Development Server is listening on localhost:4200 **
```

**Terminal 3: User Portal (Optional)**

```bash
cd src/SuperAuth.UserPortal

# Start development server  
ng serve --port 4201

# Expected output:
# ** Angular Live Development Server is listening on localhost:4201 **
```

### **Step 7: Verify Development Setup**

```bash
# Test API health
curl -k https://localhost:7001/health
# Expected: {"status":"Healthy"}

# Test dashboard
curl http://localhost:4200
# Expected: HTML response

# Check hot reload (modify a file and see auto-refresh)
```

### **Development Workflow Tips**

```bash
# Running tests
dotnet test                          # Backend tests
cd src/SuperAuth.Dashboard && npm test  # Frontend tests

# Code formatting
dotnet format                        # C# formatting
cd src/SuperAuth.Dashboard && npm run lint  # TypeScript linting

# Database reset (if needed)
dotnet ef database drop --force
dotnet ef database update

# Clean rebuild
dotnet clean && dotnet build
```

-----

## ☁️ Cloud Deployment

**⏱️ Time Required: 15 minutes**  
**💡 Best for: Staging environments, small production deployments**

Deploy SuperAuth to major cloud providers with one-click deployment options.

### **Azure Container Apps (Recommended)**

```bash
# Login to Azure
az login

# Create resource group
az group create --name superauth-rg --location eastus

# Deploy SuperAuth
az containerapp up \
  --source . \
  --name superauth \
  --resource-group superauth-rg \
  --environment superauth-env \
  --ingress external \
  --target-port 80 \
  --cpu 1.0 \
  --memory 2.0Gi

# Get the URL
az containerapp show \
  --name superauth \
  --resource-group superauth-rg \
  --query "properties.configuration.ingress.fqdn" \
  --output tsv
```

**Expected Output:**

```
✅ Resource group 'superauth-rg' created
✅ Container app 'superauth' created  
✅ Ingress enabled for container app 'superauth'

🌐 Your SuperAuth instance is available at:
https://superauth--abc123.victoriouscoast-a1b2c3d4.eastus.azurecontainerapps.io
```

### **AWS ECS Fargate**

```bash
# Configure AWS CLI
aws configure

# Create cluster
aws ecs create-cluster --cluster-name superauth-cluster

# Register task definition
aws ecs register-task-definition \
  --cli-input-json file://deployment/aws/task-definition.json

# Create service
aws ecs create-service \
  --cluster superauth-cluster \
  --service-name superauth \
  --task-definition superauth:1 \
  --desired-count 1 \
  --launch-type FARGATE \
  --network-configuration file://deployment/aws/network-config.json
```

### **Google Cloud Run**

```bash
# Enable required APIs
gcloud services enable run.googleapis.com
gcloud services enable cloudbuild.googleapis.com

# Deploy to Cloud Run
gcloud run deploy superauth \
  --source . \
  --platform managed \
  --region us-central1 \
  --allow-unauthenticated \
  --memory 2Gi \
  --cpu 1 \
  --max-instances 10

# Get service URL
gcloud run services describe superauth \
  --region us-central1 \
  --format 'value(status.url)'
```

### **Environment Configuration for Cloud**

```bash
# Set production environment variables
export SUPERAUTH_ENVIRONMENT=Production
export SUPERAUTH_DATABASE_URL="your-cloud-database-connection-string"
export SUPERAUTH_REDIS_URL="your-cloud-redis-connection-string"  
export SUPERAUTH_JWT_SECRET="your-production-jwt-secret-256-bits"
export SUPERAUTH_ALLOWED_ORIGINS="https://yourdomain.com"
```

-----

## ⚙️ Kubernetes Deployment

**⏱️ Time Required: 30 minutes**  
**💡 Best for: Production scale, enterprise deployments**

Deploy SuperAuth on Kubernetes with high availability, auto-scaling, and monitoring.

### **Prerequisites Check**

```bash
# Verify kubectl is configured
kubectl cluster-info
# Expected: Kubernetes control plane info

# Verify Helm is installed
helm version
# Expected: version.BuildInfo{Version:"v3.8+"}

# Check cluster resources
kubectl get nodes
kubectl top nodes  # Optional: shows resource usage
```

### **Step 1: Add SuperAuth Helm Repository**

```bash
# Add the SuperAuth Helm repository
helm repo add superauth https://charts.superauth.io
helm repo update

# Verify repository
helm search repo superauth
# Expected:
# NAME                    CHART VERSION   APP VERSION   DESCRIPTION
# superauth/superauth     1.0.0          1.0.0         SuperAuth authentication platform
```

### **Step 2: Create Namespace**

```bash
# Create dedicated namespace
kubectl create namespace superauth-system

# Set as default namespace (optional)
kubectl config set-context --current --namespace=superauth-system

# Verify namespace
kubectl get namespaces | grep superauth
```

### **Step 3: Configure Values**

```bash
# Download default values
helm show values superauth/superauth > values.yaml

# Edit values.yaml for your environment
cat > values-production.yaml << EOF
# SuperAuth Production Configuration
replicaCount: 3

image:
  repository: superauth/superauth
  tag: "1.0.0"
  pullPolicy: IfNotPresent

service:
  type: LoadBalancer
  port: 443
  targetPort: 80

ingress:
  enabled: true
  className: nginx
  annotations:
    cert-manager.io/cluster-issuer: letsencrypt-prod
  hosts:
    - host: auth.yourdomain.com
      paths:
        - path: /
          pathType: Prefix
  tls:
    - secretName: superauth-tls
      hosts:
        - auth.yourdomain.com

autoscaling:
  enabled: true
  minReplicas: 3
  maxReplicas: 10
  targetCPUUtilizationPercentage: 70
  targetMemoryUtilizationPercentage: 80

resources:
  limits:
    cpu: 1000m
    memory: 2Gi
  requests:
    cpu: 500m
    memory: 1Gi

postgresql:
  enabled: true
  auth:
    postgresPassword: "your-secure-postgres-password"
    database: superauth
  primary:
    persistence:
      enabled: true
      size: 20Gi
      storageClass: fast-ssd

redis:
  enabled: true
  auth:
    enabled: true
    password: "your-secure-redis-password"
  master:
    persistence:
      enabled: true
      size: 5Gi

monitoring:
  enabled: true
  serviceMonitor:
    enabled: true

security:
  podSecurityPolicy:
    enabled: true
  networkPolicy:
    enabled: true
EOF
```

### **Step 4: Deploy SuperAuth**

```bash
# Install SuperAuth with custom values
helm install superauth superauth/superauth \
  --namespace superauth-system \
  --values values-production.yaml \
  --wait \
  --timeout 10m

# Verify deployment
kubectl get pods -n superauth-system
# Expected: All pods should be Running

# Check services
kubectl get services -n superauth-system
# Expected: superauth service with external IP
```

### **Step 5: Verify Kubernetes Deployment**

```bash
# Check pod status
kubectl get pods -n superauth-system -o wide

# Check service endpoints
kubectl get endpoints -n superauth-system

# Test internal connectivity
kubectl run test-pod --image=curlimages/curl --rm -it --restart=Never -- \
  curl -k https://superauth.superauth-system.svc.cluster.local/health

# Check logs
kubectl logs -l app.kubernetes.io/name=superauth -n superauth-system --tail=50
```

### **Step 6: Setup Monitoring (Optional)**

```bash
# Install Prometheus and Grafana
helm repo add prometheus-community https://prometheus-community.github.io/helm-charts
helm repo update

# Install Prometheus
helm install prometheus prometheus-community/kube-prometheus-stack \
  --namespace monitoring \
  --create-namespace

# Access Grafana dashboard
kubectl port-forward -n monitoring svc/prometheus-grafana 3000:80
# Dashboard: http://localhost:3000 (admin/prom-operator)
```

### **Production Readiness Checklist**

```bash
# ✅ Security
kubectl get networkpolicies -n superauth-system
kubectl get podsecuritypolicies

# ✅ Persistence  
kubectl get pvc -n superauth-system

# ✅ Monitoring
kubectl get servicemonitor -n superauth-system

# ✅ Scaling
kubectl get hpa -n superauth-system

# ✅ SSL/TLS
kubectl get certificates -n superauth-system

# ✅ Backup
kubectl get cronjobs -n superauth-system
```

-----

## 🔧 Configuration

### **Environment Variables Reference**

|Variable                   |Description                 |Default               |Required|
|---------------------------|----------------------------|----------------------|--------|
|`SUPERAUTH_ENVIRONMENT`    |Runtime environment         |`Development`         |No      |
|`SUPERAUTH_DATABASE_URL`   |PostgreSQL connection string|localhost             |Yes     |
|`SUPERAUTH_REDIS_URL`      |Redis connection string     |localhost:6379        |No      |
|`SUPERAUTH_JWT_SECRET`     |JWT signing key (256-bit)   |auto-generated        |Yes     |
|`SUPERAUTH_API_URL`        |API base URL                |https://localhost:7001|Yes     |
|`SUPERAUTH_ALLOWED_ORIGINS`|CORS allowed origins        |*                     |Yes     |
|`SUPERAUTH_LOG_LEVEL`      |Logging level               |Information           |No      |

### **Database Configuration**

```bash
# PostgreSQL connection string format
SUPERAUTH_DATABASE_URL="Host=localhost;Port=5432;Database=superauth;Username=superauth;Password=yourpassword;SSL Mode=Require"

# Redis connection string format  
SUPERAUTH_REDIS_URL="localhost:6379,password=yourpassword,ssl=true"

# Qdrant vector database (optional)
SUPERAUTH_QDRANT_URL="http://localhost:6333"
```

### **Security Configuration**

```bash
# Generate secure JWT secret (256-bit)
openssl rand -base64 32

# Generate secure database password
openssl rand -base64 24

# Example production configuration
export SUPERAUTH_JWT_SECRET="$(openssl rand -base64 32)"
export SUPERAUTH_ALLOWED_ORIGINS="https://yourdomain.com,https://app.yourdomain.com"
export SUPERAUTH_REQUIRE_HTTPS="true"
export SUPERAUTH_HSTS_ENABLED="true"
```

-----

## ✅ Verification & Testing

### **Health Check Endpoints**

```bash
# Basic health check
curl -k https://localhost:7001/health
# Expected: {"status":"Healthy","totalDuration":"..."}

# Detailed health check
curl -k https://localhost:7001/health/ready
# Expected: Detailed component status

# Database connectivity
curl -k https://localhost:7001/health/db
# Expected: Database connection status
```

### **Smoke Tests**

```bash
# Test 1: API responds to requests
curl -k -w "%{http_code}" -o /dev/null -s https://localhost:7001/health
# Expected: 200

# Test 2: Dashboard loads
curl -w "%{http_code}" -o /dev/null -s http://localhost:4200
# Expected: 200

# Test 3: Database connection
curl -k -X POST https://localhost:7001/api/v1/health/db
# Expected: {"status":"Healthy"}

# Test 4: Authentication endpoint
curl -k -X POST https://localhost:7001/api/v1/auth/token \
  -H "Content-Type: application/x-www-form-urlencoded" \
  -d "grant_type=client_credentials&client_id=test&client_secret=test"
# Expected: Token response or authentication error
```

### **Performance Verification**

```bash
# Response time test
curl -k -w "@curl-format.txt" -o /dev/null -s https://localhost:7001/health

# Create curl-format.txt:
cat > curl-format.txt << EOF
     time_namelookup:  %{time_namelookup}\n
        time_connect:  %{time_connect}\n
     time_appconnect:  %{time_appconnect}\n
    time_pretransfer:  %{time_pretransfer}\n
       time_redirect:  %{time_redirect}\n
  time_starttransfer:  %{time_starttransfer}\n
                     ----------\n
          time_total:  %{time_total}\n
EOF

# Expected: time_total under 100ms for local deployments
```

-----

## 🚨 Troubleshooting

### **Common Installation Issues**

#### **Issue: Docker daemon not running**

```bash
# Symptoms
docker: Cannot connect to the Docker daemon at unix:///var/run/docker.sock

# Solutions
# Windows/Mac: Start Docker Desktop
# Linux: 
sudo systemctl start docker
sudo systemctl enable docker

# Verify fix
docker --version
```

#### **Issue: Port conflicts**

```bash
# Symptoms  
Error starting userland proxy: listen tcp4 0.0.0.0:5432: bind: address already in use

# Find what's using the port
lsof -i :5432  # macOS/Linux
netstat -ano | findstr :5432  # Windows

# Solutions
# Option 1: Stop conflicting service
sudo systemctl stop postgresql

# Option 2: Change port in docker-compose.yml
services:
  postgres:
    ports:
      - "5433:5432"  # Use different external port
```

#### **Issue: SSL certificate errors**

```bash
# Symptoms
curl: (60) SSL certificate problem: self-signed certificate

# Solutions
# For development: ignore SSL errors
curl -k https://localhost:7001/health

# For production: use proper certificates
# See deployment guides for SSL setup
```

#### **Issue: Database migration failures**

```bash
# Symptoms
Microsoft.EntityFrameworkCore.Database.Command[20102]
Failed executing DbCommand

# Solutions
# Check database connectivity
docker-compose logs postgres

# Reset database
docker-compose down -v  # Warning: deletes data!
docker-compose up -d

# Manual migration
cd src/SuperAuth.API
dotnet ef database drop --force
dotnet ef database update
```

#### **Issue: Frontend build failures**

```bash
# Symptoms
npm ERR! peer dep missing: @angular/core

# Solutions
# Clear npm cache
npm cache clean --force

# Delete node_modules and reinstall
rm -rf node_modules package-lock.json
npm install

# Check Node.js version
node --version  # Should be 18.0+
```

### **Performance Issues**

#### **Slow API responses**

```bash
# Check container resources
docker stats

# Expected healthy metrics:
# CPU: < 50% under normal load
# Memory: < 80% of allocated
# Network: Stable I/O

# If resources are maxed:
docker-compose down
# Edit docker-compose.yml to increase resources:
deploy:
  resources:
    limits:
      memory: 4G
      cpus: '2.0'
```

#### **Database connection timeouts**

```bash
# Check PostgreSQL logs
docker-compose logs postgres | grep ERROR

# Increase connection limits in postgresql.conf
max_connections = 200
shared_buffers = 256MB
```

### **Getting Help**

If you’re still experiencing issues:

1. **📖 Check Documentation**: [docs.superauth.io](https://docs.superauth.io)
1. **💬 Community Support**: [Discord Community](https://discord.gg/superauth)
1. **🐛 Report Issues**: [GitHub Issues](https://github.com/superauth/superauth/issues)
1. **📧 Email Support**: support@superauth.io
1. **📞 Enterprise Support**: enterprise@superauth.io (for commercial customers)

### **Diagnostic Information Collection**

When reporting issues, please include:

```bash
# System information
echo "=== System Information ==="
uname -a
docker --version
docker-compose --version
dotnet --version
node --version

# SuperAuth version
echo "=== SuperAuth Version ==="
curl -k https://localhost:7001/api/v1/version 2>/dev/null | jq .

# Container status
echo "=== Container Status ==="
docker-compose ps

# Recent logs (last 50 lines)
echo "=== Recent Logs ==="
docker-compose logs --tail=50

# Resource usage
echo "=== Resource Usage ==="
docker stats --no-stream
```

-----

## 🔄 Maintenance & Updates

### **Regular Maintenance Tasks**

#### **Update SuperAuth**

```bash
# Pull latest changes
git pull origin main

# Update Docker images
docker-compose pull

# Restart services
docker-compose down
docker-compose up -d

# For Kubernetes
helm upgrade superauth superauth/superauth --namespace superauth-system
```

#### **Database Backup**

```bash
# Create backup directory
mkdir -p backups/$(date +%Y%m%d)

# Backup PostgreSQL database
docker-compose exec postgres pg_dump -U superauth superauth_dev > \
  backups/$(date +%Y%m%d)/superauth_backup_$(date +%H%M).sql

# Backup Redis data
docker-compose exec redis redis-cli --rdb /data/backup.rdb
docker cp $(docker-compose ps -q redis):/data/backup.rdb \
  backups/$(date +%Y%m%d)/redis_backup_$(date +%H%M).rdb
```

#### **Log Rotation**

```bash
# Check log sizes
docker-compose logs --no-color 2>/dev/null | wc -l

# Clean old logs (Docker)
docker system prune -f
docker volume prune -f

# For Kubernetes
kubectl logs --previous -l app.kubernetes.io/name=superauth -n superauth-system
```

#### **Security Updates**

```bash
# Update base images
docker-compose pull
docker-compose up -d --force-recreate

# Check for security vulnerabilities
docker run --rm -v /var/run/docker.sock:/var/run/docker.sock \
  aquasec/trivy image superauth/superauth:latest

# Update dependencies
cd src/SuperAuth.API
dotnet list package --outdated
dotnet add package PackageName --version x.x.x
```

### **Backup and Restore Procedures**

#### **Complete System Backup**

```bash
#!/bin/bash
# backup-superauth.sh

BACKUP_DIR="backups/$(date +%Y%m%d_%H%M)"
mkdir -p "$BACKUP_DIR"

echo "🔄 Starting SuperAuth backup..."

# 1. Stop services gracefully
docker-compose stop superauth_api_1 superauth_dashboard_1

# 2. Backup database
echo "📊 Backing up PostgreSQL..."
docker-compose exec -T postgres pg_dump -U superauth -Fc superauth_dev > \
  "$BACKUP_DIR/postgres_backup.dump"

# 3. Backup Redis
echo "🗄️ Backing up Redis..."
docker-compose exec -T redis redis-cli BGSAVE
sleep 5
docker cp $(docker-compose ps -q redis):/data/dump.rdb "$BACKUP_DIR/"

# 4. Backup configuration
echo "⚙️ Backing up configuration..."
cp .env "$BACKUP_DIR/"
cp docker-compose.yml "$BACKUP_DIR/"

# 5. Backup volumes
echo "💾 Backing up volumes..."
docker run --rm -v superauth_postgres_dev_data:/source:ro \
  -v "$(pwd)/$BACKUP_DIR":/backup ubuntu \
  tar czf /backup/postgres_volume.tar.gz -C /source .

docker run --rm -v superauth_redis_dev_data:/source:ro \
  -v "$(pwd)/$BACKUP_DIR":/backup ubuntu \
  tar czf /backup/redis_volume.tar.gz -C /source .

# 6. Restart services
docker-compose start superauth_api_1 superauth_dashboard_1

echo "✅ Backup completed: $BACKUP_DIR"
```

#### **System Restore**

```bash
#!/bin/bash
# restore-superauth.sh

BACKUP_DIR="$1"

if [ -z "$BACKUP_DIR" ]; then
  echo "Usage: $0 <backup_directory>"
  exit 1
fi

echo "🔄 Starting SuperAuth restore from $BACKUP_DIR..."

# 1. Stop all services
docker-compose down

# 2. Remove existing volumes (WARNING: Data loss!)
docker volume rm superauth_postgres_dev_data superauth_redis_dev_data

# 3. Restore configuration
cp "$BACKUP_DIR/.env" .
cp "$BACKUP_DIR/docker-compose.yml" .

# 4. Start only database services
docker-compose up -d postgres redis

# 5. Wait for databases to be ready
echo "⏳ Waiting for databases to start..."
sleep 30

# 6. Restore PostgreSQL
echo "📊 Restoring PostgreSQL..."
docker-compose exec -T postgres pg_restore -U superauth -d superauth_dev < \
  "$BACKUP_DIR/postgres_backup.dump"

# 7. Restore Redis
echo "🗄️ Restoring Redis..."
docker cp "$BACKUP_DIR/dump.rdb" $(docker-compose ps -q redis):/data/
docker-compose restart redis

# 8. Start all services
docker-compose up -d

echo "✅ Restore completed successfully"
```

### **Monitoring Setup**

#### **Health Monitoring Script**

```bash
#!/bin/bash
# monitor-superauth.sh

WEBHOOK_URL="https://hooks.slack.com/your-webhook-url"  # Optional

check_service() {
  local service_name="$1"
  local url="$2"
  local expected_code="$3"
  
  http_code=$(curl -k -w "%{http_code}" -o /dev/null -s "$url" --max-time 10)
  
  if [ "$http_code" = "$expected_code" ]; then
    echo "✅ $service_name is healthy (HTTP $http_code)"
    return 0
  else
    echo "❌ $service_name is unhealthy (HTTP $http_code)"
    
    # Optional: Send alert to Slack
    if [ -n "$WEBHOOK_URL" ]; then
      curl -X POST -H 'Content-type: application/json' \
        --data "{\"text\":\"🚨 SuperAuth Alert: $service_name is unhealthy (HTTP $http_code)\"}" \
        "$WEBHOOK_URL"
    fi
    
    return 1
  fi
}

echo "🔍 SuperAuth Health Check - $(date)"
echo "=================================="

# Check API health
check_service "API Health" "https://localhost:7001/health" "200"

# Check Dashboard
check_service "Dashboard" "http://localhost:4200" "200"

# Check API authentication endpoint
check_service "Authentication" "https://localhost:7001/api/v1/auth/.well-known/openid-configuration" "200"

# Check database connectivity
check_service "Database" "https://localhost:7001/health/db" "200"

# Check container status
echo ""
echo "🐳 Container Status:"
docker-compose ps

# Check resource usage
echo ""
echo "📊 Resource Usage:"
docker stats --no-stream --format "table {{.Container}}\t{{.CPUPerc}}\t{{.MemUsage}}\t{{.MemPerc}}"

echo ""
echo "✅ Health check completed"
```

#### **Automated Monitoring with Cron**

```bash
# Add to crontab for regular monitoring
# crontab -e

# Check health every 5 minutes
*/5 * * * * /path/to/monitor-superauth.sh >> /var/log/superauth-monitor.log 2>&1

# Daily backup at 2 AM
0 2 * * * /path/to/backup-superauth.sh >> /var/log/superauth-backup.log 2>&1

# Weekly cleanup at 3 AM on Sundays
0 3 * * 0 docker system prune -f >> /var/log/superauth-cleanup.log 2>&1
```

-----

## 🚀 Next Steps

### **After Successful Installation**

Congratulations! 🎉 SuperAuth is now running. Here’s what to do next:

#### **1. Initial Configuration (5 minutes)**

- [ ] **[Create your first application](../getting-started/first-application.md)**
- [ ] **[Configure basic security policies](../how-to/configure-mfa.md)**
- [ ] **[Set up user management](../tutorials/user-management.md)**

#### **2. Integration with Your Apps (15-30 minutes)**

- [ ] **[Single Page App (SPA) Integration](../tutorials/spa-integration.md)** - React, Angular, Vue.js
- [ ] **[ASP.NET MVC Integration](../tutorials/mvc-integration.md)** - Server-side rendered apps
- [ ] **[REST API Integration](../tutorials/api-integration.md)** - Protect your APIs
- [ ] **[Mobile App Integration](../tutorials/mobile-integration.md)** - iOS, Android, React Native

#### **3. Production Preparation (1-2 hours)**

- [ ] **[Production Deployment Guide](../deployment/production-checklist.md)**
- [ ] **[Security Hardening](../security/security-best-practices.md)**
- [ ] **[Monitoring Setup](../operations/monitoring-alerting.md)**
- [ ] **[Backup Strategy](../operations/disaster-recovery.md)**

#### **4. Advanced Features (30-60 minutes each)**

- [ ] **[Single Sign-On (SSO) Setup](../tutorials/sso-setup.md)**
- [ ] **[Azure AD Integration](../how-to/azure-ad-integration.md)**
- [ ] **[Custom Security Policies](../tutorials/custom-security-policies.md)**
- [ ] **[Explainable Security Configuration](../concepts/explainable-security.md)**

### **Learning Resources**

#### **📚 Documentation Deep Dive**

- **[Architecture Overview](../concepts/architecture-overview.md)** - Understand how SuperAuth works
- **[Authentication Flow](../concepts/authentication-flow.md)** - OAuth 2.0 and OpenID Connect explained
- **[Multi-tenancy](../concepts/multi-tenancy.md)** - Support multiple organizations

#### **🎥 Video Tutorials** *(Coming Soon)*

- SuperAuth Quick Start (5 minutes)
- SPA Integration Walkthrough (15 minutes)
- Production Deployment Best Practices (30 minutes)

#### **💡 Sample Applications**

Explore our sample applications in the `samples/` directory:

- **ASP.NET MVC**: Complete MVC app with SuperAuth integration
- **Angular SPA**: Modern single-page application example
- **React SPA**: React-based authentication example
- **Mobile App**: React Native integration example

### **Community & Support**

#### **Join the Community**

- **💬 [Discord Server](https://discord.gg/superauth)** - Chat with other SuperAuth users
- **📺 [YouTube Channel](https://youtube.com/@superauth)** - Video tutorials and demos
- **📝 [Blog](https://blog.superauth.io)** - Latest updates and best practices
- **🐦 [Twitter](https://twitter.com/superauthio)** - Follow for announcements

#### **Getting Help**

- **🆘 [Troubleshooting Guide](../how-to/troubleshooting.md)** - Common issues and solutions
- **❓ [FAQ](../reference/faq.md)** - Frequently asked questions
- **🐛 [GitHub Issues](https://github.com/superauth/superauth/issues)** - Report bugs or request features
- **📧 [Email Support](mailto:support@superauth.io)** - Direct technical support

#### **Contributing**

SuperAuth is open source! We welcome contributions:

- **🔧 [Development Setup](../development/development-setup.md)** - Set up local development
- **📋 [Contributing Guide](../development/contribution-guide.md)** - How to contribute
- **🎨 [Coding Standards](../development/coding-standards.md)** - Code style guidelines

### **Migration from Other Platforms**

Already using another authentication provider? We’ve got you covered:

- **[Migrate from Okta](../how-to/migrate-from-okta.md)** - Complete migration guide
- **[Migrate from Auth0](../how-to/migrate-from-auth0.md)** - Step-by-step transition
- **[Migrate from Firebase Auth](../how-to/migrate-from-firebase.md)** - Firebase to SuperAuth
- **[Migrate from Keycloak](../how-to/migrate-from-keycloak.md)** - Open source to SuperAuth

### **Enterprise Features**

For enterprise deployments, consider:

- **🏢 [Enterprise Support](https://superauth.io/enterprise)** - 24/7 support and SLA
- **🔐 [Advanced Security](../security/enterprise-security.md)** - Enhanced security features
- **📊 [Analytics & Reporting](../operations/analytics.md)** - Detailed usage analytics
- **⚡ [High Availability](../deployment/high-availability.md)** - Multi-region deployments

-----

## 📊 Installation Success Metrics

Track your SuperAuth installation success:

### **Technical Metrics**

- ✅ **Installation Time**: Under 5 minutes (Docker Compose)
- ✅ **Health Check**: All endpoints return 200 OK
- ✅ **Response Time**: API responds under 100ms locally
- ✅ **Memory Usage**: Under 2GB for complete stack
- ✅ **First Authentication**: JWT token generated successfully

### **User Experience Metrics**

- ✅ **Dashboard Access**: Admin dashboard loads without errors
- ✅ **User Portal**: User portal accessible and functional
- ✅ **Documentation**: All links in this guide work correctly
- ✅ **Support**: Know where to get help if needed

### **Production Readiness** *(For production deployments)*

- ✅ **SSL/TLS**: HTTPS endpoints configured
- ✅ **Database**: Persistent storage configured
- ✅ **Backup**: Backup strategy implemented
- ✅ **Monitoring**: Health checks and alerts configured
- ✅ **Security**: Security best practices applied

-----

## 🎯 Quick Reference

### **Essential URLs** *(Local Development)*

|Service          |URL                           |Purpose                     |
|-----------------|------------------------------|----------------------------|
|Admin Dashboard  |http://localhost:4200         |Manage users, apps, security|
|User Portal      |http://localhost:4201         |End-user authentication     |
|API Endpoint     |https://localhost:7001        |REST API access             |
|API Documentation|https://localhost:7001/swagger|Interactive API docs        |
|Health Check     |https://localhost:7001/health |System health status        |

### **Default Credentials** *(Change in Production!)*

|Account    |Email                 |Password     |Purpose             |
|-----------|----------------------|-------------|--------------------|
|Super Admin|admin@superauth.io    |SuperAuth123!|Full system access  |
|Demo User  |user@demo.superauth.io|Demo123!     |Testing user account|

### **Essential Commands**

```bash
# Start SuperAuth
docker-compose up -d

# Stop SuperAuth
docker-compose down

# View logs
docker-compose logs -f

# Health check
curl -k https://localhost:7001/health

# Reset everything (⚠️ Data loss!)
docker-compose down -v && docker-compose up -d
```

-----

## 📝 Installation Report Template

Use this template to document your installation:

```markdown
# SuperAuth Installation Report

**Date**: ___________
**Environment**: [ ] Development [ ] Staging [ ] Production
**Installation Method**: [ ] Docker Compose [ ] Local Dev [ ] Cloud [ ] Kubernetes

## Pre-Installation
- [ ] Prerequisites verified
- [ ] System requirements met
- [ ] Network connectivity confirmed

## Installation Process
- **Start Time**: ___________
- **End Time**: ___________
- **Total Duration**: ___________
- **Issues Encountered**: ___________

## Post-Installation Verification
- [ ] Health check passes
- [ ] Dashboard accessible
- [ ] API responds correctly
- [ ] First authentication successful

## Configuration
- **Database**: ___________
- **Redis**: ___________
- **Domain**: ___________
- **SSL**: [ ] Enabled [ ] Disabled

## Next Steps
- [ ] Initial configuration completed
- [ ] First application created
- [ ] Team access configured
- [ ] Production checklist reviewed

## Notes
___________________________________________
___________________________________________
```

-----

**🎉 Congratulations on successfully installing SuperAuth!**

You’re now ready to revolutionize authentication for your applications. SuperAuth combines enterprise-grade security with developer-friendly APIs and user-centric design.

**Remember**: This installation guide is living documentation. If you find areas for improvement or encounter issues not covered here, please [contribute back to the community](https://github.com/superauth/superauth/blob/main/CONTRIBUTING.md).

**Happy authenticating!** 🚀

-----

*Last updated: January 2025*  
*Version: 1.0.0*  
*Tested on: Windows 11, macOS Ventura, Ubuntu 22.04*
