import React, { useState, useEffect } from 'react';
import { Edit, User, CreditCard, ToggleLeft, ToggleRight } from 'lucide-react';
import { toast } from 'sonner';
import { 
  Dialog, 
  DialogContent, 
  DialogHeader, 
  DialogTitle,
  DialogFooter 
} from '@/components/ui/dialog';
import { Button } from '@/components/ui/button';
import { FuncionarioDTO, UpdateFuncionarioDTO } from '@/types/funcionario';
import { formatCPF, unformatCPF, validateCPF } from '@/lib/format-utils';

interface EditFuncionarioModalProps {
  isOpen: boolean;
  onClose: () => void;
  funcionario: FuncionarioDTO | null;
  onUpdateFuncionario: (id: number, funcionario: UpdateFuncionarioDTO) => Promise<void>;
}

export const EditFuncionarioModal = ({
  isOpen,
  onClose,
  funcionario,
  onUpdateFuncionario
}: EditFuncionarioModalProps) => {
  const [nome, setNome] = useState('');
  const [cpf, setCpf] = useState('');
  const [situacao, setSituacao] = useState<'A' | 'I'>('A');
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    if (funcionario) {
      setNome(funcionario.nome);
      setCpf(formatCPF(funcionario.cpf));
      setSituacao(funcionario.situacao);
    }
  }, [funcionario]);

  const handleCpfChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const formatted = formatCPF(e.target.value);
    if (formatted.length <= 14) {
      setCpf(formatted);
    }
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    
    if (!funcionario) return;

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
      await onUpdateFuncionario(funcionario.id, {
        nome: nome.trim(),
        cpf: unformatCPF(cpf),
        situacao
      });
      
      toast.success(`Funcionário ${nome} atualizado com sucesso!`);
      onClose();
    } catch (error: any) {
      toast.error(error?.response?.data.error)
    } finally {
      setLoading(false);
    }
  };

  const handleClose = () => {
    if (funcionario) {
      setNome(funcionario.nome);
      setCpf(formatCPF(funcionario.cpf));
      setSituacao(funcionario.situacao);
    }
    onClose();
  };

  if (!funcionario) return null;

  return (
    <Dialog open={isOpen} onOpenChange={handleClose}>
      <DialogContent className="sm:max-w-[400px]">
        <DialogHeader>
          <DialogTitle className="flex items-center gap-2">
            <Edit className="w-5 h-5" />
            Editar Funcionário
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
