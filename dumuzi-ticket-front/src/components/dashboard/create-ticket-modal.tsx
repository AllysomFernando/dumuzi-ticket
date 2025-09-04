import React, { useState } from 'react';
import { Plus, User, Hash } from 'lucide-react';
import { toast } from 'sonner';
import { 
  Dialog, 
  DialogContent, 
  DialogHeader, 
  DialogTitle,
  DialogFooter 
} from '@/components/ui/dialog';
import { Button } from '@/components/ui/button';
import { FuncionarioDTO } from '@/types/funcionario';
import { CreateTicketDTO } from '@/types/ticket';

interface CreateTicketModalProps {
  isOpen: boolean;
  onClose: () => void;
  funcionarios: FuncionarioDTO[];
  onCreateTicket: (ticket: CreateTicketDTO) => Promise<void>;
}

export const CreateTicketModal = ({
  isOpen,
  onClose,
  funcionarios,
  onCreateTicket
}: CreateTicketModalProps) => {
  const [funcionarioId, setFuncionarioId] = useState('');
  const [quantidade, setQuantidade] = useState('1');
  const [loading, setLoading] = useState(false);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    
    if (!funcionarioId || !quantidade) {
      toast.error('Por favor, preencha todos os campos obrigatórios');
      return;
    }

    try {
      setLoading(true);
      await onCreateTicket({
        funcionarioId: parseInt(funcionarioId),
        quantidade: parseInt(quantidade)
      });
      
      const funcionario = funcionarios.find(f => f.id === parseInt(funcionarioId));
      toast.success(`Ticket criado com sucesso para ${funcionario?.nome}!`);
      
      setFuncionarioId('');
      setQuantidade('1');
      onClose();
    } catch (error: any) {
      toast.error(error?.response?.data.error)
    } finally {
      setLoading(false);
    }
  };

  const handleClose = () => {
    setFuncionarioId('');
    setQuantidade('1');
    onClose();
  };

  return (
    <Dialog open={isOpen} onOpenChange={handleClose}>
      <DialogContent className="sm:max-w-[400px]">
        <DialogHeader>
          <DialogTitle className="flex items-center gap-2">
            <Plus className="w-5 h-5" />
            Criar Novo Ticket
          </DialogTitle>
        </DialogHeader>
        
        <form onSubmit={handleSubmit}>
          <div className="grid gap-6 py-4">
            <div className="grid gap-2">
              <label htmlFor="funcionario" className="text-sm font-medium flex items-center gap-1">
                <User className="w-4 h-4" />
                Funcionário *
              </label>
              <select
                id="funcionario"
                value={funcionarioId}
                onChange={(e) => setFuncionarioId(e.target.value)}
                required
                className="flex h-9 w-full rounded-md border border-input bg-background px-3 py-1 text-sm shadow-sm placeholder:text-muted-foreground focus-visible:outline-none focus-visible:ring-1 focus-visible:ring-ring"
              >
                <option value="">Selecione um funcionário</option>
                {funcionarios
                  .filter(f => f.situacao === 'A')
                  .map((funcionario) => (
                  <option key={funcionario.id} value={funcionario.id.toString()}>
                    {funcionario.nome} - {funcionario.cpf}
                  </option>
                ))}
              </select>
            </div>

            <div className="grid gap-2">
              <label htmlFor="quantidade" className="text-sm font-medium flex items-center gap-1">
                <Hash className="w-4 h-4" />
                Quantidade de Tickets *
              </label>
              <input
                id="quantidade"
                type="number"
                min="1"
                value={quantidade}
                onChange={(e) => setQuantidade(e.target.value)}
                required
                className="flex h-9 w-full rounded-md border border-input bg-background px-3 py-1 text-sm shadow-sm placeholder:text-muted-foreground focus-visible:outline-none focus-visible:ring-1 focus-visible:ring-ring"
                placeholder="Ex: 2"
              />
            </div>
          </div>

          <DialogFooter className="gap-2">
            <Button
              type="button"
              variant="outline"
              onClick={handleClose}
              disabled={loading}
            >
              Cancelar
            </Button>
            <Button
              type="submit"
              disabled={loading || !funcionarioId || !quantidade}
              className="flex items-center gap-2"
            >
              {loading ? (
                <>
                  <div className="animate-spin rounded-full h-4 w-4 border-b-2 border-white" />
                  Criando...
                </>
              ) : (
                <>
                  <Plus className="w-4 h-4" />
                  Criar Ticket
                </>
              )}
            </Button>
          </DialogFooter>
        </form>
      </DialogContent>
    </Dialog>
  );
};
