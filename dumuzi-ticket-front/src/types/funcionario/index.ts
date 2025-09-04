export interface FuncionarioDTO {
  id: number;
  nome: string;
  cpf: string;
  situacao: 'A' | 'I';
  updatedAt: string;
}

export interface CreateFuncionarioDTO {
  nome: string;
  cpf: string;
  situacao: 'A';
}

export interface UpdateFuncionarioDTO {
  nome?: string;
  cpf?: string;
  situacao?: 'A' | 'I';
}