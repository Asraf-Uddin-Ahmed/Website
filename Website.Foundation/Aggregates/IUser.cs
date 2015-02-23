using System;
namespace Website.Foundation.Aggregates
{
    interface IUser
    {
        DateTime CreationTime { get; set; }
        string Email { get; set; }
        Guid ID { get; set; }
        string Password { get; set; }
        DateTime UpdateTime { get; set; }
        string UserName { get; set; }
    }
}
