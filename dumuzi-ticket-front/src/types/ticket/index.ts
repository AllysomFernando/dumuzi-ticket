import { FuncionarioDTO } from "../funcionario";

export interface TicketDTO {
  id: number;
  funcionario: FuncionarioDTO;
  quantidade: number;
  situacao: 'A' | 'I'; 
  dataEntrega: string; 
  updatedAt: string; 
}

export interface CreateTicketDTO {
  funcionarioId: number;
  quantidade: number;

}

export interface UpdateTicketDTO {
  funcionarioId?: number;
  quantidade?: number;
  situacao?: 'A' | 'I';
}