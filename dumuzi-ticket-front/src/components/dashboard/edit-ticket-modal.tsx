import React, { useState, useEffect } from 'react';
import { Edit, User, Hash, ToggleLeft, ToggleRight } from 'lucide-react';
import { toast } from 'sonner';
import { 
  Dialog, 
  DialogContent, 
  DialogHeader, 
  DialogTitle,
  DialogFooter 
} from '@/components/ui/dialog';
import { Button } from '@/components/ui/button';
import { TicketDTO, UpdateTicketDTO } from '@/types/ticket';
import { FuncionarioDTO } from '@/types/funcionario';
import { AxiosError } from 'axios';

interface EditTicketModalProps {
  isOpen: boolean;
  onClose: () => void;
  ticket: TicketDTO | null;
  funcionarios: FuncionarioDTO[];
  onUpdateTicket: (id: number, ticket: UpdateTicketDTO) => Promise<void>;
}

export const EditTicketModal = ({
  isOpen,
  onClose,
  ticket,
  funcionarios,
  onUpdateTicket
}: EditTicketModalProps) => {
  const [funcionarioId, setFuncionarioId] = useState('');
  const [quantidade, setQuantidade] = useState('1');
  const [situacao, setSituacao] = useState<'A' | 'I'>('A');
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    if (ticket) {
      setFuncionarioId(ticket.funcionario.id.toString());
      setQuantidade(ticket.quantidade.toString());
      setSituacao(ticket.situacao);
    }
  }, [ticket]);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    
    if (!ticket) return;

    if (!funcionarioId || !quantidade) {
      toast.error('Por favor, preencha todos os campos obrigatórios');
      return;
    }

    try {
      setLoading(true);
      await onUpdateTicket(ticket.id, {
        funcionarioId: parseInt(funcionarioId),
        quantidade: parseInt(quantidade),
        situacao
      });
      
      const funcionario = funcionarios.find(f => f.id === parseInt(funcionarioId));
      toast.success(`Ticket atualizado com sucesso para ${funcionario?.nome}!`);
      onClose();
    } catch (error) {
      const err = error as AxiosError<{ err: string }>;
      const backendMessage = err.response?.data?.err;

      toast.error(backendMessage ?? "Erro inesperado");
    } finally {
      setLoading(false);
    }
  };

  const handleClose = () => {
    if (ticket) {
      setFuncionarioId(ticket.funcionario.id.toString());
      setQuantidade(ticket.quantidade.toString());
      setSituacao(ticket.situacao);
    }
    onClose();
  };

  if (!ticket) return null;

  return (
    <Dialog open={isOpen} onOpenChange={handleClose}>
      <DialogContent className="sm:max-w-[400px]">
        <DialogHeader>
          <DialogTitle className="flex items-center gap-2">
            <Edit className="w-5 h-5" />
            Editar Ticket #{ticket.id}
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

            <div className="grid gap-2">
              <label className="text-sm font-medium flex items-center gap-1">
                {situacao === 'A' ? (
                  <ToggleRight className="w-4 h-4 text-green-600" />
                ) : (
                  <ToggleLeft className="w-4 h-4 text-red-600" />
                )}
                Situação
              </label>
              <div className="flex gap-2">
                <button
                  type="button"
                  onClick={() => setSituacao('A')}
                  className={`flex-1 py-2 px-3 rounded-md text-sm font-medium transition-colors ${
                    situacao === 'A' 
                      ? 'bg-green-100 text-green-800 border border-green-300' 
                      : 'bg-gray-100 text-gray-600 border border-gray-300 hover:bg-gray-200'
                  }`}
                >
                  <ToggleRight className="w-4 h-4 inline mr-1" />
                  Ativo
                </button>
                <button
                  type="button"
                  onClick={() => setSituacao('I')}
                  className={`flex-1 py-2 px-3 rounded-md text-sm font-medium transition-colors ${
                    situacao === 'I' 
                      ? 'bg-red-100 text-red-800 border border-red-300' 
                      : 'bg-gray-100 text-gray-600 border border-gray-300 hover:bg-gray-200'
                  }`}
                >
                  <ToggleLeft className="w-4 h-4 inline mr-1" />
                  Inativo
                </button>
              </div>
            </div>

            <div className="bg-blue-50 border border-blue-200 rounded-md p-3">
              <p className="text-xs text-blue-700">
                <strong>Data de Entrega:</strong> {new Date(ticket.dataEntrega || ticket.updatedAt).toLocaleString('pt-BR')}
              </p>
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
                  Salvando...
                </>
              ) : (
                <>
                  <Edit className="w-4 h-4" />
                  Salvar
                </>
              )}
            </Button>
          </DialogFooter>
        </form>
      </DialogContent>
    </Dialog>
  );
};
