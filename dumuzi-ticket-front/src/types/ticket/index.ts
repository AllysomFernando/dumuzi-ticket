import { FuncionarioDTO } from "../funcionario";

export interface TicketDTO {
  id: number;
  funcionario: FuncionarioDTO;
  quantidade: number;
  updatedAt: string;
}

export interface CreateTicketDTO {
  funcionarioId: number;
  quantidade: number;
}

export interface UpdateTicketDTO {
  funcionarioId: number;
  quantidade: number;
}