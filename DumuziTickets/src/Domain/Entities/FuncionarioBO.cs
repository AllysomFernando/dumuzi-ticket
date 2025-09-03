using DumuziTickets.Domain.Assertions;
using DumuziTickets.Domain.Services;

namespace DumuziTickets.domain.entities;

public class FuncionarioBO : AbstractEntityBO<int>, IDeletableEntity
{
    private string _nome;
    private string _cpf;

    public FuncionarioBO(int id, string nome, string cpf, EnumSituacao situacao, DateTime updatedAt) : base(id, updatedAt, situacao)
    {
        Nome = nome;
        Cpf = cpf;
        Situacao = situacao;

        Validate();
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
    public void AtualizarFuncionario(FuncionarioBO bo)
    {
        Nome = bo.Nome;
        Situacao = bo.Situacao;
        AtualizarData();

        Validate();
    }

    private void Validate()
    {
        Assert.IsNotNull(Cpf, "Um funcionário precisa ter um cpf.");
        Assert.IsNotNull(Nome, "Um funcionário precisa ter um nome.");
        Assert.CpfIsValid(Cpf);
    }
}
