using DumuziTickets.Domain.Assertions;

namespace DumuziTickets.domain.entities;

public class TicketBO : AbstractEntityBO<int>
{
    private FuncionarioBO _funcionario;
    private int _quantidade;
    private EnumSituacao _situacao;

    public TicketBO(int id, int quantidade, FuncionarioBO funcionario, EnumSituacao situacao, DateTime createdAt,
        DateTime updatedAt) : base(id, createdAt, updatedAt)
    {
        Funcionario = funcionario;
        Quantidade = quantidade;
        Situacao = situacao;

        Validate();
    }

    public FuncionarioBO Funcionario
    {
        get => _funcionario;
        private set => _funcionario = value;
    }

    public int Quantidade
    {
        get => _quantidade;
        private set => _quantidade = value;
    }

    public EnumSituacao Situacao
    {
        get => _situacao;
        private set => _situacao = value;
    }

    private void Validate()
    {
        Assert.IsNotNull(Funcionario, "Um ticket precisa ter um funcionario");
    }
}
