import { FuncionarioDTO } from "@/types/funcionario";
import { api } from "..";

export const funcionarioService = {
  getAll: async (): Promise<FuncionarioDTO[]> => {
    const response = await api.get('/Funcionario');
    return response.data;
  },

  getById: async (id: number): Promise<FuncionarioDTO> => {
    const response = await api.get(`/Funcionario/${id}`);
    return response.data;
  },

  create: async (funcionario: Omit<FuncionarioDTO, 'id'>): Promise<FuncionarioDTO> => {
    const response = await api.post('/Funcionario', funcionario);
    return response.data;
  },

  update: async (id: number, funcionario: Partial<FuncionarioDTO>): Promise<FuncionarioDTO> => {
    const response = await api.put(`/Funcionario/${id}`, funcionario);
    return response.data;
  },
};