using System;

namespace Core.Entities
{
    public class Entity : IEntity
    {
        public DateTime CreateDateTime { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }
}