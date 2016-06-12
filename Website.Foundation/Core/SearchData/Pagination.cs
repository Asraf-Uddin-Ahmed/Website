using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Website.Foundation.Core.SearchData
{
    public class Pagination
    {
        private const int DEFAULT_DISPLAY_SIZE = 10;
        public int DisplayStart { get; private set; }
        public int DisplaySize { get; private set; }


        public Pagination(int displayStart, int displaySize)
        {
            this.DisplayStart = Math.Max(0, displayStart);
            this.DisplaySize = Math.Max(0, displaySize);
        }
        public Pagination() : this(0, DEFAULT_DISPLAY_SIZE)
        {
        }
    }
}
