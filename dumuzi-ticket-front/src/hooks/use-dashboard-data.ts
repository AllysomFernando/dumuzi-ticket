import { useState, useEffect, useCallback } from 'react';
import { TicketDTO, CreateTicketDTO, UpdateTicketDTO } from '@/types/ticket';
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

  const loadData = useCallback(async () => {
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
  }, []);

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
  }, [loadData]);

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

  const updateTicket = useCallback(async (id: number, data: UpdateTicketDTO) => {
    try {
      await ticketService.update(id, data);
      await loadData();
      return true;
    } catch (error) {
      console.error('Erro ao atualizar ticket:', error);
      return false;
    }
  }, [loadData]);

  const toggleTicketStatus = useCallback(async (id: number) => {
    try {
      // Get current ticket to check its status
      const ticket = tickets.find(t => t.id === id);
      if (!ticket) return false;
      
      if (ticket.situacao === 'A') {
        await ticketService.inativar(id);
      } else {
        await ticketService.ativar(id);
      }
      
      await loadData();
      return true;
    } catch (error) {
      console.error('Erro ao alterar status do ticket:', error);
      return false;
    }
  }, [loadData, tickets]);

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
      updateTicket,
      toggleTicketStatus,
    },
  };
};
