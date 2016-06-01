
using Ninject;
using Website.Foundation.Core;
using Website.Foundation.Core.Repositories;
using Website.Foundation.Core.Services;
using Website.Foundation.Persistence.Repositories;
namespace Website.Foundation.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TableContext _context;

        [Inject]
        public UnitOfWork(TableContext context)
        {
            _context = context;
            PasswordVerifications = new PasswordVerificationRepository(context);
            Settings = new SettingsRepository(context);
            Users = new UserRepository(context);
            UserVerifications = new UserVerificationRepository(context);
        }

        public IPasswordVerificationRepository PasswordVerifications { get; private set; }
        public ISettingsRepository Settings { get; private set; }
        public IUserRepository Users { get; private set; }
        public IUserVerificationRepository UserVerifications { get; private set; }

        //public TRepo GetRepository<TRepo>() where TRepo : class
        //{
        //    using (StandardKernel kernel = new StandardKernel())
        //    {
        //        kernel.Load(Assembly.GetExecutingAssembly());
        //        TRepo result = kernel.Get<TRepo>(new ConstructorArgument("context", _context));
        //        if (result != null)
        //        {
        //            return result;
        //        }
        //    }
        //    throw new Exception("unable to create " + typeof(TRepo).FullName);
        //}

        public int Commit()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}