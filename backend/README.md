# Backend - Dumuzi Ticket System

API REST desenvolvida em .NET 8 com Clean Architecture


## Stack 

- **.NET 8** - Framework principal
- **ASP.NET Core Web API** - Framework web
- **Entity Framework Core 8.0** - ORM
- **PostgreSQL** - Banco de dados relacional
- **Swagger/OpenAPI** - Documentação automática
- **Docker** - Containerização

## Pré-requisitos

- .NET 8 SDK
- PostgreSQL (ou Docker)
- Docker (opcional)

## 🚀 Execução e Demonstração

### Execução Rápida (Docker)
```bash
# Na raiz do projeto
docker-compose up backend postgres
```

### Execução Local (Desenvolvimento)
```bash
# Restaurar dependências
dotnet restore

# Aplicar migrations
dotnet ef database update

# Executar API
dotnet run
```

**API disponível em:** http://localhost:5166  

##  Endpoints Implementados

### Funcionários
```http
GET    /api/funcionarios        # Listar todos
GET    /api/funcionarios/{id}   # Buscar por ID
POST   /api/funcionarios        # Criar novo
PUT    /api/funcionarios/{id}   # Atualizar
DELETE /api/funcionarios/{id}   # Remover
```

### Exemplo de Response
```json
{
  "id": 1,
  "nome": "João Silva",
  "email": "joao@empresa.com",
  "cargo": "Desenvolvedor",
  "ativo": true,
  "createdAt": "2025-09-05T10:30:00Z"
}
```

##  Arquitetura Implementada

O projeto segue **Clean Architecture** com quatro camadas bem definidas:

```
src/
├── Domain/              # 🔵 Camada de Domínio
│   ├── Entities/        # Entidades de negócio
│   ├── Repository/      # Interfaces dos repositórios
│   ├── ENUM/           # Enumerações
│   ├── Exceptions/     # Exceções de domínio
│   └── Dto/            # DTOs de domínio
├── Application/         # 🟢 Camada de Aplicação
│   ├── Dto/            # Data Transfer Objects
│   └── Service/        # Services de aplicação
├── Infra/              # 🟡 Camada de Infraestrutura
│   └── Persistence/    # Entity Framework, DbContext
└── Presentation/       # 🔴 Camada de Apresentação
    └── Controllers/    # Controllers da API REST
```

### Princípios Aplicados
- **Dependency Inversion** - Camadas internas não dependem das externas
- **Single Responsibility** - Cada classe tem uma responsabilidade
- **Open/Closed** - Aberto para extensão, fechado para modificação
- **Repository Pattern** - Abstração do acesso a dados

## Decisões Técnicas e Implementações

### 1. Entity Framework Code First
```csharp
// Migrations automáticas para versionamento do schema
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### 2. Dependency Injection
```csharp
// Program.cs - Configuração de serviços
builder.Services.AddScoped<IFuncionarioRepository, FuncionarioRepository>();
builder.Services.AddScoped<IFuncionarioService, FuncionarioService>();
```

### 3. DTOs para Transferência de Dados
```csharp
public class FuncionarioCreateDto
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Cargo { get; set; }
}
```

### 4. Repository Pattern
```csharp
public interface IFuncionarioRepository
{
    Task<IEnumerable<Funcionario>> GetAllAsync();
    Task<Funcionario> GetByIdAsync(int id);
    Task<Funcionario> CreateAsync(Funcionario funcionario);
    // ... outros métodos
}
```

##  Configurações e Ambiente

### appsettings.json
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=postgres;Port=5432;Database=dumuzi_db;Username=dumuzi_user;Password=dumuzi_pass"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

### Docker Configuration
```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5166

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
# Multi-stage build para otimização
```