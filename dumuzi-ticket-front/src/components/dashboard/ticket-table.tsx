import { User } from 'lucide-react';
import { TicketDTO } from '@/types/ticket';
import { FuncionarioDTO } from '@/types/funcionario';
import { EmptyState } from './empty-state';

interface TicketTableProps {
  tickets: TicketDTO[];
  funcionarios: FuncionarioDTO[];
}

export const TicketTable = ({ tickets, funcionarios }: TicketTableProps) => {
  const formatDate = (dateString: string) => {
    return new Date(dateString).toLocaleDateString('pt-BR');
  };

  return (
    <div className="bg-white rounded-lg shadow-sm border border-gray-200">
      <div className="px-6 py-4 border-b border-gray-200">
        <h2 className="text-xl font-semibold text-gray-900">Lista de Tickets</h2>
      </div>
      
      <div className="overflow-x-auto">
        <table className="min-w-full divide-y divide-gray-200">
          <thead className="bg-gray-50">
            <tr>
              <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                Funcionário
              </th>
              <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                Quantidade
              </th>
              <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                Data de Entrega
              </th>
            </tr>
          </thead>
          <tbody className="bg-white divide-y divide-gray-200">
            {tickets.map((ticket) => {
              const funcionario = funcionarios.find(f => f.id === ticket.funcionario.id);
              return (
                <tr key={ticket.id} className="hover:bg-gray-50">
                  <td className="px-6 py-4 whitespace-nowrap">
                    <div className="flex items-center">
                      <div className="flex-shrink-0 h-10 w-10">
                        <div className="h-10 w-10 rounded-full bg-blue-100 flex items-center justify-center">
                          <User className="h-6 w-6 text-blue-600" />
                        </div>
                      </div>
                      <div className="ml-4">
                        <div className="text-sm font-medium text-gray-900">
                          {funcionario?.nome || 'Funcionário não encontrado'}
                        </div>
                        <div className="text-sm text-gray-500">
                          {funcionario?.cpf}
                        </div>
                      </div>
                    </div>
                  </td>
                  <td className="px-6 py-4 whitespace-nowrap">
                    <span className="inline-flex items-center px-2.5 py-0.5 rounded-full text-xs font-medium bg-blue-100 text-blue-800">
                      {ticket.quantidade} tickets
                    </span>
                  </td>
                  <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-900">
                    {formatDate(ticket.dataEntrega || ticket.updatedAt)}
                  </td>
                </tr>
              );
            })}
          </tbody>
        </table>
        
        {tickets.length === 0 && <EmptyState />}
      </div>
    </div>
  );
};
