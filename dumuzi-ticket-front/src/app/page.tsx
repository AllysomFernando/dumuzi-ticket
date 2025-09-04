"use client";
import React, { useState } from 'react';
import { 
  StatsGrid, 
  TicketTable, 
  FilterModal, 
  CreateTicketModal,
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

  const handleShowFilterModal = () => {
    setShowFilterModal(true);
  };

  const handleShowCreateModal = () => {
    setShowCreateModal(true);
  };

  const handleClearFilters = () => {
    actions.clearFilters();
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

        <Toaster />
      </div>
    </div>
  );
};

export default TicketDashboard;