import { Utensils } from 'lucide-react';

interface EmptyStateProps {
  title?: string;
  description?: string;
}

export const EmptyState = ({ 
  title = "Nenhum ticket encontrado",
  description = "Comece criando um novo ticket de refeição."
}: EmptyStateProps) => {
  return (
    <div className="text-center py-12">
      <Utensils className="mx-auto h-12 w-12 text-gray-400" />
      <h3 className="mt-2 text-sm font-medium text-gray-900">{title}</h3>
      <p className="mt-1 text-sm text-gray-500">{description}</p>
    </div>
  );
};
