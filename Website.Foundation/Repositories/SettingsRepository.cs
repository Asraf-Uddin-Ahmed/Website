using Website.Foundation.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;

namespace Website.Foundation.Repositories
{
    public class SettingsRepository : BaseEfRepository<Settings>, ISettingsRepository
    {
        private TableContext _context;
        [Inject]
        public SettingsRepository(TableContext context) : base(context)
        {
            _context = context;
        }
    }
}
