# Backend - Dumuzi Ticket System

API REST desenvolvida em .NET 8 com Clean Architecture

## Stack 

- **.NET 8** 
- **ASP.NET Core Web API** 
- **Entity Framework Core 8.0**
- **PostgreSQL** 
- **Swagger/OpenAPI**
- **Docker**

**API disponível em:** http://localhost:5166  


##  Arquitetura Implementada

O projeto segue **Clean Architecture** com quatro camadas bem definidas:

```
src/
├── Domain/             
│   ├── Entities/        
│   ├── Repository/      
│   ├── ENUM/           
│   ├── Exceptions/    
│   └── Dto/            
├── Application/         
│   ├── Dto/            
│   └── Service/        
├── Infra/              
│   └── Persistence/    
└── Presentation/      
    └── Controllers/   
```


### Docker Configuration
```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5166

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
# Multi-stage build para otimização
```