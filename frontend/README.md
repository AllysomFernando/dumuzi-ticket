# Frontend - Dumuzi Ticket System

Interface web em Next e React;

##  Stack

- **Next.js 15** 
- **React 19** 
- **TypeScript** 
- **Tailwind CSS 4** 
- **Radix UI** 
- **Axios** 
- **Lucide React** 

## � Execução e Demonstração

### Execução Rápida (Docker)
```bash
docker-compose up frontend
```

### Execução Loca
```bash
yarn install
yarn build && yarn start
```

**Aplicação disponível em:** http://localhost:3000

##  Arquitetura Frontend Moderna

### App Router
```
src/app/
├── layout.tsx         
├── page.tsx           
├── globals.css       
└── funcionarios/      
    ├── page.tsx       
    └── [id]/          
        └── page.tsx   
```

### Componentes Organizados
```
src/components/
├── ui/             
│   ├── button.tsx    
│   ├── dialog.tsx    
│   └── input.tsx    
├── dashboard/        
├── header/          
└── navigation/     
```

### TypeScript Configuration
```json
{
  "compilerOptions": {
    "target": "ES2017",
    "lib": ["dom", "dom.iterable", "ES6"],
    "strict": true,
    "moduleResolution": "node",
    "jsx": "preserve",
    "paths": {
      "@/*": ["./src/*"]
    }
  }
}
```