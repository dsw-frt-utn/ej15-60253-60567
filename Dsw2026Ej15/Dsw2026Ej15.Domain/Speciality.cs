using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Domain
{
    public class Speciality : BaseEntity
    {
        public string Name { get; }
        public string Description { get; }

        public Speciality(Guid id, string name, string description) : base(id)
        {
            Name = name;
            Description = description;
        }
    }
}