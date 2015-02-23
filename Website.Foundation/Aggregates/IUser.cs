using System;
namespace Website.Foundation.Aggregates
{
    public interface IUser : IEntity
    {
        DateTime CreationTime { get; set; }
        string Email { get; set; }
        string Password { get; set; }
        DateTime UpdateTime { get; set; }
        string UserName { get; set; }
    }
}
