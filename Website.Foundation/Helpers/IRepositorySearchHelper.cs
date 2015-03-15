using System;
namespace Website.Foundation.Helpers
{
    public interface IRepositorySearchHelper
    {
        bool IsAllPropertyNull<TSearch>(TSearch obj);
    }
}
