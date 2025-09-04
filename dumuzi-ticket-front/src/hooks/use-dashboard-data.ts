import { useState, useEffect, useCallback } from 'react';
import { TicketDTO, CreateTicketDTO } from '@/types/ticket';
import { FuncionarioDTO } from '@/types/funcionario';
import { ticketService } from '@/services/ticket';
import { funcionarioService } from '@/services/funcionario';

export const useDashboardData = () => {
  const [tickets, setTickets] = useState<TicketDTO[]>([]);
  const [funcionarios, setFuncionarios] = useState<FuncionarioDTO[]>([]);
  const [filteredTickets, setFilteredTickets] = useState<TicketDTO[]>([]);
  const [loading, setLoading] = useState(true);
  
  const [filterFuncionario, setFilterFuncionario] = useState('');
  const [filterDataInicial, setFilterDataInicial] = useState('');
  const [filterDataFinal, setFilterDataFinal] = useState('');
  const [filterTipoRefeicao, setFilterTipoRefeicao] = useState('');

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

  const applyFilters = useCallback(() => {
    let filtered = tickets;

    if (filterFuncionario) {
      filtered = filtered.filter(ticket => 
        ticket.funcionario.id === parseInt(filterFuncionario)
      );
    }

    if (filterDataInicial) {
      filtered = filtered.filter(ticket => {
        const ticketDate = new Date(ticket.dataEntrega || ticket.updatedAt);
        const filterDate = new Date(filterDataInicial);
        return ticketDate >= filterDate;
      });
    }

    if (filterDataFinal) {
      filtered = filtered.filter(ticket => {
        const ticketDate = new Date(ticket.dataEntrega || ticket.updatedAt);
        const filterDate = new Date(filterDataFinal);
        filterDate.setHours(23, 59, 59, 999);
        return ticketDate <= filterDate;
      });
    }

    setFilteredTickets(filtered);
  }, [tickets, filterFuncionario, filterDataInicial, filterDataFinal]);

  useEffect(() => {
    loadData();
  }, []);

  useEffect(() => {
    applyFilters();
  }, [applyFilters]);

  const clearFilters = () => {
    setFilterFuncionario('');
    setFilterDataInicial('');
    setFilterDataFinal('');
    setFilterTipoRefeicao('');
  };

  const createTicket = async (ticketData: CreateTicketDTO) => {
    try {
      await ticketService.create(ticketData);
      await loadData();
    } catch (error) {
      console.error('Erro ao criar ticket:', error);
      throw error;
    }
  };

  return {
    tickets,
    funcionarios,
    filteredTickets,
    loading,
    filters: {
      funcionario: filterFuncionario,
      dataInicial: filterDataInicial,
      dataFinal: filterDataFinal,
      tipoRefeicao: filterTipoRefeicao,
    },
    setFilters: {
      setFilterFuncionario,
      setFilterDataInicial,
      setFilterDataFinal,
      setFilterTipoRefeicao,
    },
    actions: {
      loadData,
      clearFilters,
      createTicket,
    },
  };
};
