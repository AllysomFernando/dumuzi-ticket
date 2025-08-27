namespace DumuziTickets.domain.entities;

public abstract class AbstractEntityBO<K>
{
        private K _id;
        private DateTime _createdAt;
        private DateTime _updatedAt;

        protected AbstractEntityBO(K id, DateTime createdAt, DateTime updatedAt)
        {
                Id = id;
                CreatedAt = createdAt;
                UpdatedAt = updatedAt;
        }

        public K Id
        {
                get => _id;
                private set => _id = value;
        }
        
        public DateTime CreatedAt
        {
                get => _createdAt;
                private set => _createdAt = value;
        }

        public DateTime UpdatedAt
        {
                get => _updatedAt;
                private set => _updatedAt = value;
        }

        protected void UpdatedAtNow()
        {
                UpdatedAt = DateTime.Now;
        }
}