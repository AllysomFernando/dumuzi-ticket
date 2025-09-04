import { FuncionarioDTO, CreateFuncionarioDTO, UpdateFuncionarioDTO } from "@/types/funcionario";
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

  getAtivos: async (): Promise<FuncionarioDTO[]> => {
    const response = await api.get('/Funcionario/ativos');
    return response.data;
  },

  create: async (funcionario: CreateFuncionarioDTO): Promise<FuncionarioDTO> => {
    const response = await api.post('/Funcionario', funcionario);
    return response.data;
  },

  update: async (id: number, funcionario: UpdateFuncionarioDTO): Promise<FuncionarioDTO> => {
    const response = await api.put(`/Funcionario/${id}`, funcionario);
    return response.data;
  },

  inativar: async (id: number): Promise<FuncionarioDTO> => {
    const response = await api.put(`/Funcionario/${id}/inativar`);
    return response.data;
  },

  ativar: async (id: number): Promise<FuncionarioDTO> => {
    const response = await api.put(`/Funcionario/${id}/ativar`);
    return response.data;
  },
};