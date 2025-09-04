import { Calendar, User, Utensils } from 'lucide-react';
import { TicketDTO } from '@/types/ticket';
import { StatsCard } from './stats-card';

interface StatsGridProps {
  filteredTickets: TicketDTO[];
  filterDataInicial: string;
  filterDataFinal: string;
}

export const StatsGrid = ({ filteredTickets, filterDataInicial, filterDataFinal }: StatsGridProps) => {
  const formatDate = (dateString: string) => {
    return new Date(dateString).toLocaleDateString('pt-BR');
  };

  const getTotalTicketsByPeriod = () => {
    return filteredTickets.reduce((total, ticket) => total + ticket.quantidade, 0);
  };

  const getUniqueEmployeesCount = () => {
    return new Set(filteredTickets.map(t => t.funcionario.id)).size;
  };

  const getPeriodDisplay = () => {
    if (filterDataInicial && filterDataFinal) {
      return `${formatDate(filterDataInicial)} - ${formatDate(filterDataFinal)}`;
    }
    return 'Todos os períodos';
  };

  return (
    <div className="grid grid-cols-1 md:grid-cols-3 gap-6 mb-8">
      <StatsCard
        title="Total de Tickets"
        value={getTotalTicketsByPeriod()}
        icon={Utensils}
        iconColor="text-blue-600"
        iconBgColor="bg-blue-100"
      />
      
      <StatsCard
        title="Funcionários Atendidos"
        value={getUniqueEmployeesCount()}
        icon={User}
        iconColor="text-green-600"
        iconBgColor="bg-green-100"
      />
      
      <StatsCard
        title="Período Filtrado"
        value={getPeriodDisplay()}
        icon={Calendar}
        iconColor="text-purple-600"
        iconBgColor="bg-purple-100"
      />
    </div>
  );
};
