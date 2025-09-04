import { Filter, Plus, Utensils } from "lucide-react";
import { Button } from "../ui/button";

interface HeaderProps {
	onShowFilterModal: () => void;
	onShowCreateModal: () => void;
	onClearFilters: () => void;
}

export default function Header({ onShowFilterModal, onShowCreateModal, onClearFilters }: HeaderProps) {
	return (
		<div className="bg-white rounded-lg shadow-sm border border-gray-200 p-6 mb-8">
			<div className="flex flex-col sm:flex-row justify-between items-start sm:items-center">
				<div>
					<h1 className="text-3xl font-bold text-gray-900 flex items-center">
						<Utensils className="mr-3 text-blue-600" size={32} />
						Gerenciamento de Tickets de Refeição
					</h1>
					<p className="text-gray-600 mt-2">
						Controle e visualize os tickets de refeição entregues para cada
						funcionário
					</p>
				</div>
				<div className="flex gap-3 mt-4 sm:mt-0">
					<Button
						variant="secondary"
						onClick={onShowFilterModal}
						className="flex items-center px-4 py-2 rounded-lg transition-colors"
					>
						<Filter size={16} className="mr-2" />
						Filtros
					</Button>

					<Button
						variant="destructive"
						onClick={onClearFilters}
						className="flex items-center px-4 py-2 rounded-lg transition-colors"
					>
						Limpar Filtros
					</Button>

					<Button
						variant="default"
						onClick={onShowCreateModal}
						className="flex items-center px-4 py-2 text-white rounded-lg transition-colors"
					>
						<Plus size={16} className="mr-2" />
						Novo Ticket
					</Button>
				</div>
			</div>
		</div>
	);
}
