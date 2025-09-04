import React, { useState } from 'react';
import { Plus, User, CreditCard, ToggleRight } from 'lucide-react';
import { toast } from 'sonner';
import { 
  Dialog, 
  DialogContent, 
  DialogHeader, 
  DialogTitle,
  DialogFooter 
} from '@/components/ui/dialog';
import { Button } from '@/components/ui/button';
import { CreateFuncionarioDTO } from '@/types/funcionario';

interface CreateFuncionarioModalProps {
  isOpen: boolean;
  onClose: () => void;
  onCreateFuncionario: (funcionario: CreateFuncionarioDTO) => Promise<void>;
}

export const CreateFuncionarioModal = ({
  isOpen,
  onClose,
  onCreateFuncionario
}: CreateFuncionarioModalProps) => {
  const [nome, setNome] = useState('');
  const [cpf, setCpf] = useState('');
  const [loading, setLoading] = useState(false);

  const formatCPF = (value: string) => {
    const numbers = value.replace(/\D/g, '');
    return numbers.replace(/(\d{3})(\d{3})(\d{3})(\d{2})/, '$1.$2.$3-$4');
  };

  const handleCpfChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const formatted = formatCPF(e.target.value);
    if (formatted.length <= 14) {
      setCpf(formatted);
    }
  };

  const validateCPF = (cpf: string) => {
    const numbers = cpf.replace(/\D/g, '');
    return numbers.length === 11;
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    
    if (!nome.trim()) {
      toast.error('Nome é obrigatório');
      return;
    }

    if (!validateCPF(cpf)) {
      toast.error('CPF deve ter 11 dígitos');
      return;
    }

    try {
      setLoading(true);
      await onCreateFuncionario({
        nome: nome.trim(),
        cpf: cpf.replace(/\D/g, ''), // Remove formatação
        situacao: 'A'
      });
      
      toast.success(`Funcionário ${nome} cadastrado com sucesso!`);
      
      // Reset form
      setNome('');
      setCpf('');
      onClose();
    } catch (error: unknown) {
      console.error('Erro ao criar funcionário:', error);
      if (error && typeof error === 'object' && 'response' in error) {
        const axiosError = error as { response?: { status?: number; data?: { message?: string } } };
        if (axiosError.response?.status === 400 && axiosError.response?.data?.message?.includes('CPF')) {
          toast.error('Este CPF já está cadastrado');
        } else {
          toast.error('Erro ao cadastrar funcionário. Tente novamente.');
        }
      } else {
        toast.error('Erro ao cadastrar funcionário. Tente novamente.');
      }
    } finally {
      setLoading(false);
    }
  };

  const handleClose = () => {
    setNome('');
    setCpf('');
    onClose();
  };

  return (
    <Dialog open={isOpen} onOpenChange={handleClose}>
      <DialogContent className="sm:max-w-[400px]">
        <DialogHeader>
          <DialogTitle className="flex items-center gap-2">
            <Plus className="w-5 h-5" />
            Cadastrar Funcionário
          </DialogTitle>
        </DialogHeader>
        
        <form onSubmit={handleSubmit}>
          <div className="grid gap-6 py-4">
            <div className="grid gap-2">
              <label htmlFor="nome" className="text-sm font-medium flex items-center gap-1">
                <User className="w-4 h-4" />
                Nome Completo *
              </label>
              <input
                id="nome"
                type="text"
                value={nome}
                onChange={(e) => setNome(e.target.value)}
                required
                className="flex h-9 w-full rounded-md border border-input bg-background px-3 py-1 text-sm shadow-sm placeholder:text-muted-foreground focus-visible:outline-none focus-visible:ring-1 focus-visible:ring-ring"
                placeholder="Ex: João Silva"
              />
            </div>

            <div className="grid gap-2">
              <label htmlFor="cpf" className="text-sm font-medium flex items-center gap-1">
                <CreditCard className="w-4 h-4" />
                CPF *
              </label>
              <input
                id="cpf"
                type="text"
                value={cpf}
                onChange={handleCpfChange}
                required
                className="flex h-9 w-full rounded-md border border-input bg-background px-3 py-1 text-sm shadow-sm placeholder:text-muted-foreground focus-visible:outline-none focus-visible:ring-1 focus-visible:ring-ring"
                placeholder="000.000.000-00"
              />
            </div>

            <div className="grid gap-2">
              <label className="text-sm font-medium flex items-center gap-1">
                <ToggleRight className="w-4 h-4 text-green-600" />
                Situação
              </label>
              <div className="flex items-center gap-2 text-sm text-green-600">
                <span className="px-2 py-1 bg-green-100 rounded-md">Ativo</span>
                <span className="text-xs text-gray-500">(Funcionários são sempre criados como ativos)</span>
              </div>
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
              disabled={loading || !nome.trim() || !validateCPF(cpf)}
              className="flex items-center gap-2"
            >
              {loading ? (
                <>
                  <div className="animate-spin rounded-full h-4 w-4 border-b-2 border-white" />
                  Cadastrando...
                </>
              ) : (
                <>
                  <Plus className="w-4 h-4" />
                  Cadastrar
                </>
              )}
            </Button>
          </DialogFooter>
        </form>
      </DialogContent>
    </Dialog>
  );
};
