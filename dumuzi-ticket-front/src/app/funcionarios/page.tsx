"use client";
import React, { useState, useEffect } from 'react';
import { Users, Plus } from 'lucide-react';
import { toast } from 'sonner';
import { Button } from '@/components/ui/button';
import { Toaster } from '@/components/ui/sonner';
import { 
  CreateFuncionarioModal, 
  EditFuncionarioModal, 
  FuncionarioTable,
  StatsGridSkeleton 
} from '@/components/dashboard';
import { FuncionarioDTO, CreateFuncionarioDTO, UpdateFuncionarioDTO } from '@/types/funcionario';
import { funcionarioService } from '@/services/funcionario';

const FuncionariosPage = () => {
  const [funcionarios, setFuncionarios] = useState<FuncionarioDTO[]>([]);
  const [loading, setLoading] = useState(true);
  const [showCreateModal, setShowCreateModal] = useState(false);
  const [showEditModal, setShowEditModal] = useState(false);
  const [selectedFuncionario, setSelectedFuncionario] = useState<FuncionarioDTO | null>(null);

  useEffect(() => {
    loadFuncionarios();
  }, []);

  const loadFuncionarios = async () => {
    try {
      setLoading(true);
      const data = await funcionarioService.getAll();
      setFuncionarios(data);
    } catch (error) {
      console.error('Erro ao carregar funcionários:', error);
      toast.error('Erro ao carregar funcionários');
    } finally {
      setLoading(false);
    }
  };

  const handleCreateFuncionario = async (funcionario: CreateFuncionarioDTO) => {
    await funcionarioService.create(funcionario);
    await loadFuncionarios();
  };

  const handleEditFuncionario = async (id: number, funcionario: UpdateFuncionarioDTO) => {
    await funcionarioService.update(id, funcionario);
    await loadFuncionarios();
  };

  const handleToggleStatus = async (funcionario: FuncionarioDTO) => {
    try {
      if (funcionario.situacao === 'A') {
        await funcionarioService.inativar(funcionario.id);
        toast.success(`Funcionário ${funcionario.nome} inativado com sucesso!`);
      } else {
        await funcionarioService.ativar(funcionario.id);
        toast.success(`Funcionário ${funcionario.nome} ativado com sucesso!`);
      }
      await loadFuncionarios();
    } catch (error) {
      console.error('Erro ao alterar status:', error);
      toast.error('Erro ao alterar status do funcionário');
    }
  };

  const handleEdit = (funcionario: FuncionarioDTO) => {
    setSelectedFuncionario(funcionario);
    setShowEditModal(true);
  };

  const getStats = () => {
    const total = funcionarios.length;
    const ativos = funcionarios.filter(f => f.situacao === 'A').length;
    const inativos = funcionarios.filter(f => f.situacao === 'I').length;
    
    return { total, ativos, inativos };
  };

  const stats = getStats();

  if (loading) {
    return (
      <div className="min-h-screen bg-gray-50">
        <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-8">
          <div className="bg-white rounded-lg shadow-sm border border-gray-200 p-6 mb-8">
            <div className="flex flex-col sm:flex-row justify-between items-start sm:items-center">
              <div className="animate-pulse">
                <div className="h-8 bg-gray-300 rounded w-64 mb-2"></div>
                <div className="h-4 bg-gray-300 rounded w-96"></div>
              </div>
            </div>
          </div>
          <StatsGridSkeleton />
        </div>
      </div>
    );
  }

  return (
    <div className="min-h-screen bg-gray-50">
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-8">
        {/* Header */}
        <div className="bg-white rounded-lg shadow-sm border border-gray-200 p-6 mb-8">
          <div className="flex flex-col sm:flex-row justify-between items-start sm:items-center">
            <div>
              <h1 className="text-3xl font-bold text-gray-900 flex items-center">
                <Users className="mr-3 text-blue-600" size={32} />
                Gerenciamento de Funcionários
              </h1>
              <p className="text-gray-600 mt-2">
                Cadastre e gerencie os funcionários da empresa
              </p>
            </div>
            <div className="mt-4 sm:mt-0">
              <Button
                onClick={() => setShowCreateModal(true)}
                className="flex items-center gap-2"
              >
                <Plus className="w-4 h-4" />
                Novo Funcionário
              </Button>
            </div>
          </div>
        </div>

        {/* Stats Cards */}
        <div className="grid grid-cols-1 md:grid-cols-3 gap-6 mb-8">
          <div className="bg-white rounded-lg shadow-sm border border-gray-200 p-6">
            <div className="flex items-center">
              <div className="flex-shrink-0">
                <div className="w-8 h-8 bg-blue-100 rounded-full flex items-center justify-center">
                  <Users className="w-5 h-5 text-blue-600" />
                </div>
              </div>
              <div className="ml-4">
                <p className="text-sm font-medium text-gray-600">Total de Funcionários</p>
                <p className="text-2xl font-bold text-gray-900">{stats.total}</p>
              </div>
            </div>
          </div>

          <div className="bg-white rounded-lg shadow-sm border border-gray-200 p-6">
            <div className="flex items-center">
              <div className="flex-shrink-0">
                <div className="w-8 h-8 bg-green-100 rounded-full flex items-center justify-center">
                  <Users className="w-5 h-5 text-green-600" />
                </div>
              </div>
              <div className="ml-4">
                <p className="text-sm font-medium text-gray-600">Funcionários Ativos</p>
                <p className="text-2xl font-bold text-gray-900">{stats.ativos}</p>
              </div>
            </div>
          </div>

          <div className="bg-white rounded-lg shadow-sm border border-gray-200 p-6">
            <div className="flex items-center">
              <div className="flex-shrink-0">
                <div className="w-8 h-8 bg-red-100 rounded-full flex items-center justify-center">
                  <Users className="w-5 h-5 text-red-600" />
                </div>
              </div>
              <div className="ml-4">
                <p className="text-sm font-medium text-gray-600">Funcionários Inativos</p>
                <p className="text-2xl font-bold text-gray-900">{stats.inativos}</p>
              </div>
            </div>
          </div>
        </div>

        {/* Funcionários Table */}
        <FuncionarioTable
          funcionarios={funcionarios}
          onEdit={handleEdit}
          onToggleStatus={handleToggleStatus}
        />

        {/* Modals */}
        <CreateFuncionarioModal
          isOpen={showCreateModal}
          onClose={() => setShowCreateModal(false)}
          onCreateFuncionario={handleCreateFuncionario}
        />

        <EditFuncionarioModal
          isOpen={showEditModal}
          onClose={() => setShowEditModal(false)}
          funcionario={selectedFuncionario}
          onUpdateFuncionario={handleEditFuncionario}
        />

        <Toaster />
      </div>
    </div>
  );
};

export default FuncionariosPage;
