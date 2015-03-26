using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Foundation.Aggregates;
using Website.Foundation.Enums;

namespace Website.Foundation.Repositories
{
    public interface ISettingsRepository : IBaseEfRepository<Settings>
    {
        string GetValueByName(SettingsName name);
        string GetDisplayNameByName(SettingsName name);
    }
}
