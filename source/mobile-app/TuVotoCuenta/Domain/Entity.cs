using System;
namespace TuVotoCuenta.Domain
{
    public class Entity
    {
        public byte EntityId { get; set; }
        public string EntityName { get; set; }

        public override string ToString()
        {
            return EntityName;
        }
    }
}
