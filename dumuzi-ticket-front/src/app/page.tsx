"use client";
import React, { useState, useEffect } from 'react';
import { Plus, Filter, Calendar, User, Utensils } from 'lucide-react';
import { CreateTicketDTO, TicketDTO } from '@/types/ticket';
import { FuncionarioDTO } from '@/types/funcionario';
import { ticketService } from '@/services/ticket';
import { funcionarioService } from '@/services/funcionario';
import { Button } from '@/components/ui/button';


const TicketDashboard = () => {
  const [tickets, setTickets] = useState<TicketDTO[]>([]);
  const [funcionarios, setFuncionarios] = useState<FuncionarioDTO[]>([]);
  const [filteredTickets, setFilteredTickets] = useState<TicketDTO[]>([]);
  const [loading, setLoading] = useState(true);
  const [showCreateModal, setShowCreateModal] = useState(false);
  const [showFilterModal, setShowFilterModal] = useState(false);
  
  const [filterFuncionario, setFilterFuncionario] = useState('');
  const [filterDataInicial, setFilterDataInicial] = useState('');
  const [filterDataFinal, setFilterDataFinal] = useState('');
  const [filterTipoRefeicao, setFilterTipoRefeicao] = useState('');

  useEffect(() => {
    loadData();
  }, []);

  useEffect(() => {
    applyFilters();
  }, [tickets, filterFuncionario, filterDataInicial, filterDataFinal, filterTipoRefeicao]);

  const loadData = async () => {
    try {
      setLoading(true);
      const [ticketsData, funcionariosData] = await Promise.all([
        ticketService.getAll(),
        funcionarioService.getAll()
      ]);
      setTickets(ticketsData);
      setFuncionarios(funcionariosData);
    } catch (error) {
      console.error('Erro ao carregar dados:', error);
    } finally {
      setLoading(false);
    }
  };

  const applyFilters = () => {
    let filtered = tickets;

    if (filterFuncionario) {
      filtered = filtered.filter(ticket => 
        ticket.funcionario.id === parseInt(filterFuncionario)
      );
    }

    if (filterDataInicial) {
      filtered = filtered.filter(ticket => 
        new Date(ticket.updatedAt) >= new Date(filterDataInicial)
      );
    }

    if (filterDataFinal) {
      filtered = filtered.filter(ticket => 
        new Date(ticket.updatedAt) <= new Date(filterDataFinal)
      );
    }

    setFilteredTickets(filtered);
  };

  const clearFilters = () => {
    setFilterFuncionario('');
    setFilterDataInicial('');
    setFilterDataFinal('');
    setFilterTipoRefeicao('');

    setFilteredTickets(tickets);
    setShowFilterModal(false);
  };


  const getTotalTicketsByPeriod = () => {
    return filteredTickets.reduce((total, ticket) => total + ticket.quantidade, 0);
  };


  const formatDate = (dateString: string) => {
    return new Date(dateString).toLocaleDateString('pt-BR');
  };


  if (loading) {
    return (
      <div className="min-h-screen bg-gray-50 flex items-center justify-center">
        <div className="text-center">
          <div className="animate-spin rounded-full h-32 w-32 border-b-2 border-blue-600 mx-auto"></div>
          <p className="mt-4 text-gray-600">Carregando dados...</p>
        </div>
      </div>
    );
  }

  return (
    <div className="min-h-screen bg-gray-50">
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-8">
        <div className="bg-white rounded-lg shadow-sm border border-gray-200 p-6 mb-8">
          <div className="flex flex-col sm:flex-row justify-between items-start sm:items-center">
            <div>
              <h1 className="text-3xl font-bold text-gray-900 flex items-center">
                <Utensils className="mr-3 text-blue-600" size={32} />
                Gerenciamento de Tickets de Refeição
              </h1>
              <p className="text-gray-600 mt-2">
                Controle e visualize os tickets de refeição entregues para cada funcionário
              </p>
            </div>
            <div className="flex gap-3 mt-4 sm:mt-0">
              <Button
                variant="secondary"
                onClick={() => setShowFilterModal(true)}
                className="flex items-center px-4 py-2  rounded-lg  transition-colors"
              >
                <Filter size={16} className="mr-2" />
                Filtros
              </Button>

              <Button
                variant="destructive"
                onClick={clearFilters}
                className="flex items-center px-4 py-2 bg-gray-300rounded-lg transition-colors"
              >
                Limpar Filtros
              </Button>

              {/* {showFilterModal && (

              )} */}

              <Button
                variant="default"
                onClick={() => setShowCreateModal(true)}
                className="flex items-center px-4 py-2 text-white rounded-lg transition-colors"
              >
                <Plus size={16} className="mr-2" />
                Novo Ticket
              </Button>
            </div>
          </div>
        </div>

        <div className="grid grid-cols-1 md:grid-cols-3 gap-6 mb-8">
          <div className="bg-white rounded-lg shadow-sm border border-gray-200 p-6">
            <div className="flex items-center">
              <div className="flex-shrink-0">
                <div className="w-8 h-8 bg-blue-100 rounded-full flex items-center justify-center">
                  <Utensils className="w-5 h-5 text-blue-600" />
                </div>
              </div>
              <div className="ml-4">
                <p className="text-sm font-medium text-gray-600">Total de Tickets</p>
                <p className="text-2xl font-bold text-gray-900">{getTotalTicketsByPeriod()}</p>
              </div>
            </div>
          </div>

          <div className="bg-white rounded-lg shadow-sm border border-gray-200 p-6">
            <div className="flex items-center">
              <div className="flex-shrink-0">
                <div className="w-8 h-8 bg-green-100 rounded-full flex items-center justify-center">
                  <User className="w-5 h-5 text-green-600" />
                </div>
              </div>
              <div className="ml-4">
                <p className="text-sm font-medium text-gray-600">Funcionários Atendidos</p>
                <p className="text-2xl font-bold text-gray-900">
                  {new Set(filteredTickets.map(t => t.funcionario.id)).size}
                </p>
              </div>
            </div>
          </div>

          <div className="bg-white rounded-lg shadow-sm border border-gray-200 p-6">
            <div className="flex items-center">
              <div className="flex-shrink-0">
                <div className="w-8 h-8 bg-purple-100 rounded-full flex items-center justify-center">
                  <Calendar className="w-5 h-5 text-purple-600" />
                </div>
              </div>
              <div className="ml-4">
                <p className="text-sm font-medium text-gray-600">Período Filtrado</p>
                <p className="text-lg font-bold text-gray-900">
                  {filterDataInicial && filterDataFinal 
                    ? `${formatDate(filterDataInicial)} - ${formatDate(filterDataFinal)}`
                    : 'Todos os períodos'
                  }
                </p>
              </div>
            </div>
          </div>
        </div>

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
                {filteredTickets.map((ticket) => {
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
                              {funcionario?.nome|| 'Funcionário não encontrado'}
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
                        {formatDate(ticket.updatedAt)}
                      </td> 
                    </tr>
                  );
                })}
              </tbody>
            </table>
            {filteredTickets.length === 0 && (
              <div className="text-center py-12">
                <Utensils className="mx-auto h-12 w-12 text-gray-400" />
                <h3 className="mt-2 text-sm font-medium text-gray-900">Nenhum ticket encontrado</h3>
                <p className="mt-1 text-sm text-gray-500">
                  Comece criando um novo ticket de refeição.
                </p>
              </div>
            )}
          </div>
        </div>
      </div>
    </div>
  );
};

export default TicketDashboard;