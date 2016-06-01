using System;
using Website.Foundation.Core.Repositories;

namespace Website.Foundation.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        ISettingsRepository Settings { get; }
        IUserRepository Users { get; }
        IUserVerificationRepository UserVerifications { get; }
        int Commit();
    }
}