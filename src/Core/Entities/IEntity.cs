using System;

namespace Core.Entities
{
    public interface IEntity
    {
        DateTime CreateDateTime { get; set; }
        DateTime UpdateDateTime { get; set; }
    }
}