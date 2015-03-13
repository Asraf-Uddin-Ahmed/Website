﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Foundation.Aggregates;

namespace Website.Foundation.Repositories
{
    public interface ISettingsRepository : IBaseEfRepository<Settings>
    {
        string GetValueByName(string name);
        string GetDisplayNameByName(string name);
    }
}
