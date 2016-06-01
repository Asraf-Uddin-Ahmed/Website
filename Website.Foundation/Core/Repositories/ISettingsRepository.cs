using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Foundation.Core.Aggregates;
using Website.Foundation.Core.Enums;
using Website.Foundation.Core.Repositories;

namespace Website.Foundation.Core.Repositories
{
    public interface ISettingsRepository : IRepository<Settings>
    {
        string GetValueByName(SettingsName name);
        string GetDisplayNameByName(SettingsName name);
    }
}
