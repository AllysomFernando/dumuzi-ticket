import React from 'react';
import { User, Edit, ToggleLeft, ToggleRight, Calendar } from 'lucide-react';
import { Button } from '@/components/ui/button';
import { FuncionarioDTO } from '@/types/funcionario';
import { EmptyState } from './empty-state';

interface FuncionarioTableProps {
  funcionarios: FuncionarioDTO[];
  onEdit: (funcionario: FuncionarioDTO) => void;
  onToggleStatus: (funcionario: FuncionarioDTO) => void;
}

export const FuncionarioTable = ({ funcionarios, onEdit, onToggleStatus }: FuncionarioTableProps) => {
  const formatCPF = (cpf: string) => {
    return cpf.replace(/(\d{3})(\d{3})(\d{3})(\d{2})/, '$1.$2.$3-$4');
  };

  const formatDate = (dateString: string) => {
    return new Date(dateString).toLocaleDateString('pt-BR', {
      day: '2-digit',
      month: '2-digit',
      year: 'numeric',
      hour: '2-digit',
      minute: '2-digit'
    });
  };

  const getSituacaoDisplay = (situacao: 'A' | 'I') => {
    return situacao === 'A' ? 'Ativo' : 'Inativo';
  };

  const getSituacaoColor = (situacao: 'A' | 'I') => {
    return situacao === 'A' 
      ? 'bg-green-100 text-green-800' 
      : 'bg-red-100 text-red-800';
  };

  return (
    <div className="bg-white rounded-lg shadow-sm border border-gray-200">
      <div className="px-6 py-4 border-b border-gray-200">
        <h2 className="text-xl font-semibold text-gray-900">Lista de Funcionários</h2>
      </div>
      
      <div className="overflow-x-auto">
        <table className="min-w-full divide-y divide-gray-200">
          <thead className="bg-gray-50">
            <tr>
              <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                Funcionário
              </th>
              <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                CPF
              </th>
              <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                Situação
              </th>
              <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                Última Alteração
              </th>
              <th className="px-6 py-3 text-right text-xs font-medium text-gray-500 uppercase tracking-wider">
                Ações
              </th>
            </tr>
          </thead>
          <tbody className="bg-white divide-y divide-gray-200">
            {funcionarios.map((funcionario) => (
              <tr key={funcionario.id} className="hover:bg-gray-50">
                <td className="px-6 py-4 whitespace-nowrap">
                  <div className="flex items-center">
                    <div className="flex-shrink-0 h-10 w-10">
                      <div className="h-10 w-10 rounded-full bg-blue-100 flex items-center justify-center">
                        <User className="h-6 w-6 text-blue-600" />
                      </div>
                    </div>
                    <div className="ml-4">
                      <div className="text-sm font-medium text-gray-900">
                        {funcionario.nome}
                      </div>
                      <div className="text-sm text-gray-500">
                        ID: {funcionario.id}
                      </div>
                    </div>
                  </div>
                </td>
                <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-900">
                  {formatCPF(funcionario.cpf)}
                </td>
                <td className="px-6 py-4 whitespace-nowrap">
                  <span className={`inline-flex items-center px-2.5 py-0.5 rounded-full text-xs font-medium ${getSituacaoColor(funcionario.situacao)}`}>
                    {funcionario.situacao === 'A' ? (
                      <ToggleRight className="w-3 h-3 mr-1" />
                    ) : (
                      <ToggleLeft className="w-3 h-3 mr-1" />
                    )}
                    {getSituacaoDisplay(funcionario.situacao)}
                  </span>
                </td>
                <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-900">
                  <div className="flex items-center">
                    <Calendar className="w-4 h-4 mr-1 text-gray-400" />
                    {formatDate(funcionario.dataAlteracao)}
                  </div>
                </td>
                <td className="px-6 py-4 whitespace-nowrap text-right text-sm font-medium">
                  <div className="flex justify-end gap-2">
                    <Button
                      variant="outline"
                      size="sm"
                      onClick={() => onEdit(funcionario)}
                      className="flex items-center gap-1"
                    >
                      <Edit className="w-3 h-3" />
                      Editar
                    </Button>
                    <Button
                      variant={funcionario.situacao === 'A' ? 'destructive' : 'default'}
                      size="sm"
                      onClick={() => onToggleStatus(funcionario)}
                      className="flex items-center gap-1"
                    >
                      {funcionario.situacao === 'A' ? (
                        <>
                          <ToggleLeft className="w-3 h-3" />
                          Inativar
                        </>
                      ) : (
                        <>
                          <ToggleRight className="w-3 h-3" />
                          Ativar
                        </>
                      )}
                    </Button>
                  </div>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
        
        {funcionarios.length === 0 && (
          <EmptyState 
            title="Nenhum funcionário encontrado"
            description="Comece cadastrando um novo funcionário."
          />
        )}
      </div>
    </div>
  );
};
