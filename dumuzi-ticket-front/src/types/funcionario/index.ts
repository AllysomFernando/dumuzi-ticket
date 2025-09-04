export interface FuncionarioDTO {
  id: number;
  nome: string;
  cpf: string;
  situacao: 'A' | 'I'; // A = Ativo, I = Inativo
  dataAlteracao: string;
}

export interface CreateFuncionarioDTO {
  nome: string;
  cpf: string;
  situacao: 'A'; // Sempre ativo na criação
}

export interface UpdateFuncionarioDTO {
  nome?: string;
  cpf?: string;
  situacao?: 'A' | 'I';
}