using DumuziTickets.Domain.Assertions;

namespace DumuziTickets.domain.entities;

public abstract class AbstractEntityBO<K>
{
        private K _id;
        private DateTime _updatedAt;
        private EnumSituacao _situacao;

        protected AbstractEntityBO(K id, DateTime updatedAt, EnumSituacao situacao)
        {
                Id = id;
                UpdatedAt = updatedAt;
                Situacao = situacao;

                Validate();
        }

        public K Id
        {
                get => _id;
                private set => _id = value;
        }


        public DateTime UpdatedAt
        {
                get => _updatedAt;
                private set => _updatedAt = value;
        }

        public EnumSituacao Situacao
        {
                get => _situacao;
                protected set => _situacao = value;
        }

        public void AtualizarData()
        {
                _updatedAt = DateTime.UtcNow;
        }

        private void Validate()
        {
                Assert.IsFalse(Situacao == EnumSituacao.I, "Não pode criar um funcionário já inativo.");
        }
}
