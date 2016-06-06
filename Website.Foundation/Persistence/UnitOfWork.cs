﻿using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Website.Foundation.Core;

namespace Website.Foundation.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _context;
        [Inject]
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }



        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
