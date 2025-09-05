/**
 * Utilitários para formatação de datas
 */

/**
 * Formata uma data ISO string para o formato brasileiro
 * @param dateString - String de data no formato ISO (ex: "2025-09-03T15:50:23.913637")
 * @param includeTime - Se deve incluir horário na formatação
 * @returns Data formatada em pt-BR
 */
export const formatDate = (dateString: string, includeTime: boolean = false): string => {
  try {
    // Remove os microssegundos extras se existirem (mantém apenas 3 dígitos para milissegundos)
    const cleanDateString = dateString.replace(/(\.\d{3})\d*/, '$1');
    
    const date = new Date(cleanDateString);
    
    // Verifica se a data é válida
    if (isNaN(date.getTime())) {
      console.warn('Data inválida:', dateString);
      return 'Data inválida';
    }

    const options: Intl.DateTimeFormatOptions = {
      day: '2-digit',
      month: '2-digit',
      year: 'numeric',
      timeZone: 'America/Sao_Paulo' // Timezone do Brasil
    };

    if (includeTime) {
      options.hour = '2-digit';
      options.minute = '2-digit';
    }

    return date.toLocaleDateString('pt-BR', options);
  } catch (error) {
    console.error('Erro ao formatar data:', error);
    return 'Data inválida';
  }
};

/**
 * Formata uma data ISO string para o formato brasileiro com horário
 * @param dateString - String de data no formato ISO
 * @returns Data formatada em pt-BR com horário
 */
export const formatDateTime = (dateString: string): string => {
  return formatDate(dateString, true);
};

/**
 * Formata CPF para exibição
 * @param cpf - CPF sem formatação
 * @returns CPF formatado (000.000.000-00)
 */
export const formatCPF = (cpf: string): string => {
  return cpf.replace(/(\d{3})(\d{3})(\d{3})(\d{2})/, '$1.$2.$3-$4');
};

/**
 * Remove formatação do CPF
 * @param cpf - CPF formatado
 * @returns CPF apenas com números
 */
export const unformatCPF = (cpf: string): string => {
  return cpf.replace(/\D/g, '');
};

/**
 * Valida se um CPF tem 11 dígitos
 * @param cpf - CPF para validar
 * @returns true se válido
 */
export const validateCPF = (cpf: string): boolean => {
  const numbers = unformatCPF(cpf);
  return numbers.length === 11;
};
