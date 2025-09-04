import { Plus, Filter } from 'lucide-react';
import { Button } from '@/components/ui/button';

interface DashboardHeaderProps {
  onCreateTicket?: () => void;
  onOpenFilters?: () => void;
  showCreateButton?: boolean;
  showFilterButton?: boolean;
  title?: string;
}

export const DashboardHeader = ({
  onCreateTicket,
  onOpenFilters,
  showCreateButton = false,
  showFilterButton = false,
  title = "Dashboard de Tickets"
}: DashboardHeaderProps) => {
  return (
    <div className="flex flex-col sm:flex-row sm:items-center sm:justify-between mb-8">
      <div>
        <h1 className="text-3xl font-bold text-gray-900">{title}</h1>
        <p className="mt-2 text-gray-600">
          Gerencie e visualize os tickets de refeição dos funcionários
        </p>
      </div>
      
      {(showCreateButton || showFilterButton) && (
        <div className="mt-4 sm:mt-0 flex space-x-3">
          {showFilterButton && (
            <Button
              variant="outline"
              onClick={onOpenFilters}
              className="flex items-center space-x-2"
            >
              <Filter className="w-4 h-4" />
              <span>Filtros</span>
            </Button>
          )}
          
          {showCreateButton && (
            <Button
              onClick={onCreateTicket}
              className="flex items-center space-x-2"
            >
              <Plus className="w-4 h-4" />
              <span>Novo Ticket</span>
            </Button>
          )}
        </div>
      )}
    </div>
  );
};
