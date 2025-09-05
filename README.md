# Dumuzi Ticket System

Sistema de gerenciamento de tickets

## Stack

### Backend
- **ASP.NET Core Web API**
- **Entity Framework Core** 
- **PostgreSQL**
- **Clean Architecture**
- **Docker** 

### Frontend
- **Next.js 15**
- **React 19** 
- **TypeScript**
- **Tailwind CSS 4** 
- **Radix UI** 

##  Como Executar

### Execução Rápida (Docker)
```bash
git clone https://github.com/AllysomFernando/dumuzi-ticket.git
cd dumuzi-ticket
docker-compose up --build
```

**Acesso aos serviços:**
- Frontend: http://localhost:3000
- Backend API: http://localhost:5166


##  Estrutura de Arquivos
```
dumuzi-ticket/
├── backend/             
│   ├── src/            
│   │   ├── Domain/     
│   │   ├── Application/ 
│   │   ├── Infra/       
│   │   └── Presentation/
│   ├── Migrations/      
│   └── Dockerfile       
├── frontend/            
│   ├── src/            
│   │   ├── app/        
│   │   ├── components/ 
│   │   ├── services/   
│   │   └── types/      
│   └── Dockerfile      
└── docker-compose.yml  
```

**Desenvolvido por:** Allysom Fernando  
**GitHub:** [@AllysomFernando](https://github.com/AllysomFernando)  
**Repositório:** [dumuzi-ticket](https://github.com/AllysomFernando/dumuzi-ticket)
