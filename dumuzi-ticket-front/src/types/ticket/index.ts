import { FuncionarioDTO } from "../funcionario";

export interface TicketDTO {
  id: number;
  funcionario: FuncionarioDTO;
  quantidade: number;
  situacao: 'A' | 'I'; // A = Ativo, I = Inativo
  dataEntrega: string; // Data de entrega do ticket
  updatedAt: string; // Data de última alteração
}

export interface CreateTicketDTO {
  funcionarioId: number;
  quantidade: number;
  // situacao sempre será 'A' na criação
  // dataEntrega será automaticamente definida pelo backend
}

export interface UpdateTicketDTO {
  funcionarioId?: number;
  quantidade?: number;
  situacao?: 'A' | 'I';
}