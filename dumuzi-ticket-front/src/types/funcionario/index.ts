export interface FuncionarioDTO {
  id: number;
  nome: string;
  cpf: string;
  situacao: 'A' | 'I';
  dataAlteracao: string;
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