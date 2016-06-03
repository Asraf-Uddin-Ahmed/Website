﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Website.Foundation.Core
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}
