using Website.Foundation.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Website.Foundation.Repositories
{
    public class SettingsRepository : BaseEfRepository<Settings>, ISettingsRepository
    {
        private TableContext _context;
        public SettingsRepository()
        {
            _context = new TableContext();
        }
    }
}
