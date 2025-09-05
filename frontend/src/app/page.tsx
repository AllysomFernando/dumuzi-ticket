"use client";
import React, { useState } from 'react';
import { TicketDTO, UpdateTicketDTO } from '@/types/ticket';
import { 
  StatsGrid, 
  TicketTable, 
  FilterModal, 
  CreateTicketModal,
  EditTicketModal,
  StatsGridSkeleton,
  TableSkeleton
} from '@/components/dashboard';
import Header from '@/components/header';
import { Toaster } from '@/components/ui/sonner';
import { useDashboardData } from '@/hooks/use-dashboard-data';

const TicketDashboard = () => {
  const { 
    filteredTickets, 
    funcionarios, 
    loading, 
    filters, 
    setFilters,
    actions 
  } = useDashboardData();
  
  const [showFilterModal, setShowFilterModal] = useState(false);
  const [showCreateModal, setShowCreateModal] = useState(false);
  const [showEditModal, setShowEditModal] = useState(false);
  const [editingTicket, setEditingTicket] = useState<TicketDTO | null>(null);

  const handleShowFilterModal = () => {
    setShowFilterModal(true);
  };

  const handleShowCreateModal = () => {
    setShowCreateModal(true);
  };

  const handleClearFilters = () => {
    actions.clearFilters();
  };

  const handleEditTicket = (ticket: TicketDTO) => {
    setEditingTicket(ticket);
    setShowEditModal(true);
  };

  const handleToggleTicketStatus = async (ticket: TicketDTO) => {
    await actions.toggleTicketStatus(ticket.id);
  };

  const handleUpdateTicket = async (id: number, ticket: UpdateTicketDTO) => {
    await actions.updateTicket(id, ticket);
  };

  if (loading) {
    return (
      <div className="min-h-screen bg-gray-50">
        <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-8">
          <Header
            onShowFilterModal={handleShowFilterModal}
            onShowCreateModal={handleShowCreateModal}
            onClearFilters={handleClearFilters}
          />
          <StatsGridSkeleton />
          <TableSkeleton />
        </div>
      </div>
    );
  }

  return (
    <div className="min-h-screen bg-gray-50">
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-8">
        <Header
          onShowFilterModal={handleShowFilterModal}
          onShowCreateModal={handleShowCreateModal}
          onClearFilters={handleClearFilters}
        />
        
        <StatsGrid
          filteredTickets={filteredTickets}
          filterDataInicial={filters.dataInicial}
          filterDataFinal={filters.dataFinal}
        />
        
        <TicketTable
          tickets={filteredTickets}
          funcionarios={funcionarios}
          onEdit={handleEditTicket}
          onToggleStatus={handleToggleTicketStatus}
        />

        <FilterModal
          isOpen={showFilterModal}
          onClose={() => setShowFilterModal(false)}
          funcionarios={funcionarios}
          filters={filters}
          onFilterChange={setFilters}
          onClearFilters={actions.clearFilters}
        />

        <CreateTicketModal
          isOpen={showCreateModal}
          onClose={() => setShowCreateModal(false)}
          funcionarios={funcionarios}
          onCreateTicket={actions.createTicket}
        />

        <EditTicketModal
          isOpen={showEditModal}
          onClose={() => {
            setShowEditModal(false);
            setEditingTicket(null);
          }}
          ticket={editingTicket}
          funcionarios={funcionarios}
          onUpdateTicket={handleUpdateTicket}
        />

        <Toaster />
      </div>
    </div>
  );
};

export default TicketDashboard;