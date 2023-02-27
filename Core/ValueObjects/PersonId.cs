using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Person;

namespace Core.ValueObjects
{
    public class PersonId : ValueObject
    {
        public Guid Id { get; private set; }

        public PersonId()
        {
            Id = Guid.NewGuid();
        }

        public PersonId(Guid id)
        {
            this.Id = id;
        }

        public static implicit operator Guid(PersonId personId) => personId.Id;

        public static explicit operator PersonId(Guid personId) => new(personId);
        public override string ToString() => Id.ToString();

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
        }
    }
}
