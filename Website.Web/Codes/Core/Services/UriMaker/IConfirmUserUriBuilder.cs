﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Website.Web.Codes.Core.Services.UriMaker
{
    public interface IConfirmUserUriBuilder : IUriBuilder
    {
        void Build(string verificationCode);
    }
}
