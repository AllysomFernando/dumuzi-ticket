namespace DumuziTickets.domain.entities;

public class FuncionarioBO : AbstractEntityBO<int>
{   
    private string _nome;
    private string _cpf;
    private Situacao _situacao;
    
    public FuncionarioBO(int id, string nome, string cpf, Situacao situacao,  DateTime createdAt, DateTime updatedAt) : base(id, createdAt, updatedAt)
    {
        Nome = nome;
        Cpf = cpf;
        Situacao = situacao;
    }

    public string Nome
    {
        get => _nome;
        private set => _nome = value;
    }

    public string Cpf
    {
        get => _cpf;
        private set => _cpf = value;
    }

    public Situacao Situacao
    {
        get => _situacao;
        private set => _situacao = value;
    }
}