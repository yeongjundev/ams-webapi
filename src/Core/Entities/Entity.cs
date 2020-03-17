using System;

namespace Core.Entities
{
    public abstract class Entity : IEntity
    {
        public DateTime CreateDateTime { get; set; }
        public DateTime UpdateDateTime { get; set; }

        public Entity()
        {
            CreateDateTime = DateTime.Now;
            UpdateDateTime = DateTime.Now;
        }
    }
}