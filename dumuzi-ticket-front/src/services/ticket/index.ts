import { CreateTicketDTO, TicketDTO, UpdateTicketDTO } from "@/types/ticket";
import { api } from "..";

export const ticketService = {
  getAll: async (): Promise<TicketDTO[]> => {
    const response = await api.get('/Ticket');
    return response.data;
  },

  getById: async (id: number): Promise<TicketDTO> => {
    const response = await api.get(`/Ticket/${id}`);
    return response.data;
  },

  getByFuncionarioId: async (funcionarioId: number): Promise<TicketDTO[]> => {
    const response = await api.get(`/Ticket/funcionario/${funcionarioId}`);
    return response.data;
  },

  getByFuncionarioRange: async (
    funcionarioId: number,
    dataInicial: string,
    dataFinal: string
  ): Promise<TicketDTO[]> => {
    const response = await api.get(
      `/Ticket/funcionario/${funcionarioId}/${dataInicial}/${dataFinal}`
    );
    return response.data;
  },

  create: async (ticket: CreateTicketDTO): Promise<TicketDTO> => {
    const response = await api.post('/Ticket', ticket);
    return response.data;
  },

  update: async (id: number, ticket: UpdateTicketDTO): Promise<TicketDTO> => {
    const response = await api.put(`/Ticket/${id}`, ticket);
    return response.data;
  },

  inativar: async (id: number): Promise<TicketDTO> => {
    const response = await api.patch(`/Ticket/deactivate/${id}`)
    return response.data
  },

  ativar: async (id: number): Promise<TicketDTO> => {
    const response = await api.patch(`/Ticket/activate/${id}`);
    return response.data
  }
};