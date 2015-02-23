using System;
namespace Website.Foundation.Aggregates
{
    public interface IEntity
    {
        Guid ID { get; set; }
    }
}
