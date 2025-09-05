import React from 'react';
import { Calendar, Filter, X } from 'lucide-react';
import { 
  Dialog, 
  DialogContent, 
  DialogHeader, 
  DialogTitle,
  DialogFooter 
} from '@/components/ui/dialog';
import { Button } from '@/components/ui/button';
import { FuncionarioDTO } from '@/types/funcionario';

interface FilterModalProps {
  isOpen: boolean;
  onClose: () => void;
  funcionarios: FuncionarioDTO[];
  filters: {
    funcionario: string;
    dataInicial: string;
    dataFinal: string;
    tipoRefeicao: string;
  };
  onFilterChange: {
    setFilterFuncionario: (value: string) => void;
    setFilterDataInicial: (value: string) => void;
    setFilterDataFinal: (value: string) => void;
    setFilterTipoRefeicao: (value: string) => void;
  };
  onClearFilters: () => void;
}

export const FilterModal = ({
  isOpen,
  onClose,
  funcionarios,
  filters,
  onFilterChange,
  onClearFilters
}: FilterModalProps) => {
  const handleApplyFilters = () => {
    onClose();
  };

  const handleClearFilters = () => {
    onClearFilters();
    onClose();
  };

  return (
    <Dialog open={isOpen} onOpenChange={onClose}>
      <DialogContent className="sm:max-w-[500px]">
        <DialogHeader>
          <DialogTitle className="flex items-center gap-2">
            <Filter className="w-5 h-5" />
            Filtrar Tickets
          </DialogTitle>
        </DialogHeader>
        
        <div className="grid gap-6 py-4">
          <div className="grid gap-2">
            <label htmlFor="funcionario" className="text-sm font-medium">
              Funcionário
            </label>
            <select
              id="funcionario"
              value={filters.funcionario}
              onChange={(e) => onFilterChange.setFilterFuncionario(e.target.value)}
              className="flex h-9 w-full rounded-md border border-input bg-background px-3 py-1 text-sm shadow-sm placeholder:text-muted-foreground focus-visible:outline-none focus-visible:ring-1 focus-visible:ring-ring"
            >
              <option value="">Todos os funcionários</option>
              {funcionarios.map((funcionario) => (
                <option key={funcionario.id} value={funcionario.id.toString()}>
                  {funcionario.nome} - {funcionario.cpf}
                </option>
              ))}
            </select>
          </div>

          <div className="grid grid-cols-2 gap-4">
            <div className="grid gap-2">
              <label htmlFor="dataInicial" className="text-sm font-medium flex items-center gap-1">
                <Calendar className="w-4 h-4" />
                Data Inicial
              </label>
              <input
                id="dataInicial"
                type="date"
                value={filters.dataInicial}
                onChange={(e) => onFilterChange.setFilterDataInicial(e.target.value)}
                className="flex h-9 w-full rounded-md border border-input bg-background px-3 py-1 text-sm shadow-sm placeholder:text-muted-foreground focus-visible:outline-none focus-visible:ring-1 focus-visible:ring-ring"
              />
            </div>

            <div className="grid gap-2">
              <label htmlFor="dataFinal" className="text-sm font-medium flex items-center gap-1">
                <Calendar className="w-4 h-4" />
                Data Final
              </label>
              <input
                id="dataFinal"
                type="date"
                value={filters.dataFinal}
                onChange={(e) => onFilterChange.setFilterDataFinal(e.target.value)}
                className="flex h-9 w-full rounded-md border border-input bg-background px-3 py-1 text-sm shadow-sm placeholder:text-muted-foreground focus-visible:outline-none focus-visible:ring-1 focus-visible:ring-ring"
              />
            </div>
          </div>
        </div>

        <DialogFooter className="gap-2">
          <Button
            variant="outline"
            onClick={handleClearFilters}
            className="flex items-center gap-2"
          >
            <X className="w-4 h-4" />
            Limpar Filtros
          </Button>
          <Button onClick={handleApplyFilters}>
            Aplicar Filtros
          </Button>
        </DialogFooter>
      </DialogContent>
    </Dialog>
  );
};
